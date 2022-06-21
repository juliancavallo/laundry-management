namespace LaundryManagement.UI.Forms.Shipping
{
    partial class frmAdministrationShippings
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
            this.btnNewShipping = new System.Windows.Forms.Button();
            this.gridShippings = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridShippings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewShipping
            // 
            this.btnNewShipping.Location = new System.Drawing.Point(48, 362);
            this.btnNewShipping.Name = "btnNewShipping";
            this.btnNewShipping.Size = new System.Drawing.Size(138, 35);
            this.btnNewShipping.TabIndex = 5;
            this.btnNewShipping.Text = "New";
            this.btnNewShipping.UseVisualStyleBackColor = true;
            this.btnNewShipping.Click += new System.EventHandler(this.btnNewShipping_Click);
            // 
            // gridShippings
            // 
            this.gridShippings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShippings.Location = new System.Drawing.Point(48, 47);
            this.gridShippings.Name = "gridShippings";
            this.gridShippings.RowTemplate.Height = 25;
            this.gridShippings.Size = new System.Drawing.Size(588, 272);
            this.gridShippings.TabIndex = 4;
            // 
            // frmAdministrationShippings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 441);
            this.Controls.Add(this.btnNewShipping);
            this.Controls.Add(this.gridShippings);
            this.Name = "frmAdministrationShippings";
            this.Text = "frmAdministrationShippings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdministrationShippings_FormClosing);
            this.Load += new System.EventHandler(this.frmAdministrationShippings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShippings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnNewShipping;
        private System.Windows.Forms.DataGridView gridShippings;
    }
}