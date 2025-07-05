using FortiScheduler.Properties;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace FortiScheduler
{
    public partial class frmFortiScheduler : Form
    {
        // Define global variables and constants here if needed
        // For example, you can define a constant for the default port number
        public int DefaultPort = 443; // Default port for FortiGate API
        public string DefaultIP = "192.168.99.1"; // Default IP address for FortiGate
        public string DefaultApiKey = ""; // Default API key for FortiGate
        public bool isConnectionSettingsLocked = false; // Flag to check if connection settings are locked
        public bool autoConnect = false; // Flag to check if auto connect is enabled

        public frmFortiScheduler()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs(); // first argument is the executable name, second is the -l flag if present, third is the default IP, fourth is the default port, fifth is the default API key

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i].ToLower();
                switch (arg)
                {
                    case "-l":
                        isConnectionSettingsLocked = true; // Check if the settings are locked via command line argument
                        addEvent("Connection settings are locked by your IT Administrator.");
                        break;
                    case "-host":
                        if (i + 1 < args.Length) // Check if there is a next argument for the host
                        {
                            DefaultIP = args[i + 1]; // Set the default IP from command line argument
                            addEvent($"Default IP set to {DefaultIP}");
                        }
                        break;
                    case "-port":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int port)) // Check if there is a next argument for the port and if it's a valid integer
                        {
                            DefaultPort = port; // Set the default port from command line argument
                            addEvent($"Default Port set to {DefaultPort}");
                        }
                        break;
                    case "-apikey":
                        if (i + 1 < args.Length) // Check if there is a next argument for the API key
                        {
                            DefaultApiKey = args[i + 1]; // Set the default API key from command line argument
                            addEvent("Default API Key set.");
                        }
                        break;
                    case "-a":
                        autoConnect = true; // Check if auto connect is enabled via command line argument
                        addEvent("Auto connect is enabled.");
                        break;
                    case "-s":
                        lbEvents.Visible = false; // Hide the event log listbox
                        btnClearEvents.Visible = false; // Hide the clear events button
                        gbEvents.Visible = false; // Hide the events group box
                        this.Size = new Size(630, 220); // Resize the form for silent mode
                        addEvent("Silent mode enabled. Event log is hidden.");
                        break;
                }
            }

            
        }

        // Logic to handle adding an event to the listbox lbEvents

        public void addEvent(string eventName)
        {
            if (!string.IsNullOrEmpty(eventName))
            {
                lbEvents.Items.Add("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + eventName);
            }
            else
            {
                MessageBox.Show("Event name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //oad event handler for the form
        private void frmFortiScheduler_Load(object sender, EventArgs e)
        {
            
            // Check if the connection settings are locked, if so, disable the input fields
            if (isConnectionSettingsLocked)
            {
                tbIP.Enabled = false;
                tbPort.Enabled = false;
                tbApiKey.Enabled = false;

                addEvent("Connection settings are locked by your IT Administrator.");
            }
            else
            {
                tbIP.Enabled = true;
                tbPort.Enabled = true;
                tbApiKey.Enabled = true;
                btnConnect.Enabled = true;
            }

            tbIP.Text = DefaultIP;
            tbPort.Text = DefaultPort.ToString(); // Set the default port
            tbApiKey.Text = DefaultApiKey; // Set the default API key

            addEvent("Form loaded successfully.");
            if (autoConnect
                )
            {
                addEvent("Auto connect is enabled. Attempting to connect...");
                btnConnect.PerformClick(); // Automatically attempt to connect if auto connect is enabled
            }
            else
            {
                addEvent("Auto connect is disabled. Please click the Verify Connection button to connect.");
            }

            //Set dtpStartTime and dtpEndTime to today at 8:00 AM and 6:30 PM respectively
            dtpStartTime.Value = DateTime.Today.AddHours(8); // Set start time to 8:00 AM
            dtpEndTime.Value = DateTime.Today.AddHours(18).AddMinutes(30); // Set end time to 6:30 PM
            addEvent("Default start time set to 8:00 AM and end time set to 6:30 PM.");


        }

        private void btnClearEvents_Click(object sender, EventArgs e)
        {
            lbEvents.Items.Clear();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (tbIP.Text == string.Empty || tbPort.Text == string.Empty || tbApiKey.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbPort.Text.Length < 1 || tbPort.Text.Length > 5 || !int.TryParse(tbPort.Text, out _))
            {
                MessageBox.Show("Please enter a valid port number (1-65535).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Connection connection = new Connection(tbIP.Text, Convert.ToInt32(tbPort.Text), tbApiKey.Text);
            await connection.ConnectAsync(); // Await the asynchronous call to fix CS4014

            if (connection.IsConnected)
            {
                // Save the last connection details to the settings
                DefaultIP = tbIP.Text;
                DefaultApiKey = tbApiKey.Text;
                DefaultPort = Convert.ToInt32(tbPort.Text);

                tbConnectionStatus.Text = $"Connected to {tbIP.Text}:{tbPort.Text}";
                addEvent($"Connected to {tbIP.Text}:{tbPort.Text}");
                btnConnect.Enabled = false; // Disable the connect button after a successful connection



                // Populate the schedule combo box with available schedules
                cbSchedules.Items.Clear();
                List<string> schedules = await connection.GetSchedulesAsync(); // Assuming GetSchedulesAsync is a method that fetches schedules
                if (schedules.Count == 0)
                {
                    addEvent("No schedules found. Add some schedules in your Fortigate to use this program.");

                }
                else
                {
                    addEvent($"Found {schedules.Count} schedules.");
                    gbSettings.Enabled = true;
                    cbSchedules.Items.AddRange(schedules.ToArray());
                    if (schedules.Count == 1) { cbSchedules.SelectedIndex = 0; }
                }

            }
            else
            {
                tbConnectionStatus.Text = "Connection failed.";
                addEvent("Connection failed. Please check your settings.");
            }
        }

    

        private async void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            // Check if dtpStart.Value < dtpEnd.Value
            if (dtpStartTime.Value >= dtpEndTime.Value)
            {
                MessageBox.Show("The start time must be earlier than the end time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Check if a schedule is selected
            if (cbSchedules.SelectedItem == null)
            {
                MessageBox.Show("Please select a schedule to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                // Get the selected schedule
                string selectedSchedule = cbSchedules.SelectedItem.ToString();
                // Get the start and end times from the date time pickers
                DateTime startTime = dtpStartTime.Value;
                DateTime endTime = dtpEndTime.Value;
                // Create a new Connection object with the current settings
                Connection connection = new Connection(tbIP.Text, Convert.ToInt32(tbPort.Text), tbApiKey.Text);
                // Call the UpdateScheduleAsync method to update the schedule
                await connection.UpdateScheduleAsync(selectedSchedule, startTime, endTime); // Await the asynchronous call to fix CS4014

                if (connection.isUpdated)
                {
                    addEvent($"Schedule '{selectedSchedule}' updated successfully from {startTime} to {endTime}.");
                    MessageBox.Show($"Schedule '{selectedSchedule}' updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    addEvent($"Failed to update schedule '{selectedSchedule}'.");
                    MessageBox.Show($"Failed to update schedule '{selectedSchedule}'. Please check your connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
    }
}
