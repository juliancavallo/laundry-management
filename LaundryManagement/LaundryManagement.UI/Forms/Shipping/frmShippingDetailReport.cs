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
    public partial class frmShippingDetailReport : Form, ILanguageObserver
    {
        private ShippingBLL shippingBLL;
        private IList<Control> controls;
        private int idShipping;
        public frmShippingDetailReport(int _idShipping)
        {
            idShipping = _idShipping;
            shippingBLL = new ShippingBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridShippingDetail.AllowUserToAddRows = false;
            this.gridShippingDetail.AllowUserToDeleteRows = false;
            this.gridShippingDetail.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridShippingDetail.MultiSelect = false;
            this.gridShippingDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridShippingDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "ShippingDetail";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmShippingDetailReport_Load(object sender, EventArgs e)
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

        private void frmShippingDetailReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void ReloadGridEvent()
        {
            this.gridShippingDetail.DataSource = null;
            this.gridShippingDetail.DataSource = shippingBLL.GetDetailForView(idShipping);
            this.gridShippingDetail.Columns["ArticleId"].Visible = false;
        }
    }
}
