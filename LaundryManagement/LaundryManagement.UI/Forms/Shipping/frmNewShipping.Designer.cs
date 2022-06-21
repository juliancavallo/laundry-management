namespace LaundryManagement.UI.Forms.Shipping
{
    partial class frmNewShipping
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
            this.comboOrigin = new System.Windows.Forms.ComboBox();
            this.comboDestination = new System.Windows.Forms.ComboBox();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // comboOrigin
            // 
            this.comboOrigin.FormattingEnabled = true;
            this.comboOrigin.Location = new System.Drawing.Point(37, 46);
            this.comboOrigin.Name = "comboOrigin";
            this.comboOrigin.Size = new System.Drawing.Size(227, 23);
            this.comboOrigin.TabIndex = 0;
            this.comboOrigin.SelectedIndexChanged += new System.EventHandler(this.comboOrigin_SelectedIndexChanged);
            // 
            // comboDestination
            // 
            this.comboDestination.FormattingEnabled = true;
            this.comboDestination.Location = new System.Drawing.Point(305, 46);
            this.comboDestination.Name = "comboDestination";
            this.comboDestination.Size = new System.Drawing.Size(227, 23);
            this.comboDestination.TabIndex = 1;
            this.comboDestination.SelectedIndexChanged += new System.EventHandler(this.comboDestination_SelectedIndexChanged);
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(37, 28);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(38, 15);
            this.lblOrigin.TabIndex = 2;
            this.lblOrigin.Text = "label1";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(305, 28);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(38, 15);
            this.lblDestination.TabIndex = 3;
            this.lblDestination.Text = "label1";
            // 
            // gridItems
            // 
            this.gridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItems.Location = new System.Drawing.Point(37, 162);
            this.gridItems.Name = "gridItems";
            this.gridItems.RowTemplate.Height = 25;
            this.gridItems.Size = new System.Drawing.Size(495, 252);
            this.gridItems.TabIndex = 4;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(40, 120);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(224, 23);
            this.txtItem.TabIndex = 5;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(40, 102);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(38, 15);
            this.lblItem.TabIndex = 6;
            this.lblItem.Text = "label1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(303, 123);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "button1";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(422, 123);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(110, 23);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "button2";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(204, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 37);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "button1";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmNewShipping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 490);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.gridItems);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.comboDestination);
            this.Controls.Add(this.comboOrigin);
            this.Name = "frmNewShipping";
            this.Text = "frmNewShipping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewShipping_FormClosing);
            this.Load += new System.EventHandler(this.frmNewShipping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboOrigin;
        private System.Windows.Forms.ComboBox comboDestination;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
    }
}