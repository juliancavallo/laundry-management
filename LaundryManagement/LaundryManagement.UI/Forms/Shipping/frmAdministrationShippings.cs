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
    public partial class frmAdministrationShippings : Form, ILanguageObserver
    {
        public ShippingTypeEnum shippingType;

        ShippingBLL shippingBLL;
        private IList<Control> controls;

        public frmAdministrationShippings(ShippingTypeEnum _shippingType)
        {
            shippingType = _shippingType;
            shippingBLL = new ShippingBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnNewShipping };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridShippings.AllowUserToAddRows = false;
            this.gridShippings.AllowUserToDeleteRows = false;
            this.gridShippings.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridShippings.MultiSelect = false;
            this.gridShippings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridShippings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.btnNewShipping.TabIndex = 0;

            this.Tag = "Shippings";
            this.btnNewShipping.Tag = "Create";

        }

        private void ReloadGridEvent()
        {
            this.gridShippings.DataSource = null;
            this.gridShippings.DataSource = shippingBLL.GetByTypeForView(shippingType);
            this.gridShippings.Columns["Id"].Visible = false;
        }

        private void btnNewShipping_Click(object sender, EventArgs e)
        {
            var frm = new frmNewShipping(shippingType);
            frm.FormClosing += new FormClosingEventHandler((sender, e) => ReloadGridEvent());
            frm.Show();
        }


        private void frmAdministrationShippings_Load(object sender, EventArgs e)
        {
            try
            {
                this.ReloadGridEvent();
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

        private void frmAdministrationShippings_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);
    }
}
