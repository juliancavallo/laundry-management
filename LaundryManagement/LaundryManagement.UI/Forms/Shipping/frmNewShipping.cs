using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Extensions;
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

namespace LaundryManagement.UI.Forms.Shipping
{
    public partial class frmNewShipping : Form, ILanguageObserver
    {
        public ShippingTypeEnum shippingType;

        private ShippingBLL shippingBLL;
        private LocationBLL locationBLL;
        private ItemBLL itemBLL;
        private List<Control> controls;

        private List<ShippingDetailDTO> shippingDetailDTO;

        public frmNewShipping(ShippingTypeEnum _shippingType)
        {
            shippingType = _shippingType;
            shippingBLL = new ShippingBLL();
            locationBLL = new LocationBLL();
            itemBLL = new ItemBLL();    
            shippingDetailDTO = new List<ShippingDetailDTO>();


            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblDestination, this.lblOrigin, this.lblItem, this.btnAdd, this.btnRemove, this.btnSave };
            Translate();
            PopulateComboLocation();
            EnableControls();
        }


        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridItems.AllowUserToAddRows = false;
            this.gridItems.AllowUserToDeleteRows = false;
            this.gridItems.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridItems.MultiSelect = false;
            this.gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.comboOrigin.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboDestination.DropDownStyle = ComboBoxStyle.DropDownList;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "Shipping";
            this.btnAdd.Tag = "AddRow";
            this.btnRemove.Tag = "DeleteRow";
            this.btnSave.Tag = "Save";
            this.lblDestination.Tag = "Destination";
            this.lblOrigin.Tag = "Origin";
            this.lblItem.Tag = "Item";

            this.gridItems.DataSource = null;
        }

        private void PopulateComboLocation()
        {
            try
            {
                var originType = locationBLL.GetByShippingType(shippingType, true);
                var destinationType = locationBLL.GetByShippingType(shippingType, false);

                this.comboOrigin.DataSource = null;
                this.comboOrigin.DataSource = locationBLL.GetAllByType(originType, shippingType == ShippingTypeEnum.Internal);
                this.comboOrigin.DisplayMember = "Name";
                this.comboOrigin.ValueMember = "Id";
                this.comboOrigin.SelectedIndex = -1;

                this.comboDestination.DataSource = null;
                this.comboDestination.DataSource = locationBLL.GetAllByType(destinationType, shippingType == ShippingTypeEnum.Internal);
                this.comboDestination.DisplayMember = "Name";
                this.comboDestination.ValueMember = "Id";
                this.comboDestination.SelectedIndex = -1;
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

        private void EnableControls()
        {
            var locationsSelected = this.comboOrigin.SelectedItem != null && this.comboDestination.SelectedItem != null;
            this.comboOrigin.Enabled = !locationsSelected;
            this.comboDestination.Enabled = !locationsSelected;
            this.btnAdd.Enabled = locationsSelected;
            this.btnRemove.Enabled = locationsSelected;
            this.btnSave.Enabled = locationsSelected;
            this.txtItem.Enabled = locationsSelected;

            if(locationsSelected)
                this.txtItem.Focus();
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void AddItem()
        {
            try
            {
                var code = txtItem.Text;
                if (string.IsNullOrWhiteSpace(code))
                    return;

                var item = itemBLL.GetByCode(code);

                shippingDetailDTO.Add(new ShippingDetailDTO()
                {
                    Item = item,
                });

                LoadGrid();
                this.txtItem.Clear();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
            }
        }

        private void LoadGrid()
        {
            this.gridItems.DataSource = null;
            this.gridItems.DataSource = shippingBLL.MapToView(shippingDetailDTO);
            this.gridItems.Columns["ArticleId"].Visible = false;
        }

        private void frmNewShipping_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmNewShipping_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void comboOrigin_SelectedIndexChanged(object sender, EventArgs e) => EnableControls();

        private void comboDestination_SelectedIndexChanged(object sender, EventArgs e) => EnableControls();

        private void txtItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddItem();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridItems);
                var selectedItem = (ShippingDetailViewDTO)this.gridItems.CurrentRow.DataBoundItem;

                shippingDetailDTO.RemoveAll(x => x.Item.Article.Id == selectedItem.ArticleId);

                LoadGrid();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) => AddItem();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (shippingDetailDTO.Count == 0)
                {
                    FormValidation.ShowMessage("There are no items to save", ValidationType.Warning);
                    return;
                }

                var shipping = new ShippingDTO();
                shipping.Origin = (LocationDTO)this.comboOrigin.SelectedItem;
                shipping.Destination = (LocationDTO)this.comboDestination.SelectedItem;
                shipping.ShippingDetail = shippingDetailDTO;
                shipping.CreatedDate = DateTime.Now;
                shipping.Status = ShippingStatusEnum.Created;
                shipping.Type = shippingType;

                shippingBLL.Save(shipping);
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
            } 
        }
    }
}
