namespace LaundryManagement.UI.Forms.Stock
{
    partial class frmStockReport
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.comboItemType = new System.Windows.Forms.ComboBox();
            this.comboItemLocation = new System.Windows.Forms.ComboBox();
            this.comboItemStatus = new System.Windows.Forms.ComboBox();
            this.lblItemType = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(25, 192);
            this.grid.Name = "grid";
            this.grid.RowTemplate.Height = 25;
            this.grid.Size = new System.Drawing.Size(705, 245);
            this.grid.TabIndex = 0;
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(24, 18);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(38, 15);
            this.lblItemCode.TabIndex = 6;
            this.lblItemCode.Text = "label1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(25, 139);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(213, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(25, 38);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(213, 23);
            this.txtCode.TabIndex = 4;
            // 
            // comboItemType
            // 
            this.comboItemType.FormattingEnabled = true;
            this.comboItemType.Location = new System.Drawing.Point(270, 38);
            this.comboItemType.Name = "comboItemType";
            this.comboItemType.Size = new System.Drawing.Size(213, 23);
            this.comboItemType.TabIndex = 7;
            // 
            // comboItemLocation
            // 
            this.comboItemLocation.FormattingEnabled = true;
            this.comboItemLocation.Location = new System.Drawing.Point(517, 38);
            this.comboItemLocation.Name = "comboItemLocation";
            this.comboItemLocation.Size = new System.Drawing.Size(213, 23);
            this.comboItemLocation.TabIndex = 8;
            // 
            // comboItemStatus
            // 
            this.comboItemStatus.FormattingEnabled = true;
            this.comboItemStatus.Location = new System.Drawing.Point(25, 89);
            this.comboItemStatus.Name = "comboItemStatus";
            this.comboItemStatus.Size = new System.Drawing.Size(213, 23);
            this.comboItemStatus.TabIndex = 9;
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(270, 18);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(38, 15);
            this.lblItemType.TabIndex = 10;
            this.lblItemType.Text = "label1";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(517, 18);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(38, 15);
            this.lblLocation.TabIndex = 11;
            this.lblLocation.Text = "label1";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(25, 71);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 15);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "label1";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(248, 456);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(213, 32);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "button1";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmStockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblItemType);
            this.Controls.Add(this.comboItemStatus);
            this.Controls.Add(this.comboItemLocation);
            this.Controls.Add(this.comboItemType);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.grid);
            this.Name = "frmStockReport";
            this.Text = "frmStockReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStockReport_FormClosing);
            this.Load += new System.EventHandler(this.frmStockReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ComboBox comboItemType;
        private System.Windows.Forms.ComboBox comboItemLocation;
        private System.Windows.Forms.ComboBox comboItemStatus;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnExport;
    }
}