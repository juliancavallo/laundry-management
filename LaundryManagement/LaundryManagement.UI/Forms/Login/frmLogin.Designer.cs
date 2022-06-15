using System.Windows.Forms;

namespace LaundryManagement.UI
{
    partial class frmLogin
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblResetPassword = new System.Windows.Forms.LinkLabel();
            this.comboLanguages = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(35, 57);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(228, 23);
            this.txtEmail.TabIndex = 0;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(35, 39);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(36, 15);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(80, 212);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(122, 38);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(35, 114);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(35, 132);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(228, 23);
            this.txtPassword.TabIndex = 3;
            // 
            // lblResetPassword
            // 
            this.lblResetPassword.AutoSize = true;
            this.lblResetPassword.Location = new System.Drawing.Point(35, 165);
            this.lblResetPassword.Name = "lblResetPassword";
            this.lblResetPassword.Size = new System.Drawing.Size(98, 15);
            this.lblResetPassword.TabIndex = 5;
            this.lblResetPassword.TabStop = true;
            this.lblResetPassword.Text = "lblResetPassword";
            this.lblResetPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResetPassword_LinkClicked);
            // 
            // comboLanguages
            // 
            this.comboLanguages.FormattingEnabled = true;
            this.comboLanguages.Location = new System.Drawing.Point(81, 282);
            this.comboLanguages.Name = "comboLanguages";
            this.comboLanguages.Size = new System.Drawing.Size(121, 23);
            this.comboLanguages.TabIndex = 6;
            this.comboLanguages.SelectedIndexChanged += new System.EventHandler(this.comboLanguages_SelectedIndexChanged);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 317);
            this.Controls.Add(this.comboLanguages);
            this.Controls.Add(this.lblResetPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Name = "frmLogin";
            this.Text = "frmLogin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtEmail;
        private Label lblEmail;
        private Button btnLogin;
        private Label lblPassword;
        private TextBox txtPassword;
        private LinkLabel lblResetPassword;
        private ComboBox comboLanguages;
    }
}