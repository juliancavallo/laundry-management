namespace LaundryManagement.UI.Forms.Translations
{
    partial class frmTranslations
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
            this.lblLanguage = new System.Windows.Forms.Label();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnManageLanguages = new System.Windows.Forms.Button();
            this.lblTranslations = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(71, 13);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(59, 15);
            this.lblLanguage.TabIndex = 15;
            this.lblLanguage.Text = "Language";
            // 
            // comboLanguage
            // 
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.Location = new System.Drawing.Point(71, 31);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Size = new System.Drawing.Size(151, 23);
            this.comboLanguage.TabIndex = 14;
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(54, 97);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(406, 266);
            this.dataGridView1.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(175, 441);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(138, 47);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(73, 387);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(151, 33);
            this.btnAddRow.TabIndex = 18;
            this.btnAddRow.Text = "Add row";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(269, 387);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(151, 33);
            this.btnDeleteRow.TabIndex = 19;
            this.btnDeleteRow.Text = "Delete row";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnManageLanguages
            // 
            this.btnManageLanguages.Location = new System.Drawing.Point(267, 25);
            this.btnManageLanguages.Name = "btnManageLanguages";
            this.btnManageLanguages.Size = new System.Drawing.Size(151, 33);
            this.btnManageLanguages.TabIndex = 20;
            this.btnManageLanguages.Text = "Manage languages";
            this.btnManageLanguages.UseVisualStyleBackColor = true;
            this.btnManageLanguages.Click += new System.EventHandler(this.btnManageLanguages_Click);
            // 
            // lblTranslations
            // 
            this.lblTranslations.AutoSize = true;
            this.lblTranslations.Location = new System.Drawing.Point(73, 79);
            this.lblTranslations.Name = "lblTranslations";
            this.lblTranslations.Size = new System.Drawing.Size(69, 15);
            this.lblTranslations.TabIndex = 21;
            this.lblTranslations.Text = "Translations";
            // 
            // frmTranslations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 503);
            this.Controls.Add(this.lblTranslations);
            this.Controls.Add(this.btnManageLanguages);
            this.Controls.Add(this.btnDeleteRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.comboLanguage);
            this.Name = "frmTranslations";
            this.Text = "frmTranslations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTranslations_FormClosing);
            this.Load += new System.EventHandler(this.frmTranslations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox comboLanguage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.Button btnManageLanguages;
        private System.Windows.Forms.Label lblTranslations;
    }
}