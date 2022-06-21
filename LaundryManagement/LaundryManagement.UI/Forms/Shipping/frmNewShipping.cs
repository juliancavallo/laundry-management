using LaundryManagement.BLL;
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

namespace LaundryManagement.UI.Forms.Shipping
{
    public partial class frmNewShipping : Form, ILanguageObserver
    {
        public ShippingTypeEnum shippingType;

        private ShippingBLL shippingBLL;
        private LocationBLL locationBLL;
        private IList<Control> controls;

        public frmNewShipping(ShippingTypeEnum _shippingType)
        {
            shippingType = _shippingType;
            shippingBLL = new ShippingBLL();
            locationBLL = new LocationBLL();

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

                this.comboDestination.DataSource = null;
                this.comboDestination.DataSource = locationBLL.GetAllByType(destinationType, shippingType == ShippingTypeEnum.Internal);
                this.comboDestination.DisplayMember = "Name";
                this.comboDestination.ValueMember = "Id";
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
            var locationsSelected = this.comboOrigin.SelectedItem != null || this.comboDestination.SelectedItem != null;
            this.comboOrigin.Enabled = !locationsSelected;
            this.comboDestination.Enabled = !locationsSelected;
            this.btnAdd.Enabled = locationsSelected;
            this.btnRemove.Enabled = locationsSelected;
            this.btnSave.Enabled = locationsSelected;
            this.txtItem.Enabled = locationsSelected;
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmNewShipping_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmNewShipping_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void comboOrigin_SelectedIndexChanged(object sender, EventArgs e) => EnableControls();

        private void comboDestination_SelectedIndexChanged(object sender, EventArgs e) => EnableControls();
    }
}
