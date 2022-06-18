using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
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
    public partial class frmTranslations : Form, ILanguageObserver
    {
        private TranslatorBLL translatorBLL;
        private IList<Control> controls;
        private List<TranslationViewDTO> itemsToDelete;

        public frmTranslations()
        {
            translatorBLL = new TranslatorBLL();

            InitializeComponent();
            PopulateComboLanguages();
            ApplySetup();

            controls = new List<Control>() { this, this.lblLanguage};
            itemsToDelete = new List<TranslationViewDTO>();

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
            
            this.comboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Tag = "Manage";
            this.lblLanguage.Tag = "Language";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void frmTranslations_Load(object sender, EventArgs e)
        {
            Session.SubsribeObserver(this);
        }

        private void LoadGridData(IList<TranslationViewDTO> source)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = source;
            this.dataGridView1.Columns["IdTag"].Visible = false;
            this.dataGridView1.Columns["IdTranslation"].Visible = false;
        }

        #region Language
        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void PopulateComboLanguages()
        {
            var defaultLanguage = translatorBLL.GetDefaultLanguage();
            this.comboLanguage.DataSource = null;
            this.comboLanguage.DataSource = translatorBLL.GetAllLanguages();
            this.comboLanguage.DisplayMember = "Name";
            this.comboLanguage.ValueMember = "Id";

            this.comboLanguage.SelectedValue = defaultLanguage.Id;
            this.LoadGridData(translatorBLL.GetTranslationsForView(defaultLanguage));
        }

        private void frmTranslations_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubsribeObserver(this);

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as Language;
            this.LoadGridData(translatorBLL.GetTranslationsForView(selectedItem));
        }
        #endregion

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            var newRow = new TranslationViewDTO()
            {
                IdTranslation = 0,
                IdTag = 0,
                Tag = "",
                Description = "",
            };
            var source = this.dataGridView1.DataSource as List<TranslationViewDTO>;
            source.Add(newRow);

            this.LoadGridData(source);
            this.dataGridView1.FirstDisplayedScrollingRowIndex = source.Count - 1;
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            var source = this.dataGridView1.DataSource as List<TranslationViewDTO>;
            var item = this.dataGridView1.CurrentRow.DataBoundItem as TranslationViewDTO;
            itemsToDelete.Add(item);
            source.Remove(item);

            this.LoadGridData(source);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var source = this.dataGridView1.DataSource as List<TranslationViewDTO>;
            var selectedItem = this.comboLanguage.SelectedItem as Language;
            translatorBLL.Save(source, selectedItem.Id);
            translatorBLL.Delete(itemsToDelete, selectedItem.Id);

            Session.SetTranslations(translatorBLL.GetTranslations(Session.Instance.User.Language as Language));
            Session.ChangeLanguage(Session.Instance.User.Language);

            this.Close();
        }
    }
}
