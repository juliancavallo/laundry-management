namespace LaundryManagement.UI.Forms.Roadmap
{
    partial class frmRoadmapReport
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.btnViewDetail = new System.Windows.Forms.Button();
            this.gridRoadmaps = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmaps)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(117, 386);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(175, 40);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(277, 15);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(38, 15);
            this.lblDateTo.TabIndex = 12;
            this.lblDateTo.Text = "label2";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(33, 15);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(38, 15);
            this.lblDateFrom.TabIndex = 11;
            this.lblDateFrom.Text = "label1";
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(277, 33);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(200, 23);
            this.dateTo.TabIndex = 10;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(33, 33);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(200, 23);
            this.dateFrom.TabIndex = 9;
            // 
            // btnViewDetail
            // 
            this.btnViewDetail.Location = new System.Drawing.Point(385, 386);
            this.btnViewDetail.Name = "btnViewDetail";
            this.btnViewDetail.Size = new System.Drawing.Size(175, 40);
            this.btnViewDetail.TabIndex = 8;
            this.btnViewDetail.Text = "button1";
            this.btnViewDetail.UseVisualStyleBackColor = true;
            this.btnViewDetail.Click += new System.EventHandler(this.btnViewDetail_Click);
            // 
            // gridRoadmaps
            // 
            this.gridRoadmaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRoadmaps.Location = new System.Drawing.Point(33, 94);
            this.gridRoadmaps.Name = "gridRoadmaps";
            this.gridRoadmaps.RowTemplate.Height = 25;
            this.gridRoadmaps.Size = new System.Drawing.Size(628, 266);
            this.gridRoadmaps.TabIndex = 7;
            // 
            // frmRoadmapReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 450);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.btnViewDetail);
            this.Controls.Add(this.gridRoadmaps);
            this.Name = "frmRoadmapReport";
            this.Text = "frmRoadmapReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRoadmapReport_FormClosing);
            this.Load += new System.EventHandler(this.frmRoadmapReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmaps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.Button btnViewDetail;
        private System.Windows.Forms.DataGridView gridRoadmaps;
    }
}