namespace AQWE
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuServer = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuItemServerStop = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.mnuLogging = new System.Windows.Forms.MenuItem();
            this.mnuItemLogInfoMessages = new System.Windows.Forms.MenuItem();
            this.mnuItemLogWarnings = new System.Windows.Forms.MenuItem();
            this.mnuItemLogErrors = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.mnuItemWriteLogsToFile = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.mnuItemLogClientMessages = new System.Windows.Forms.MenuItem();
            this.mnuItemLogServerMessages = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.mnuItemClearLog = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.mnuItemAbout = new System.Windows.Forms.MenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusOnlineUsers = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsOnlineUsers = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSplit = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSplit2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuServer,
            this.menuItem8,
            this.mnuLogging,
            this.mnuHelp});
            // 
            // mnuServer
            // 
            this.mnuServer.Index = 0;
            this.mnuServer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.mnuItemServerStop,
            this.menuItem4,
            this.mnuItemExit});
            this.mnuServer.Text = "Server";
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Index = 0;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7});
            this.menuItem2.Text = "Start";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Default Port";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Custom Port";
            // 
            // mnuItemServerStop
            // 
            this.mnuItemServerStop.Enabled = false;
            this.mnuItemServerStop.Index = 1;
            this.mnuItemServerStop.Text = "Stop";
            this.mnuItemServerStop.Click += new System.EventHandler(this.mnuItemServerStop_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // mnuItemExit
            // 
            this.mnuItemExit.Index = 3;
            this.mnuItemExit.Text = "Exit";
            this.mnuItemExit.Click += new System.EventHandler(this.mnuItemExit_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Enabled = false;
            this.menuItem8.Index = 1;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
            this.menuItem8.Text = "Management";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "Connections";
            // 
            // mnuLogging
            // 
            this.mnuLogging.Index = 2;
            this.mnuLogging.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuItemLogInfoMessages,
            this.mnuItemLogWarnings,
            this.mnuItemLogErrors,
            this.menuItem14,
            this.mnuItemWriteLogsToFile,
            this.menuItem16,
            this.mnuItemLogClientMessages,
            this.mnuItemLogServerMessages,
            this.menuItem19,
            this.mnuItemClearLog});
            this.mnuLogging.Text = "Logging";
            // 
            // mnuItemLogInfoMessages
            // 
            this.mnuItemLogInfoMessages.Checked = true;
            this.mnuItemLogInfoMessages.Index = 0;
            this.mnuItemLogInfoMessages.Text = "Log info messages";
            this.mnuItemLogInfoMessages.Click += new System.EventHandler(this.mnuItemLogInfoMessages_Click);
            // 
            // mnuItemLogWarnings
            // 
            this.mnuItemLogWarnings.Checked = true;
            this.mnuItemLogWarnings.Index = 1;
            this.mnuItemLogWarnings.Text = "Log warnings";
            this.mnuItemLogWarnings.Click += new System.EventHandler(this.mnuItemLogWarnings_Click);
            // 
            // mnuItemLogErrors
            // 
            this.mnuItemLogErrors.Checked = true;
            this.mnuItemLogErrors.Index = 2;
            this.mnuItemLogErrors.Text = "Log errors";
            this.mnuItemLogErrors.Click += new System.EventHandler(this.mnuItemLogErrors_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 3;
            this.menuItem14.Text = "-";
            // 
            // mnuItemWriteLogsToFile
            // 
            this.mnuItemWriteLogsToFile.Checked = true;
            this.mnuItemWriteLogsToFile.Index = 4;
            this.mnuItemWriteLogsToFile.Text = "Write logs to file";
            this.mnuItemWriteLogsToFile.Click += new System.EventHandler(this.mnuItemWriteLogsToFile_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 5;
            this.menuItem16.Text = "-";
            // 
            // mnuItemLogClientMessages
            // 
            this.mnuItemLogClientMessages.Checked = true;
            this.mnuItemLogClientMessages.Index = 6;
            this.mnuItemLogClientMessages.Text = "Log client messages";
            this.mnuItemLogClientMessages.Click += new System.EventHandler(this.mnuItemLogClientMessages_Click);
            // 
            // mnuItemLogServerMessages
            // 
            this.mnuItemLogServerMessages.Checked = true;
            this.mnuItemLogServerMessages.Index = 7;
            this.mnuItemLogServerMessages.Text = "Log server messages";
            this.mnuItemLogServerMessages.Click += new System.EventHandler(this.mnuItemLogServerMessages_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 8;
            this.menuItem19.Text = "-";
            // 
            // mnuItemClearLog
            // 
            this.mnuItemClearLog.Index = 9;
            this.mnuItemClearLog.Text = "Clear log";
            this.mnuItemClearLog.Click += new System.EventHandler(this.mnuItemClearLog_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 3;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem22,
            this.menuItem23,
            this.menuItem25,
            this.mnuItemAbout});
            this.mnuHelp.Text = "Help";
            // 
            // menuItem22
            // 
            this.menuItem22.Enabled = false;
            this.menuItem22.Index = 0;
            this.menuItem22.Text = "Help";
            // 
            // menuItem23
            // 
            this.menuItem23.Enabled = false;
            this.menuItem23.Index = 1;
            this.menuItem23.Text = "Tutorials";
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 2;
            this.menuItem25.Text = "-";
            // 
            // mnuItemAbout
            // 
            this.mnuItemAbout.Index = 3;
            this.mnuItemAbout.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusOnlineUsers,
            this.tsOnlineUsers,
            this.tsSplit,
            this.statusWarnings,
            this.tsWarnings,
            this.tsSplit2,
            this.statusErrors,
            this.tsErrors});
            this.statusStrip1.Location = new System.Drawing.Point(0, 251);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(571, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusOnlineUsers
            // 
            this.statusOnlineUsers.Image = global::AQWE.Properties.Resources.onlineusers;
            this.statusOnlineUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusOnlineUsers.Name = "statusOnlineUsers";
            this.statusOnlineUsers.Size = new System.Drawing.Size(29, 17);
            this.statusOnlineUsers.Text = "0";
            // 
            // tsOnlineUsers
            // 
            this.tsOnlineUsers.Name = "tsOnlineUsers";
            this.tsOnlineUsers.Size = new System.Drawing.Size(73, 17);
            this.tsOnlineUsers.Text = "Online Users";
            // 
            // tsSplit
            // 
            this.tsSplit.Name = "tsSplit";
            this.tsSplit.Size = new System.Drawing.Size(10, 17);
            this.tsSplit.Text = "|";
            // 
            // statusWarnings
            // 
            this.statusWarnings.Image = global::AQWE.Properties.Resources.modicon;
            this.statusWarnings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusWarnings.Name = "statusWarnings";
            this.statusWarnings.Size = new System.Drawing.Size(29, 17);
            this.statusWarnings.Text = "0";
            // 
            // tsWarnings
            // 
            this.tsWarnings.Name = "tsWarnings";
            this.tsWarnings.Size = new System.Drawing.Size(57, 17);
            this.tsWarnings.Text = "Warnings";
            // 
            // tsSplit2
            // 
            this.tsSplit2.Name = "tsSplit2";
            this.tsSplit2.Size = new System.Drawing.Size(10, 17);
            this.tsSplit2.Text = "|";
            // 
            // statusErrors
            // 
            this.statusErrors.Image = global::AQWE.Properties.Resources.erroricon;
            this.statusErrors.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusErrors.Name = "statusErrors";
            this.statusErrors.Size = new System.Drawing.Size(29, 17);
            this.statusErrors.Text = "0";
            // 
            // tsErrors
            // 
            this.tsErrors.Name = "tsErrors";
            this.tsErrors.Size = new System.Drawing.Size(37, 17);
            this.tsErrors.Text = "Errors";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(0, -2);
            this.txtLog.Margin = new System.Windows.Forms.Padding(0);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtLog.Size = new System.Drawing.Size(571, 253);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 273);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "HardCore Emulator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuServer;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem mnuItemServerStop;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem mnuItemExit;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem mnuLogging;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuItem mnuItemLogInfoMessages;
        private System.Windows.Forms.MenuItem mnuItemLogWarnings;
        private System.Windows.Forms.MenuItem mnuItemLogErrors;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem mnuItemWriteLogsToFile;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem mnuItemLogClientMessages;
        private System.Windows.Forms.MenuItem mnuItemLogServerMessages;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem mnuItemClearLog;
        private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem mnuItemAbout;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ToolStripStatusLabel statusOnlineUsers;
        private System.Windows.Forms.ToolStripStatusLabel tsOnlineUsers;
        private System.Windows.Forms.ToolStripStatusLabel tsSplit;
        private System.Windows.Forms.ToolStripStatusLabel statusWarnings;
        private System.Windows.Forms.ToolStripStatusLabel tsWarnings;
        private System.Windows.Forms.ToolStripStatusLabel tsSplit2;
        private System.Windows.Forms.ToolStripStatusLabel statusErrors;
        private System.Windows.Forms.ToolStripStatusLabel tsErrors;
    }
}

