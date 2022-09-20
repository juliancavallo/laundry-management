using System.Windows.Forms;

namespace LaundryManagement.UI
{
    partial class frmAdministrationUsers
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
            this.gridUsers = new System.Windows.Forms.DataGridView();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnViewRoles = new System.Windows.Forms.Button();
            this.btnEditRoles = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // gridUsers
            // 
            this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUsers.Location = new System.Drawing.Point(53, 54);
            this.gridUsers.Name = "gridUsers";
            this.gridUsers.RowTemplate.Height = 25;
            this.gridUsers.Size = new System.Drawing.Size(588, 272);
            this.gridUsers.TabIndex = 0;
            // 
            // btnNewUser
            // 
            this.btnNewUser.Location = new System.Drawing.Point(53, 369);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(138, 35);
            this.btnNewUser.TabIndex = 1;
            this.btnNewUser.Text = "New";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(271, 369);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(138, 35);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(503, 369);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(138, 35);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnViewRoles
            // 
            this.btnViewRoles.Location = new System.Drawing.Point(53, 428);
            this.btnViewRoles.Name = "btnViewRoles";
            this.btnViewRoles.Size = new System.Drawing.Size(138, 35);
            this.btnViewRoles.TabIndex = 4;
            this.btnViewRoles.Text = "View Roles";
            this.btnViewRoles.UseVisualStyleBackColor = true;
            this.btnViewRoles.Click += new System.EventHandler(this.btnViewRoles_Click);
            // 
            // btnEditRoles
            // 
            this.btnEditRoles.Location = new System.Drawing.Point(271, 428);
            this.btnEditRoles.Name = "btnEditRoles";
            this.btnEditRoles.Size = new System.Drawing.Size(138, 35);
            this.btnEditRoles.TabIndex = 5;
            this.btnEditRoles.Text = "Edit Roles";
            this.btnEditRoles.UseVisualStyleBackColor = true;
            this.btnEditRoles.Click += new System.EventHandler(this.btnEditRoles_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(503, 428);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(138, 35);
            this.btnHistory.TabIndex = 6;
            this.btnHistory.Text = "History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // frmAdministrationUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 482);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnEditRoles);
            this.Controls.Add(this.btnViewRoles);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNewUser);
            this.Controls.Add(this.gridUsers);
            this.Name = "frmAdministrationUsers";
            this.Text = "frmAdministrationUsers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdministrationUsers_FormClosing);
            this.Load += new System.EventHandler(this.frmAdministrationUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView gridUsers;
        private Button btnNewUser;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnViewRoles;
        private Button btnEditRoles;
        private Button btnHistory;
    }
}