namespace LaundryManagement.UI.Forms.Roadmap
{
    partial class frmRoadmapDetailReport
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
            this.gridRoadmapDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmapDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // gridRoadmapDetail
            // 
            this.gridRoadmapDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRoadmapDetail.Location = new System.Drawing.Point(25, 23);
            this.gridRoadmapDetail.Name = "gridRoadmapDetail";
            this.gridRoadmapDetail.RowTemplate.Height = 25;
            this.gridRoadmapDetail.Size = new System.Drawing.Size(522, 349);
            this.gridRoadmapDetail.TabIndex = 1;
            // 
            // frmRoadmapDetailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 397);
            this.Controls.Add(this.gridRoadmapDetail);
            this.Name = "frmRoadmapDetailReport";
            this.Text = "frmRoadmapDetailReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRoadmapDetailReport_FormClosing);
            this.Load += new System.EventHandler(this.frmRoadmapDetailReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmapDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridRoadmapDetail;
    }
}