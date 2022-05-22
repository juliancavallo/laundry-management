namespace LaundryManagement.UI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcesses = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdministrationUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdministration,
            this.menuProcesses,
            this.menuReports,
            this.menuLogout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAdministration
            // 
            this.menuAdministration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdministrationUsers});
            this.menuAdministration.Name = "menuAdministration";
            this.menuAdministration.Size = new System.Drawing.Size(98, 20);
            this.menuAdministration.Text = "Administration";
            // 
            // menuProcesses
            // 
            this.menuProcesses.Name = "menuProcesses";
            this.menuProcesses.Size = new System.Drawing.Size(70, 20);
            this.menuProcesses.Text = "Processes";
            // 
            // menuReports
            // 
            this.menuReports.Name = "menuReports";
            this.menuReports.Size = new System.Drawing.Size(59, 20);
            this.menuReports.Text = "Reports";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(57, 20);
            this.menuLogout.Text = "Logout";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // menuAdministrationUsers
            // 
            this.menuAdministrationUsers.Name = "menuAdministrationUsers";
            this.menuAdministrationUsers.Size = new System.Drawing.Size(180, 22);
            this.menuAdministrationUsers.Text = "Usuarios";
            this.menuAdministrationUsers.Click += new System.EventHandler(this.menuAdministrationUsers_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 597);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuAdministration;
        private ToolStripMenuItem menuProcesses;
        private ToolStripMenuItem menuReports;
        private ToolStripMenuItem menuLogout;
        private ToolStripMenuItem menuAdministrationUsers;
    }
}