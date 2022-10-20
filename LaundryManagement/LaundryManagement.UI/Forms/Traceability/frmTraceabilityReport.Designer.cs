namespace LaundryManagement.UI.Forms.Traceability
{
    partial class frmTraceabilityReport
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
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblMovementType = new System.Windows.Forms.Label();
            this.comboMovementType = new System.Windows.Forms.ComboBox();
            this.lblItemStatus = new System.Windows.Forms.Label();
            this.comboItemStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(37, 138);
            this.grid.Name = "grid";
            this.grid.RowTemplate.Height = 25;
            this.grid.Size = new System.Drawing.Size(691, 287);
            this.grid.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(37, 41);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(185, 23);
            this.txtCode.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(37, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(152, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(36, 21);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(38, 15);
            this.lblItemCode.TabIndex = 3;
            this.lblItemCode.Text = "label1";
            // 
            // lblMovementType
            // 
            this.lblMovementType.AutoSize = true;
            this.lblMovementType.Location = new System.Drawing.Point(289, 16);
            this.lblMovementType.Name = "lblMovementType";
            this.lblMovementType.Size = new System.Drawing.Size(38, 15);
            this.lblMovementType.TabIndex = 9;
            this.lblMovementType.Text = "label1";
            // 
            // comboMovementType
            // 
            this.comboMovementType.FormattingEnabled = true;
            this.comboMovementType.Location = new System.Drawing.Point(291, 41);
            this.comboMovementType.Name = "comboMovementType";
            this.comboMovementType.Size = new System.Drawing.Size(185, 23);
            this.comboMovementType.TabIndex = 8;
            // 
            // lblItemStatus
            // 
            this.lblItemStatus.AutoSize = true;
            this.lblItemStatus.Location = new System.Drawing.Point(541, 16);
            this.lblItemStatus.Name = "lblItemStatus";
            this.lblItemStatus.Size = new System.Drawing.Size(38, 15);
            this.lblItemStatus.TabIndex = 11;
            this.lblItemStatus.Text = "label1";
            // 
            // comboItemStatus
            // 
            this.comboItemStatus.FormattingEnabled = true;
            this.comboItemStatus.Location = new System.Drawing.Point(543, 41);
            this.comboItemStatus.Name = "comboItemStatus";
            this.comboItemStatus.Size = new System.Drawing.Size(185, 23);
            this.comboItemStatus.TabIndex = 10;
            // 
            // frmTraceabilityReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 454);
            this.Controls.Add(this.lblItemStatus);
            this.Controls.Add(this.comboItemStatus);
            this.Controls.Add(this.lblMovementType);
            this.Controls.Add(this.comboMovementType);
            this.Controls.Add(this.lblItemCode);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.grid);
            this.Name = "frmTraceabilityReport";
            this.Text = "frmTraceabilityReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTraceabilityReport_FormClosing);
            this.Load += new System.EventHandler(this.frmTraceabilityReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblMovementType;
        private System.Windows.Forms.ComboBox comboMovementType;
        private System.Windows.Forms.Label lblItemStatus;
        private System.Windows.Forms.ComboBox comboItemStatus;
    }
}