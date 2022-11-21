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

namespace LaundryManagement.UI.Forms.Shipping
{
    public partial class frmShippingReport : Form, ILanguageObserver
    {
        private ShippingTypeEnum shippingType;
        private ShippingBLL shippingBLL;
        private IList<Control> controls;
        public frmShippingReport(ShippingTypeEnum _shippingType)
        {
            shippingType = _shippingType;
            shippingBLL = new ShippingBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnViewDetail, this.btnSearch, 
                this.lblDateFrom, this.lblDateTo, this.btnExport };
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


            this.Tag = "Shippings";
            this.btnViewDetail.Tag = "ViewDetail";
            this.btnSearch.Tag = "Search";
            this.btnExport.Tag = "ExportSelected";
            this.lblDateTo.Tag = "DateTo";
            this.lblDateFrom.Tag = "DateFrom";

            this.dateFrom.Value = DateTime.Now.AddDays(-7);
            this.dateTo.Value = DateTime.Now;
        }

        private void ReloadGridEvent(ShippingFilter filter)
        {
            this.gridShippings.DataSource = null;
            this.gridShippings.DataSource = shippingBLL.GetForView(filter);
            this.gridShippings.Columns["Id"].Visible = false;
        }

        private void frmShippingReport_Load(object sender, EventArgs e)
        {
            try
            {
                var filter = new ShippingFilter();
                filter.ShippingType = shippingType;
                filter.DateFrom = this.dateFrom.Value;
                filter.DateTo = this.dateTo.Value;

                this.ReloadGridEvent(filter);
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

        private void frmShippingReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridShippings);
                var selectedRow = (ShippingViewDTO)this.gridShippings.CurrentRow.DataBoundItem;
                var frm = new frmShippingDetailReport(selectedRow.Id);
                frm.Show();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = new ShippingFilter();
                filter.ShippingType = shippingType;
                filter.DateFrom = this.dateFrom.Value;
                filter.DateTo = this.dateTo.Value;

                this.ReloadGridEvent(filter);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                FormValidation.ValidateGridSelectedRow(this.gridShippings);
                var selectedRow = (ShippingViewDTO)this.gridShippings.CurrentRow.DataBoundItem;

                shippingBLL.Export(selectedRow.Id);

                FormValidation.ShowMessage($"The report has been saved to {Session.Settings.ReportsPath}", ValidationType.Info);

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
