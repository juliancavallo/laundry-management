using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Translations
{
    public partial class frmTranslations : Form, ILanguageObserver
    {
        private TranslatorBLL translatorBLL;
        private IList<Control> controls;
        private IDictionary<int, List<TranslationViewDTO>> itemsToDelete;
        private IDictionary<int, List<TranslationViewDTO>> itemsToUpdate;

        public frmTranslations()
        {
            translatorBLL = new TranslatorBLL();
            itemsToUpdate = translatorBLL.GetAllTranslationsByLanguage();
            itemsToDelete = new Dictionary<int, List<TranslationViewDTO>>();

            InitializeComponent();
            PopulateComboLanguages();
            ApplySetup();

            controls = new List<Control>() { this, this.lblLanguage, this.lblTranslations, this.btnAddRow, this.btnDeleteRow, this.btnSave, this.btnManageLanguages};
            

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
            this.Tag = "Translations";
            this.lblLanguage.Tag = "Language";
            this.lblTranslations.Tag = "Translations";
            this.btnAddRow.Tag = "AddRow";
            this.btnDeleteRow.Tag = "DeleteRow";
            this.btnSave.Tag = "Save";
            this.btnManageLanguages.Tag = "ManageLanguages";

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            try
            {
                var defaultLanguage = translatorBLL.GetDefaultLanguage();
                this.comboLanguage.DataSource = null;
                this.comboLanguage.DataSource = translatorBLL.GetAllLanguages();
                this.comboLanguage.DisplayMember = "Name";
                this.comboLanguage.ValueMember = "Id";

                this.comboLanguage.SelectedValue = defaultLanguage.Id;
                this.LoadGridData(itemsToUpdate[defaultLanguage.Id]);
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

        private void frmTranslations_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void frmTranslations_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = ((ComboBox)sender).SelectedItem as Language;
            this.LoadGridData(itemsToUpdate[selectedItem.Id]);
        }
        #endregion

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedLanguage = this.comboLanguage.SelectedItem as Language;
                var newRow = new TranslationViewDTO()
                {
                    IdTranslation = 0,
                    IdTag = 0,
                    Tag = "",
                    Description = "",
                };

                itemsToUpdate[selectedLanguage.Id].Add(newRow);
                
                this.LoadGridData(itemsToUpdate[selectedLanguage.Id]);
                this.dataGridView1.FirstDisplayedScrollingRowIndex = itemsToUpdate[selectedLanguage.Id].Count - 1;
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
                var selectedLanguage = this.comboLanguage.SelectedItem as Language;
                var item = this.dataGridView1.CurrentRow.DataBoundItem as TranslationViewDTO;
                
                if(!itemsToDelete.ContainsKey(selectedLanguage.Id))
                    itemsToDelete.Add(selectedLanguage.Id, new List<TranslationViewDTO>());

                itemsToDelete[selectedLanguage.Id].Add(item);
                itemsToUpdate[selectedLanguage.Id].Remove(item);

                this.LoadGridData(itemsToUpdate[selectedLanguage.Id]);
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
                foreach(var list in itemsToUpdate)
                {
                    translatorBLL.Save(list.Value, list.Key);
                    
                    if(itemsToDelete.ContainsKey(list.Key))
                        translatorBLL.Delete(itemsToDelete[list.Key], list.Key);
                }

                Session.SetTranslations(translatorBLL.GetTranslations(Session.Instance.User.Language as Language));
                Session.ChangeLanguage(Session.Instance.User.Language);

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

        private void btnManageLanguages_Click(object sender, EventArgs e)
        {
            var frm = new frmLanguage();
            frm.Show();
            frm.FormClosing += new FormClosingEventHandler((sender, e) => PopulateComboLanguages());
        }
    }
}
