namespace LaundryManagement.UI.Forms.Roles
{
    partial class frmNewPermission
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lblParent = new System.Windows.Forms.Label();
            this.txtPermissionName = new System.Windows.Forms.TextBox();
            this.lblPermissionName = new System.Windows.Forms.Label();
            this.lblPermissionCode = new System.Windows.Forms.Label();
            this.txtPermissionCode = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblFamilies = new System.Windows.Forms.Label();
            this.comboFamilies = new System.Windows.Forms.ComboBox();
            this.lblLeafs = new System.Windows.Forms.Label();
            this.btnAddLeaf = new System.Windows.Forms.Button();
            this.comboLeafs = new System.Windows.Forms.ComboBox();
            this.btnAddFamily = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(314, 30);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(290, 251);
            this.treeView1.TabIndex = 2;
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(314, 12);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(53, 15);
            this.lblParent.TabIndex = 3;
            this.lblParent.Text = "lblChilds";
            // 
            // txtPermissionName
            // 
            this.txtPermissionName.Location = new System.Drawing.Point(25, 30);
            this.txtPermissionName.Name = "txtPermissionName";
            this.txtPermissionName.Size = new System.Drawing.Size(215, 23);
            this.txtPermissionName.TabIndex = 4;
            // 
            // lblPermissionName
            // 
            this.lblPermissionName.AutoSize = true;
            this.lblPermissionName.Location = new System.Drawing.Point(25, 12);
            this.lblPermissionName.Name = "lblPermissionName";
            this.lblPermissionName.Size = new System.Drawing.Size(38, 15);
            this.lblPermissionName.TabIndex = 5;
            this.lblPermissionName.Text = "label1";
            // 
            // lblPermissionCode
            // 
            this.lblPermissionCode.AutoSize = true;
            this.lblPermissionCode.Location = new System.Drawing.Point(25, 80);
            this.lblPermissionCode.Name = "lblPermissionCode";
            this.lblPermissionCode.Size = new System.Drawing.Size(38, 15);
            this.lblPermissionCode.TabIndex = 7;
            this.lblPermissionCode.Text = "label2";
            // 
            // txtPermissionCode
            // 
            this.txtPermissionCode.Location = new System.Drawing.Point(25, 98);
            this.txtPermissionCode.Name = "txtPermissionCode";
            this.txtPermissionCode.Size = new System.Drawing.Size(215, 23);
            this.txtPermissionCode.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(204, 359);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 45);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "button1";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblFamilies
            // 
            this.lblFamilies.AutoSize = true;
            this.lblFamilies.Location = new System.Drawing.Point(25, 155);
            this.lblFamilies.Name = "lblFamilies";
            this.lblFamilies.Size = new System.Drawing.Size(38, 15);
            this.lblFamilies.TabIndex = 18;
            this.lblFamilies.Text = "label1";
            // 
            // comboFamilies
            // 
            this.comboFamilies.FormattingEnabled = true;
            this.comboFamilies.Location = new System.Drawing.Point(25, 173);
            this.comboFamilies.Name = "comboFamilies";
            this.comboFamilies.Size = new System.Drawing.Size(214, 23);
            this.comboFamilies.TabIndex = 17;
            // 
            // lblLeafs
            // 
            this.lblLeafs.AutoSize = true;
            this.lblLeafs.Location = new System.Drawing.Point(25, 240);
            this.lblLeafs.Name = "lblLeafs";
            this.lblLeafs.Size = new System.Drawing.Size(38, 15);
            this.lblLeafs.TabIndex = 16;
            this.lblLeafs.Text = "label1";
            // 
            // btnAddLeaf
            // 
            this.btnAddLeaf.Location = new System.Drawing.Point(68, 287);
            this.btnAddLeaf.Name = "btnAddLeaf";
            this.btnAddLeaf.Size = new System.Drawing.Size(118, 24);
            this.btnAddLeaf.TabIndex = 15;
            this.btnAddLeaf.Text = "button1";
            this.btnAddLeaf.UseVisualStyleBackColor = true;
            this.btnAddLeaf.Click += new System.EventHandler(this.btnAddLeaf_Click);
            // 
            // comboLeafs
            // 
            this.comboLeafs.FormattingEnabled = true;
            this.comboLeafs.Location = new System.Drawing.Point(25, 258);
            this.comboLeafs.Name = "comboLeafs";
            this.comboLeafs.Size = new System.Drawing.Size(214, 23);
            this.comboLeafs.TabIndex = 14;
            // 
            // btnAddFamily
            // 
            this.btnAddFamily.Location = new System.Drawing.Point(68, 202);
            this.btnAddFamily.Name = "btnAddFamily";
            this.btnAddFamily.Size = new System.Drawing.Size(118, 24);
            this.btnAddFamily.TabIndex = 19;
            this.btnAddFamily.Text = "button1";
            this.btnAddFamily.UseVisualStyleBackColor = true;
            this.btnAddFamily.Click += new System.EventHandler(this.btnAddFamily_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(402, 287);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(118, 24);
            this.btnRemove.TabIndex = 20;
            this.btnRemove.Text = "button1";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmNewPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 425);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddFamily);
            this.Controls.Add(this.lblFamilies);
            this.Controls.Add(this.comboFamilies);
            this.Controls.Add(this.lblLeafs);
            this.Controls.Add(this.btnAddLeaf);
            this.Controls.Add(this.comboLeafs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPermissionCode);
            this.Controls.Add(this.txtPermissionCode);
            this.Controls.Add(this.lblPermissionName);
            this.Controls.Add(this.txtPermissionName);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.treeView1);
            this.Name = "frmNewPermission";
            this.Text = "frmNewPermission";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewPermission_FormClosing);
            this.Load += new System.EventHandler(this.frmNewPermission_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.TextBox txtPermissionName;
        private System.Windows.Forms.Label lblPermissionName;
        private System.Windows.Forms.Label lblPermissionCode;
        private System.Windows.Forms.TextBox txtPermissionCode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblFamilies;
        private System.Windows.Forms.ComboBox comboFamilies;
        private System.Windows.Forms.Label lblLeafs;
        private System.Windows.Forms.Button btnAddLeaf;
        private System.Windows.Forms.ComboBox comboLeafs;
        private System.Windows.Forms.Button btnAddFamily;
        private System.Windows.Forms.Button btnRemove;
    }
}