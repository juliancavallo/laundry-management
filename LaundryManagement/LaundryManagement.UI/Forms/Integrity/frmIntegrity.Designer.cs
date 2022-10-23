namespace LaundryManagement.UI.Forms.Integrity
{
    partial class frmIntegrity
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
            this.comboLanguages = new System.Windows.Forms.ComboBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnValidateCredentials = new System.Windows.Forms.Button();
            this.btnViewEntities = new System.Windows.Forms.Button();
            this.radioLastBackup = new System.Windows.Forms.RadioButton();
            this.radioRecalculate = new System.Windows.Forms.RadioButton();
            this.btnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboLanguages
            // 
            this.comboLanguages.FormattingEnabled = true;
            this.comboLanguages.Location = new System.Drawing.Point(129, 463);
            this.comboLanguages.Name = "comboLanguages";
            this.comboLanguages.Size = new System.Drawing.Size(121, 23);
            this.comboLanguages.TabIndex = 7;
            this.comboLanguages.SelectedIndexChanged += new System.EventHandler(this.comboLanguages_SelectedIndexChanged);
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(30, 26);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(357, 58);
            this.txtHeader.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(232, 108);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(232, 126);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(147, 23);
            this.txtPassword.TabIndex = 11;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(30, 108);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(30, 15);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(30, 126);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(147, 23);
            this.txtUser.TabIndex = 9;
            // 
            // btnValidateCredentials
            // 
            this.btnValidateCredentials.Location = new System.Drawing.Point(92, 173);
            this.btnValidateCredentials.Name = "btnValidateCredentials";
            this.btnValidateCredentials.Size = new System.Drawing.Size(228, 23);
            this.btnValidateCredentials.TabIndex = 13;
            this.btnValidateCredentials.Text = "button1";
            this.btnValidateCredentials.UseVisualStyleBackColor = true;
            this.btnValidateCredentials.Click += new System.EventHandler(this.btnValidateCredentials_Click);
            // 
            // btnViewEntities
            // 
            this.btnViewEntities.Location = new System.Drawing.Point(92, 230);
            this.btnViewEntities.Name = "btnViewEntities";
            this.btnViewEntities.Size = new System.Drawing.Size(228, 23);
            this.btnViewEntities.TabIndex = 14;
            this.btnViewEntities.Text = "button1";
            this.btnViewEntities.UseVisualStyleBackColor = true;
            this.btnViewEntities.Click += new System.EventHandler(this.btnViewEntities_Click);
            // 
            // radioLastBackup
            // 
            this.radioLastBackup.AutoSize = true;
            this.radioLastBackup.Location = new System.Drawing.Point(92, 301);
            this.radioLastBackup.Name = "radioLastBackup";
            this.radioLastBackup.Size = new System.Drawing.Size(94, 19);
            this.radioLastBackup.TabIndex = 15;
            this.radioLastBackup.TabStop = true;
            this.radioLastBackup.Text = "radioButton1";
            this.radioLastBackup.UseVisualStyleBackColor = true;
            // 
            // radioRecalculate
            // 
            this.radioRecalculate.AutoSize = true;
            this.radioRecalculate.Location = new System.Drawing.Point(92, 326);
            this.radioRecalculate.Name = "radioRecalculate";
            this.radioRecalculate.Size = new System.Drawing.Size(94, 19);
            this.radioRecalculate.TabIndex = 16;
            this.radioRecalculate.TabStop = true;
            this.radioRecalculate.Text = "radioButton2";
            this.radioRecalculate.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(92, 373);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(228, 23);
            this.btnAccept.TabIndex = 18;
            this.btnAccept.Text = "button1";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // frmIntegrity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 498);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.radioRecalculate);
            this.Controls.Add(this.radioLastBackup);
            this.Controls.Add(this.btnViewEntities);
            this.Controls.Add(this.btnValidateCredentials);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.comboLanguages);
            this.Name = "frmIntegrity";
            this.Text = "frmIntegrity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIntegrity_FormClosing);
            this.Load += new System.EventHandler(this.frmIntegrity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboLanguages;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnValidateCredentials;
        private System.Windows.Forms.Button btnViewEntities;
        private System.Windows.Forms.RadioButton radioLastBackup;
        private System.Windows.Forms.RadioButton radioRecalculate;
        private System.Windows.Forms.Button btnAccept;
    }
}