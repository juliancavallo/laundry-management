using LaundryManagement.BLL;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Translations
{
    public partial class frmLanguage : Form, ILanguageObserver
    {
        private TranslatorBLL translatorBLL;
        private IList<Control> controls;
        private List<Language> itemsToDelete;
        public frmLanguage()
        {
            translatorBLL = new TranslatorBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave, this.btnAddRow, this.btnDeleteRow};
            itemsToDelete = new List<Language>();

            this.LoadGridData(translatorBLL.GetAllLanguages());

            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.dataGridView1.AllowUserToAddRows = true;
            this.dataGridView1.AllowUserToDeleteRows = true;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Tag = "Language";
            this.btnAddRow.Tag = "AddRow";
            this.btnDeleteRow.Tag = "DeleteRow";
            this.btnSave.Tag = "Save";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LoadGridData(IList<Language> source)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = source;
            this.dataGridView1.Columns["Id"].Visible = false;
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                var newRow = new Language() { Id = 0, Name = "", Default = false };
                var source = this.dataGridView1.DataSource as List<Language>;
                source.Add(newRow);

                this.LoadGridData(source);
                this.dataGridView1.FirstDisplayedScrollingRowIndex = source.Count - 1;
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

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                var source = this.dataGridView1.DataSource as List<Language>;
                var item = this.dataGridView1.CurrentRow.DataBoundItem as Language;

                var validationResult = translatorBLL.ApplyValidationForDelete(item);
                FormValidation.ShowValidationMessages(validationResult);

                itemsToDelete.Add(item);
                source.Remove(item);

                this.LoadGridData(source);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var source = this.dataGridView1.DataSource as List<Language>;
                if(source.Where(x => x.Default).Count() != 1)
                {
                    FormValidation.ShowMessage("There can only be one default language", ValidationType.Warning);
                    return;
                }
                translatorBLL.Save(source);
                translatorBLL.Delete(itemsToDelete);
                this.Close();
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

        #region Language
        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmLanguage_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void frmLanguage_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);
        #endregion

    }
}
