using System.Windows.Forms;

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
            this.menuAdministrationUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAdministrationArticles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdministrationCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdministrationItemTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.locationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcesses = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessesLaundryShipping = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessesClinicShipping = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessesInternalShipping = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuProcessesRoadMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuProcessesLaundryReception = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessesClinicReception = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuProcessesItemRemoval = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessesItemCreation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportsMovements = new System.Windows.Forms.ToolStripMenuItem();
            this.shippingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuAdministrationUsers,
            this.toolStripSeparator4,
            this.menuAdministrationArticles,
            this.menuAdministrationCategories,
            this.menuAdministrationItemTypes,
            this.toolStripSeparator5,
            this.locationsToolStripMenuItem});
            this.menuAdministration.Name = "menuAdministration";
            this.menuAdministration.Size = new System.Drawing.Size(98, 20);
            this.menuAdministration.Text = "Administration";
            // 
            // menuAdministrationUsers
            // 
            this.menuAdministrationUsers.Name = "menuAdministrationUsers";
            this.menuAdministrationUsers.Size = new System.Drawing.Size(180, 22);
            this.menuAdministrationUsers.Text = "Users";
            this.menuAdministrationUsers.Click += new System.EventHandler(this.menuAdministrationUsers_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // menuAdministrationArticles
            // 
            this.menuAdministrationArticles.Name = "menuAdministrationArticles";
            this.menuAdministrationArticles.Size = new System.Drawing.Size(180, 22);
            this.menuAdministrationArticles.Text = "Articles";
            // 
            // menuAdministrationCategories
            // 
            this.menuAdministrationCategories.Name = "menuAdministrationCategories";
            this.menuAdministrationCategories.Size = new System.Drawing.Size(180, 22);
            this.menuAdministrationCategories.Text = "Categories";
            // 
            // menuAdministrationItemTypes
            // 
            this.menuAdministrationItemTypes.Name = "menuAdministrationItemTypes";
            this.menuAdministrationItemTypes.Size = new System.Drawing.Size(180, 22);
            this.menuAdministrationItemTypes.Text = "Item Types";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // locationsToolStripMenuItem
            // 
            this.locationsToolStripMenuItem.Name = "locationsToolStripMenuItem";
            this.locationsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.locationsToolStripMenuItem.Text = "Locations";
            // 
            // menuProcesses
            // 
            this.menuProcesses.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProcessesLaundryShipping,
            this.menuProcessesClinicShipping,
            this.menuProcessesInternalShipping,
            this.toolStripSeparator1,
            this.menuProcessesRoadMap,
            this.toolStripSeparator2,
            this.menuProcessesLaundryReception,
            this.menuProcessesClinicReception,
            this.toolStripSeparator3,
            this.menuProcessesItemRemoval,
            this.menuProcessesItemCreation});
            this.menuProcesses.Name = "menuProcesses";
            this.menuProcesses.Size = new System.Drawing.Size(70, 20);
            this.menuProcesses.Text = "Processes";
            // 
            // menuProcessesLaundryShipping
            // 
            this.menuProcessesLaundryShipping.Name = "menuProcessesLaundryShipping";
            this.menuProcessesLaundryShipping.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesLaundryShipping.Text = "Laundry Shipping";
            // 
            // menuProcessesClinicShipping
            // 
            this.menuProcessesClinicShipping.Name = "menuProcessesClinicShipping";
            this.menuProcessesClinicShipping.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesClinicShipping.Text = "Clinic Shipping";
            // 
            // menuProcessesInternalShipping
            // 
            this.menuProcessesInternalShipping.Name = "menuProcessesInternalShipping";
            this.menuProcessesInternalShipping.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesInternalShipping.Text = "Internal Shipping";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // menuProcessesRoadMap
            // 
            this.menuProcessesRoadMap.Name = "menuProcessesRoadMap";
            this.menuProcessesRoadMap.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesRoadMap.Text = "Road Map";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // menuProcessesLaundryReception
            // 
            this.menuProcessesLaundryReception.Name = "menuProcessesLaundryReception";
            this.menuProcessesLaundryReception.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesLaundryReception.Text = "Laundry Reception";
            // 
            // menuProcessesClinicReception
            // 
            this.menuProcessesClinicReception.Name = "menuProcessesClinicReception";
            this.menuProcessesClinicReception.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesClinicReception.Text = "Clinic Reception";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // menuProcessesItemRemoval
            // 
            this.menuProcessesItemRemoval.Name = "menuProcessesItemRemoval";
            this.menuProcessesItemRemoval.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesItemRemoval.Text = "Item Removal";
            // 
            // menuProcessesItemCreation
            // 
            this.menuProcessesItemCreation.Name = "menuProcessesItemCreation";
            this.menuProcessesItemCreation.Size = new System.Drawing.Size(173, 22);
            this.menuProcessesItemCreation.Text = "Item Creation";
            // 
            // menuReports
            // 
            this.menuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReportsMovements,
            this.shippingsToolStripMenuItem});
            this.menuReports.Name = "menuReports";
            this.menuReports.Size = new System.Drawing.Size(59, 20);
            this.menuReports.Text = "Reports";
            // 
            // menuReportsMovements
            // 
            this.menuReportsMovements.Name = "menuReportsMovements";
            this.menuReportsMovements.Size = new System.Drawing.Size(137, 22);
            this.menuReportsMovements.Text = "Movements";
            // 
            // shippingsToolStripMenuItem
            // 
            this.shippingsToolStripMenuItem.Name = "shippingsToolStripMenuItem";
            this.shippingsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.shippingsToolStripMenuItem.Text = "Shippings";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(57, 20);
            this.menuLogout.Text = "Logout";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
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
        private ToolStripMenuItem menuAdministrationArticles;
        private ToolStripMenuItem menuAdministrationCategories;
        private ToolStripMenuItem menuAdministrationItemTypes;
        private ToolStripMenuItem locationsToolStripMenuItem;
        private ToolStripMenuItem menuProcessesLaundryShipping;
        private ToolStripMenuItem menuProcessesClinicShipping;
        private ToolStripMenuItem menuProcessesInternalShipping;
        private ToolStripMenuItem menuProcessesRoadMap;
        private ToolStripMenuItem menuProcessesItemCreation;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuProcessesLaundryReception;
        private ToolStripMenuItem menuProcessesClinicReception;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuProcessesItemRemoval;
        private ToolStripMenuItem menuReportsMovements;
        private ToolStripMenuItem shippingsToolStripMenuItem;
    }
}