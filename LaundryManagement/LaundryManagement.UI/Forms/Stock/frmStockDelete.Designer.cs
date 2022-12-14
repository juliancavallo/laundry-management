namespace LaundryManagement.UI.Forms.Stock
{
    partial class frmStockDelete
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
            this.btnImport = new System.Windows.Forms.Button();
            this.lblImportedItems = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(32, 130);
            this.grid.Name = "grid";
            this.grid.RowTemplate.Height = 25;
            this.grid.Size = new System.Drawing.Size(705, 245);
            this.grid.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(32, 29);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(213, 32);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "button1";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblImportedItems
            // 
            this.lblImportedItems.AutoSize = true;
            this.lblImportedItems.Location = new System.Drawing.Point(33, 102);
            this.lblImportedItems.Name = "lblImportedItems";
            this.lblImportedItems.Size = new System.Drawing.Size(38, 15);
            this.lblImportedItems.TabIndex = 15;
            this.lblImportedItems.Text = "label1";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(263, 401);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(213, 32);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "button1";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmStockDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 445);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblImportedItems);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.grid);
            this.Name = "frmStockDelete";
            this.Text = "frmStockDelete";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStockImport_FormClosing);
            this.Load += new System.EventHandler(this.frmStockImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblImportedItems;
        private System.Windows.Forms.Button btnDelete;
    }
}