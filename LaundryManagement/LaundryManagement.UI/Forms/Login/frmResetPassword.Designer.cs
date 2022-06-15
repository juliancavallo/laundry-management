namespace LaundryManagement.UI
{
    partial class frmResetPassword
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
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblRepeatPassword = new System.Windows.Forms.Label();
            this.txtNewPasswordRepeated = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(38, 114);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(84, 15);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "New password";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(38, 132);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(228, 23);
            this.txtNewPassword.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(38, 39);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(36, 15);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(38, 57);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(228, 23);
            this.txtEmail.TabIndex = 5;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(84, 266);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(122, 38);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblRepeatPassword
            // 
            this.lblRepeatPassword.AutoSize = true;
            this.lblRepeatPassword.Location = new System.Drawing.Point(38, 190);
            this.lblRepeatPassword.Name = "lblRepeatPassword";
            this.lblRepeatPassword.Size = new System.Drawing.Size(141, 15);
            this.lblRepeatPassword.TabIndex = 11;
            this.lblRepeatPassword.Text = "Repeat the new password";
            // 
            // txtNewPasswordRepeated
            // 
            this.txtNewPasswordRepeated.Location = new System.Drawing.Point(38, 208);
            this.txtNewPasswordRepeated.Name = "txtNewPasswordRepeated";
            this.txtNewPasswordRepeated.Size = new System.Drawing.Size(228, 23);
            this.txtNewPasswordRepeated.TabIndex = 10;
            // 
            // frmResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 328);
            this.ControlBox = false;
            this.Controls.Add(this.lblRepeatPassword);
            this.Controls.Add(this.txtNewPasswordRepeated);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmResetPassword";
            this.Text = "frmResetPassword";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmResetPassword_FormClosing);
            this.Load += new System.EventHandler(this.frmResetPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblRepeatPassword;
        private System.Windows.Forms.TextBox txtNewPasswordRepeated;
    }
}