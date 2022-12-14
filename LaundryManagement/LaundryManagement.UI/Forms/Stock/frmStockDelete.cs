using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Stock
{
    public partial class frmStockDelete : Form, ILanguageObserver
    {
        private ItemBLL itemBLL;
        private IList<Control> controls;
        public frmStockDelete()
        {
            itemBLL = new ItemBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblImportedItems, this.btnImport, this.btnDelete };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grid.MultiSelect = false;
            this.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "StockDelete";
            this.lblImportedItems.Tag = "ImportedItems";
            this.btnImport.Tag = "Import";
            this.btnDelete.Tag = "Delete";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmStockImport_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmStockImport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {

                var fileDialog = new OpenFileDialog();
                fileDialog.DefaultExt = "json";
                fileDialog.Multiselect = false;
                fileDialog.Filter = "Json files (*.json)|*json";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var sr = new StreamReader(fileDialog.FileName);
                    var json = sr.ReadToEnd();
                    var result = itemBLL.ImportStockToDeleteFromJson(json);
                    
                    this.grid.DataSource = null;
                    this.grid.DataSource = result;
                }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var items = (List<ItemViewDTO>)this.grid.DataSource;

                itemBLL.Delete(items.Select(x => x.Code).ToList());

                FormValidation.ShowMessage("The items were deleted successfuly", ValidationType.Info);
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
