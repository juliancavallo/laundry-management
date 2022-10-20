namespace LaundryManagement.UI.Forms.Logs
{
    partial class frmLogs
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.comboMovementType = new System.Windows.Forms.ComboBox();
            this.lblMovementType = new System.Windows.Forms.Label();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessageView = new System.Windows.Forms.TextBox();
            this.comboLevel = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.comboUser = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(22, 199);
            this.grid.Name = "grid";
            this.grid.RowTemplate.Height = 25;
            this.grid.Size = new System.Drawing.Size(691, 265);
            this.grid.TabIndex = 4;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(24, 148);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(152, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comboMovementType
            // 
            this.comboMovementType.FormattingEnabled = true;
            this.comboMovementType.Location = new System.Drawing.Point(24, 40);
            this.comboMovementType.Name = "comboMovementType";
            this.comboMovementType.Size = new System.Drawing.Size(185, 23);
            this.comboMovementType.TabIndex = 6;
            // 
            // lblMovementType
            // 
            this.lblMovementType.AutoSize = true;
            this.lblMovementType.Location = new System.Drawing.Point(22, 15);
            this.lblMovementType.Name = "lblMovementType";
            this.lblMovementType.Size = new System.Drawing.Size(38, 15);
            this.lblMovementType.TabIndex = 7;
            this.lblMovementType.Text = "label1";
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Location = new System.Drawing.Point(269, 40);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 23);
            this.dateTimeFrom.TabIndex = 8;
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Location = new System.Drawing.Point(513, 40);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(200, 23);
            this.dateTimeTo.TabIndex = 9;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(513, 19);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(38, 15);
            this.lblDateTo.TabIndex = 10;
            this.lblDateTo.Text = "label1";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(269, 22);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(38, 15);
            this.lblDateFrom.TabIndex = 11;
            this.lblDateFrom.Text = "label1";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(24, 104);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(185, 23);
            this.txtMessage.TabIndex = 12;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(24, 80);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(38, 15);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "label1";
            // 
            // txtMessageView
            // 
            this.txtMessageView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMessageView.Location = new System.Drawing.Point(24, 482);
            this.txtMessageView.Name = "txtMessageView";
            this.txtMessageView.Size = new System.Drawing.Size(689, 23);
            this.txtMessageView.TabIndex = 14;
            // 
            // comboLevel
            // 
            this.comboLevel.FormattingEnabled = true;
            this.comboLevel.Location = new System.Drawing.Point(269, 104);
            this.comboLevel.Name = "comboLevel";
            this.comboLevel.Size = new System.Drawing.Size(200, 23);
            this.comboLevel.TabIndex = 15;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(269, 86);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(38, 15);
            this.lblLevel.TabIndex = 16;
            this.lblLevel.Text = "label1";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(513, 86);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(38, 15);
            this.lblUser.TabIndex = 18;
            this.lblUser.Text = "label1";
            // 
            // comboUser
            // 
            this.comboUser.FormattingEnabled = true;
            this.comboUser.Location = new System.Drawing.Point(513, 104);
            this.comboUser.Name = "comboUser";
            this.comboUser.Size = new System.Drawing.Size(200, 23);
            this.comboUser.TabIndex = 17;
            // 
            // frmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 533);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.comboUser);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.comboLevel);
            this.Controls.Add(this.txtMessageView);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dateTimeTo);
            this.Controls.Add(this.dateTimeFrom);
            this.Controls.Add(this.lblMovementType);
            this.Controls.Add(this.comboMovementType);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grid);
            this.Name = "frmLogs";
            this.Text = "frmLogs";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox comboMovementType;
        private System.Windows.Forms.Label lblMovementType;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessageView;
        private System.Windows.Forms.ComboBox comboLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox comboUser;
    }
}