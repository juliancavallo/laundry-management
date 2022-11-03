namespace LaundryManagement.UI.Forms.Reception
{
    partial class frmAdministrationReceptions
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
            this.btnNewReception = new System.Windows.Forms.Button();
            this.gridReceptions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridReceptions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewReception
            // 
            this.btnNewReception.Location = new System.Drawing.Point(33, 347);
            this.btnNewReception.Name = "btnNewReception";
            this.btnNewReception.Size = new System.Drawing.Size(138, 35);
            this.btnNewReception.TabIndex = 7;
            this.btnNewReception.Text = "New";
            this.btnNewReception.UseVisualStyleBackColor = true;
            this.btnNewReception.Click += new System.EventHandler(this.btnNewReception_Click);
            // 
            // gridReceptions
            // 
            this.gridReceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReceptions.Location = new System.Drawing.Point(33, 32);
            this.gridReceptions.Name = "gridReceptions";
            this.gridReceptions.RowTemplate.Height = 25;
            this.gridReceptions.Size = new System.Drawing.Size(588, 272);
            this.gridReceptions.TabIndex = 6;
            // 
            // frmAdministrationReceptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 414);
            this.Controls.Add(this.btnNewReception);
            this.Controls.Add(this.gridReceptions);
            this.Name = "frmAdministrationReceptions";
            this.Text = "frmAdministrationReceptions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdministrationReceptions_FormClosing);
            this.Load += new System.EventHandler(this.frmAdministrationReceptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridReceptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewReception;
        private System.Windows.Forms.DataGridView gridReceptions;
    }
}