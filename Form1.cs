using FortiScheduler.Properties;
using System.Configuration;

namespace FortiScheduler
{
    public partial class frmFortiScheduler : Form
    {
        public frmFortiScheduler()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload(); // Reload settings to ensure they are up-to-date
            Properties.Settings.Default["firstOpen"] = true; // Set firstOpen to true on form initialization
        }

        // Logic to handle adding an event to the listbox lbEvents

        public void addEvent(string eventName)
        {
            if (!string.IsNullOrEmpty(eventName))
            {
                lbEvents.Items.Add("[" + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds + "] " + eventName);
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
            if (Settings.Default["connectionSettingsLocked"] != null && (bool)Settings.Default["connectionSettingsLocked"])
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

            if (Settings.Default["lastIP"] != null)
            {
                tbConnectionStatus.Text = Settings.Default["lastIP"].ToString();
            }
            addEvent("Form loaded successfully.");

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
                Settings.Default["lastIP"] = tbIP.Text;
                Settings.Default["lastAPIKey"] = tbApiKey.Text;
                Settings.Default.Save(); // Save the settings to the config file
                Settings.Default.Reload(); // Reload settings to reflect changes
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

        private void btnConnect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Right" && e.Clicks == 2)
            {
                // Toggle the connection settings lock state
                if (Settings.Default["connectionSettingsLocked"] == null || !(bool)Settings.Default["connectionSettingsLocked"])
                {
                    Settings.Default["connectionSettingsLocked"] = true;
                    tbIP.Enabled = false;
                    tbPort.Enabled = false;
                    tbApiKey.Enabled = false;
                    addEvent("Connection settings locked. Double right-click the Verify Connection button twice to unlock.");
                }
                else
                {
                    Settings.Default["connectionSettingsLocked"] = false;
                    tbIP.Enabled = true;
                    tbPort.Enabled = true;
                    tbApiKey.Enabled = true;
                    addEvent("Connection settings unlocked. You can now edit them.");
                }
                Settings.Default.Save(); // Save the lock state to the config file
                Settings.Default.Reload(); // Reload settings to reflect changes
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
