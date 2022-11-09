namespace LaundryManagement.UI.Forms.Reception
{
    partial class frmReceptionDetailReport
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
            this.gridReceptionDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridReceptionDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // gridReceptionDetail
            // 
            this.gridReceptionDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReceptionDetail.Location = new System.Drawing.Point(33, 26);
            this.gridReceptionDetail.Name = "gridReceptionDetail";
            this.gridReceptionDetail.RowTemplate.Height = 25;
            this.gridReceptionDetail.Size = new System.Drawing.Size(522, 349);
            this.gridReceptionDetail.TabIndex = 1;
            // 
            // frmReceptionDetailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 396);
            this.Controls.Add(this.gridReceptionDetail);
            this.Name = "frmReceptionDetailReport";
            this.Text = "frmReceptionDetailReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReceptionDetailReport_FormClosing);
            this.Load += new System.EventHandler(this.frmReceptionDetailReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridReceptionDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridReceptionDetail;
    }
}