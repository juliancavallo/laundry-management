namespace LaundryManagement.UI.Forms.Reception
{
    partial class frmReceptionRoadmaps
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
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.comboOrigin = new System.Windows.Forms.ComboBox();
            this.gridRoadmaps = new System.Windows.Forms.DataGridView();
            this.btnSearchRoadmaps = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmaps)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(247, 42);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(177, 23);
            this.txtDestination.TabIndex = 0;
            // 
            // comboOrigin
            // 
            this.comboOrigin.FormattingEnabled = true;
            this.comboOrigin.Location = new System.Drawing.Point(40, 42);
            this.comboOrigin.Name = "comboOrigin";
            this.comboOrigin.Size = new System.Drawing.Size(177, 23);
            this.comboOrigin.TabIndex = 1;
            // 
            // gridRoadmaps
            // 
            this.gridRoadmaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRoadmaps.Location = new System.Drawing.Point(31, 135);
            this.gridRoadmaps.Name = "gridRoadmaps";
            this.gridRoadmaps.RowTemplate.Height = 25;
            this.gridRoadmaps.Size = new System.Drawing.Size(388, 200);
            this.gridRoadmaps.TabIndex = 2;
            // 
            // btnSearchRoadmaps
            // 
            this.btnSearchRoadmaps.Location = new System.Drawing.Point(158, 87);
            this.btnSearchRoadmaps.Name = "btnSearchRoadmaps";
            this.btnSearchRoadmaps.Size = new System.Drawing.Size(127, 32);
            this.btnSearchRoadmaps.TabIndex = 3;
            this.btnSearchRoadmaps.Text = "button1";
            this.btnSearchRoadmaps.UseVisualStyleBackColor = true;
            this.btnSearchRoadmaps.Click += new System.EventHandler(this.btnSearchRoadmaps_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(158, 362);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(127, 32);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "button1";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(31, 24);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(53, 15);
            this.lblOrigin.TabIndex = 12;
            this.lblOrigin.Text = "lblOrigin";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(247, 24);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(80, 15);
            this.lblDestination.TabIndex = 13;
            this.lblDestination.Text = "lblDestination";
            // 
            // frmReceptionRoadmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 418);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnSearchRoadmaps);
            this.Controls.Add(this.gridRoadmaps);
            this.Controls.Add(this.comboOrigin);
            this.Controls.Add(this.txtDestination);
            this.Name = "frmReceptionRoadmaps";
            this.Text = "frmReceptionRoadmaps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReceptionRoadmaps_FormClosing);
            this.Load += new System.EventHandler(this.frmReceptionRoadmaps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoadmaps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.ComboBox comboOrigin;
        private System.Windows.Forms.DataGridView gridRoadmaps;
        private System.Windows.Forms.Button btnSearchRoadmaps;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.Label lblDestination;
    }
}