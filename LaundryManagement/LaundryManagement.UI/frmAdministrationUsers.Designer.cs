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
            this.lblSelectedUser = new System.Windows.Forms.Label();
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
            this.gridUsers.SelectionChanged += new System.EventHandler(this.gridUsers_SelectionChanged);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Location = new System.Drawing.Point(53, 399);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(138, 49);
            this.btnNewUser.TabIndex = 1;
            this.btnNewUser.Text = "New";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(271, 399);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(138, 49);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(503, 399);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(138, 49);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblSelectedUser
            // 
            this.lblSelectedUser.AutoSize = true;
            this.lblSelectedUser.Location = new System.Drawing.Point(56, 340);
            this.lblSelectedUser.Name = "lblSelectedUser";
            this.lblSelectedUser.Size = new System.Drawing.Size(38, 15);
            this.lblSelectedUser.TabIndex = 4;
            this.lblSelectedUser.Text = "label1";
            // 
            // frmAdministrationUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 510);
            this.Controls.Add(this.lblSelectedUser);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNewUser);
            this.Controls.Add(this.gridUsers);
            this.Name = "frmAdministrationUsers";
            this.Text = "frmAdministrationUsers";
            this.Load += new System.EventHandler(this.frmAdministrationUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView gridUsers;
        private Button btnNewUser;
        private Button btnEdit;
        private Button btnDelete;
        private Label lblSelectedUser;
    }
}