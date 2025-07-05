namespace FortiScheduler
{
    partial class frmFortiScheduler
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFortiScheduler));
            gbConnection = new GroupBox();
            tbPort = new TextBox();
            tbIP = new TextBox();
            lblIPPort = new Label();
            btnConnect = new Button();
            tbApiKey = new TextBox();
            lblAPIKey = new Label();
            label1 = new Label();
            tbConnectionStatus = new TextBox();
            gbSettings = new GroupBox();
            cbSchedules = new ComboBox();
            btnUpdateSchedule = new Button();
            dtpEndTime = new DateTimePicker();
            lblEndTime = new Label();
            dtpStartTime = new DateTimePicker();
            lblStart = new Label();
            lblScheduleName = new Label();
            gbEvents = new GroupBox();
            btnClearEvents = new Button();
            lbEvents = new ListBox();
            gbConnection.SuspendLayout();
            gbSettings.SuspendLayout();
            gbEvents.SuspendLayout();
            SuspendLayout();
            // 
            // gbConnection
            // 
            gbConnection.Controls.Add(tbPort);
            gbConnection.Controls.Add(tbIP);
            gbConnection.Controls.Add(lblIPPort);
            gbConnection.Controls.Add(btnConnect);
            gbConnection.Controls.Add(tbApiKey);
            gbConnection.Controls.Add(lblAPIKey);
            gbConnection.Controls.Add(label1);
            gbConnection.Controls.Add(tbConnectionStatus);
            gbConnection.Location = new Point(12, 12);
            gbConnection.Name = "gbConnection";
            gbConnection.Size = new Size(336, 165);
            gbConnection.TabIndex = 0;
            gbConnection.TabStop = false;
            gbConnection.Text = "Connection";
            // 
            // tbPort
            // 
            tbPort.Location = new Point(251, 53);
            tbPort.MaxLength = 5;
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(79, 23);
            tbPort.TabIndex = 2;
            // 
            // tbIP
            // 
            tbIP.Location = new Point(130, 53);
            tbIP.Name = "tbIP";
            tbIP.Size = new Size(115, 23);
            tbIP.TabIndex = 1;
            // 
            // lblIPPort
            // 
            lblIPPort.AutoSize = true;
            lblIPPort.Location = new Point(6, 56);
            lblIPPort.Name = "lblIPPort";
            lblIPPort.Size = new Size(56, 15);
            lblIPPort.TabIndex = 5;
            lblIPPort.Text = "IP / Port: ";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(181, 111);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(141, 23);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "Verify Connection";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            btnConnect.MouseDown += btnConnect_MouseDown;
            // 
            // tbApiKey
            // 
            tbApiKey.Location = new Point(130, 85);
            tbApiKey.Name = "tbApiKey";
            tbApiKey.Size = new Size(200, 23);
            tbApiKey.TabIndex = 3;
            tbApiKey.UseSystemPasswordChar = true;
            // 
            // lblAPIKey
            // 
            lblAPIKey.AutoSize = true;
            lblAPIKey.Location = new Point(6, 88);
            lblAPIKey.Name = "lblAPIKey";
            lblAPIKey.Size = new Size(53, 15);
            lblAPIKey.TabIndex = 2;
            lblAPIKey.Text = "API Key: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 25);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 1;
            label1.Text = "Connection Status: ";
            // 
            // tbConnectionStatus
            // 
            tbConnectionStatus.Enabled = false;
            tbConnectionStatus.Location = new Point(130, 22);
            tbConnectionStatus.Name = "tbConnectionStatus";
            tbConnectionStatus.Size = new Size(200, 23);
            tbConnectionStatus.TabIndex = 0;
            // 
            // gbSettings
            // 
            gbSettings.Controls.Add(cbSchedules);
            gbSettings.Controls.Add(btnUpdateSchedule);
            gbSettings.Controls.Add(dtpEndTime);
            gbSettings.Controls.Add(lblEndTime);
            gbSettings.Controls.Add(dtpStartTime);
            gbSettings.Controls.Add(lblStart);
            gbSettings.Controls.Add(lblScheduleName);
            gbSettings.Enabled = false;
            gbSettings.Location = new Point(354, 12);
            gbSettings.Name = "gbSettings";
            gbSettings.Size = new Size(244, 165);
            gbSettings.TabIndex = 1;
            gbSettings.TabStop = false;
            gbSettings.Text = "Settings";
            // 
            // cbSchedules
            // 
            cbSchedules.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSchedules.FormattingEnabled = true;
            cbSchedules.Location = new Point(107, 22);
            cbSchedules.Name = "cbSchedules";
            cbSchedules.Size = new Size(129, 23);
            cbSchedules.Sorted = true;
            cbSchedules.TabIndex = 9;
            // 
            // btnUpdateSchedule
            // 
            btnUpdateSchedule.Location = new Point(95, 111);
            btnUpdateSchedule.Name = "btnUpdateSchedule";
            btnUpdateSchedule.Size = new Size(141, 23);
            btnUpdateSchedule.TabIndex = 8;
            btnUpdateSchedule.Text = "Update Schedule";
            btnUpdateSchedule.UseVisualStyleBackColor = true;
            btnUpdateSchedule.Click += btnUpdateSchedule_Click;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "yyyy/MM/dd HH:mm";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(107, 82);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(129, 23);
            dtpEndTime.TabIndex = 7;
            // 
            // lblEndTime
            // 
            lblEndTime.AutoSize = true;
            lblEndTime.Location = new Point(6, 88);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(63, 15);
            lblEndTime.TabIndex = 4;
            lblEndTime.Text = "End Time: ";
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "yyyy/MM/dd HH:mm";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(107, 53);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(129, 23);
            dtpStartTime.TabIndex = 6;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(6, 56);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(67, 15);
            lblStart.TabIndex = 2;
            lblStart.Text = "Start Time: ";
            // 
            // lblScheduleName
            // 
            lblScheduleName.AutoSize = true;
            lblScheduleName.Location = new Point(6, 25);
            lblScheduleName.Name = "lblScheduleName";
            lblScheduleName.Size = new Size(96, 15);
            lblScheduleName.TabIndex = 0;
            lblScheduleName.Text = "Schedule Name: ";
            // 
            // gbEvents
            // 
            gbEvents.Controls.Add(btnClearEvents);
            gbEvents.Controls.Add(lbEvents);
            gbEvents.Location = new Point(12, 183);
            gbEvents.Name = "gbEvents";
            gbEvents.Size = new Size(586, 255);
            gbEvents.TabIndex = 2;
            gbEvents.TabStop = false;
            gbEvents.Text = "Events";
            // 
            // btnClearEvents
            // 
            btnClearEvents.Location = new Point(438, 226);
            btnClearEvents.Name = "btnClearEvents";
            btnClearEvents.Size = new Size(141, 23);
            btnClearEvents.TabIndex = 9;
            btnClearEvents.Text = "Clear Events";
            btnClearEvents.UseVisualStyleBackColor = true;
            btnClearEvents.Click += btnClearEvents_Click;
            // 
            // lbEvents
            // 
            lbEvents.Enabled = false;
            lbEvents.FormattingEnabled = true;
            lbEvents.ItemHeight = 15;
            lbEvents.Location = new Point(6, 22);
            lbEvents.Name = "lbEvents";
            lbEvents.Size = new Size(573, 199);
            lbEvents.TabIndex = 0;
            lbEvents.TabStop = false;
            // 
            // frmFortiScheduler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(610, 450);
            Controls.Add(gbEvents);
            Controls.Add(gbSettings);
            Controls.Add(gbConnection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmFortiScheduler";
            Text = "FortiScheduler";
            Load += frmFortiScheduler_Load;
            gbConnection.ResumeLayout(false);
            gbConnection.PerformLayout();
            gbSettings.ResumeLayout(false);
            gbSettings.PerformLayout();
            gbEvents.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbConnection;
        private GroupBox gbSettings;
        private GroupBox gbEvents;
        private TextBox tbConnectionStatus;
        private TextBox tbApiKey;
        private Label lblAPIKey;
        private Label label1;
        private Button btnConnect;
        private Label lblEndTime;
        private DateTimePicker dtpStartTime;
        private Label lblStart;
        private Label lblScheduleName;
        private DateTimePicker dtpEndTime;
        private Button btnUpdateSchedule;
        private Button btnClearEvents;
        private Label lblIPPort;
        private TextBox tbPort;
        private TextBox tbIP;
        public ListBox lbEvents;
        private ComboBox cbSchedules;
    }
}
