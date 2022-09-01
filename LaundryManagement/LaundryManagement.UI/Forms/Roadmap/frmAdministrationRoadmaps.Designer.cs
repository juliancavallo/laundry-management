namespace LaundryManagement.UI.Forms.Roadmap
{
    partial class frmAdministrationRoadmaps
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
            this.btnNewRoadmap = new System.Windows.Forms.Button();
            this.gridRoadmap = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmap)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewRoadmap
            // 
            this.btnNewRoadmap.Location = new System.Drawing.Point(34, 347);
            this.btnNewRoadmap.Name = "btnNewRoadmap";
            this.btnNewRoadmap.Size = new System.Drawing.Size(138, 35);
            this.btnNewRoadmap.TabIndex = 7;
            this.btnNewRoadmap.Text = "New";
            this.btnNewRoadmap.UseVisualStyleBackColor = true;
            this.btnNewRoadmap.Click += new System.EventHandler(this.btnNewRoadmap_Click);
            // 
            // gridRoadmap
            // 
            this.gridRoadmap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRoadmap.Location = new System.Drawing.Point(34, 32);
            this.gridRoadmap.Name = "gridRoadmap";
            this.gridRoadmap.RowTemplate.Height = 25;
            this.gridRoadmap.Size = new System.Drawing.Size(588, 272);
            this.gridRoadmap.TabIndex = 6;
            // 
            // frmAdministrationRoadmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 411);
            this.Controls.Add(this.btnNewRoadmap);
            this.Controls.Add(this.gridRoadmap);
            this.Name = "frmAdministrationRoadmaps";
            this.Text = "frmAdministrationRoadmaps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdministrationRoadmaps_FormClosing);
            this.Load += new System.EventHandler(this.frmAdministrationRoadmaps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewRoadmap;
        private System.Windows.Forms.DataGridView gridRoadmap;
    }
}