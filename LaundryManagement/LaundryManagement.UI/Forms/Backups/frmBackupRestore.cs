using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Backups
{
    public partial class frmBackupRestore : Form, ILanguageObserver
    {
        private IList<Control> controls;
        private readonly BackupRestoreBLL backupRestoreBLL;
        public frmBackupRestore()
        {
            InitializeComponent();
            ApplySetup();

            backupRestoreBLL = new BackupRestoreBLL();
            controls = new List<Control>() { this, this.btnBackup, this.btnRestore };

            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "Backups";
            this.btnBackup.Tag = "Backup";
            this.btnRestore.Tag = "Restore";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        #region Language
        public void UpdateLanguage(ILanguage language) => Translate(language);

        private void Translate(ILanguage language = null) => FormValidation.Translate(Session.Translations, controls);

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        #endregion

        private void ReloadGrid()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = backupRestoreBLL.GetBackups().ToList();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                backupRestoreBLL.Backup();
                ReloadGrid();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.dataGridView1);

                var selectedRow = (BackupDTO)this.dataGridView1.CurrentRow.DataBoundItem;

                backupRestoreBLL.Restore(selectedRow.BackupPath);

                FormValidation.ShowMessage($"Backup {selectedRow.BackupPath} succesfully restored", ValidationType.Info);
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            try
            {
                ReloadGrid();
                Session.SubscribeObserver(this);
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }
    }
}
