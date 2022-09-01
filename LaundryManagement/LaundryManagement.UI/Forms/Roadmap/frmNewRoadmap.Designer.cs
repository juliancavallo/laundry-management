namespace LaundryManagement.UI.Forms.Roadmap
{
    partial class frmNewRoadmap
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
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.comboDestination = new System.Windows.Forms.ComboBox();
            this.comboOrigin = new System.Windows.Forms.ComboBox();
            this.btnSearchShippings = new System.Windows.Forms.Button();
            this.gridShippings = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridShippings)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(302, 35);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(38, 15);
            this.lblDestination.TabIndex = 7;
            this.lblDestination.Text = "label1";
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(34, 35);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(38, 15);
            this.lblOrigin.TabIndex = 6;
            this.lblOrigin.Text = "label1";
            // 
            // comboDestination
            // 
            this.comboDestination.FormattingEnabled = true;
            this.comboDestination.Location = new System.Drawing.Point(302, 53);
            this.comboDestination.Name = "comboDestination";
            this.comboDestination.Size = new System.Drawing.Size(227, 23);
            this.comboDestination.TabIndex = 5;
            // 
            // comboOrigin
            // 
            this.comboOrigin.FormattingEnabled = true;
            this.comboOrigin.Location = new System.Drawing.Point(34, 53);
            this.comboOrigin.Name = "comboOrigin";
            this.comboOrigin.Size = new System.Drawing.Size(227, 23);
            this.comboOrigin.TabIndex = 4;
            // 
            // btnSearchShippings
            // 
            this.btnSearchShippings.Location = new System.Drawing.Point(34, 101);
            this.btnSearchShippings.Name = "btnSearchShippings";
            this.btnSearchShippings.Size = new System.Drawing.Size(108, 34);
            this.btnSearchShippings.TabIndex = 8;
            this.btnSearchShippings.Text = "button1";
            this.btnSearchShippings.UseVisualStyleBackColor = true;
            this.btnSearchShippings.Click += new System.EventHandler(this.btnSearchShippings_Click);
            // 
            // gridShippings
            // 
            this.gridShippings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShippings.Location = new System.Drawing.Point(34, 162);
            this.gridShippings.Name = "gridShippings";
            this.gridShippings.RowTemplate.Height = 25;
            this.gridShippings.Size = new System.Drawing.Size(495, 190);
            this.gridShippings.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 371);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 34);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "button1";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmNewRoadmap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 429);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridShippings);
            this.Controls.Add(this.btnSearchShippings);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.comboDestination);
            this.Controls.Add(this.comboOrigin);
            this.Name = "frmNewRoadmap";
            this.Text = "frmNewRoadmap";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewRoadmap_FormClosing);
            this.Load += new System.EventHandler(this.frmNewRoadmap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShippings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.ComboBox comboDestination;
        private System.Windows.Forms.ComboBox comboOrigin;
        private System.Windows.Forms.Button btnSearchShippings;
        private System.Windows.Forms.DataGridView gridShippings;
        private System.Windows.Forms.Button btnSave;
    }
}