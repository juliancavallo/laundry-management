using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
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

namespace LaundryManagement.UI.Forms.Stock
{
    public partial class frmStockReport : Form, ILanguageObserver
    {
        private ItemBLL itemBLL;
        private LocationBLL locationBLL;
        private ItemTypeBLL itemTypeBLL;
        private IList<Control> controls;
        public frmStockReport()
        {
            itemBLL = new ItemBLL();
            locationBLL = new LocationBLL();
            itemTypeBLL = new ItemTypeBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblItemCode, this.btnSearch, lblItemType, lblLocation, lblStatus };
            Translate();
            PopulateCombos();
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

            this.Tag = "Stock";
            this.lblItemCode.Tag = "ItemCode";
            this.btnSearch.Tag = "Search";
            this.lblStatus.Tag = "Status";
            this.lblLocation.Tag = "Location";
            this.lblItemType.Tag = "ItemType";

            this.comboItemType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboItemStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboItemLocation.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PopulateCombos()
        {
            try
            {
                var locationSource = locationBLL.GetAll();
                locationSource.Add(new LocationDTO() { Id = 0, CompleteName = Session.Translations["All"].Text });
                this.comboItemLocation.DataSource = null;
                this.comboItemLocation.DataSource = locationSource.OrderBy(x => x.Id).ToList();
                this.comboItemLocation.DisplayMember = "CompleteName";
                this.comboItemLocation.ValueMember = "Id";

                var itemTypeSource = itemTypeBLL.GetAll();
                itemTypeSource.Add(new ItemTypeDTO() { Id = 0, Name = Session.Translations["All"].Text });
                this.comboItemType.DataSource = null;
                this.comboItemType.DataSource = itemTypeSource.OrderBy(x => x.Id).ToList();
                this.comboItemType.DisplayMember = "Name";
                this.comboItemType.ValueMember = "Id";

                var itemStatusSource = itemBLL.GetAllItemStatus();
                itemStatusSource.Add(new ItemStatusDTO() { Id = 0, Name = Session.Translations["All"].Text });
                this.comboItemStatus.DataSource = null;
                this.comboItemStatus.DataSource = itemStatusSource.OrderBy(x => x.Id).ToList();
                this.comboItemStatus.DisplayMember = "Name";
                this.comboItemStatus.ValueMember = "Id";
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

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmStockReport_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmStockReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = new ItemFilter();
                filter.Code = this.txtCode.Text;
                filter.ItemType = (ItemTypeDTO)this.comboItemType.SelectedItem;
                filter.ItemStatus = (ItemStatusEnum?)(int?)this.comboItemStatus.SelectedValue;
                filter.LocationDTO = (LocationDTO)this.comboItemLocation.SelectedItem;

                this.grid.DataSource = null;
                this.grid.DataSource = itemBLL.GetByFilter(filter);
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
