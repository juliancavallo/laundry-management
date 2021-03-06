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

namespace LaundryManagement.UI.Forms.Traceability
{
    public partial class frmTraceabilityReport : Form, ILanguageObserver
    {
        private TraceabilityBLL traceabilityBLL;
        private IList<Control> controls;
        public frmTraceabilityReport()
        {
            traceabilityBLL = new TraceabilityBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblItemCode, this.btnSearch };
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

            this.Tag = "Traceability";
            this.lblItemCode.Tag = "ItemCode";
            this.btnSearch.Tag = "Search";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmTraceabilityReport_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmTraceabilityReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void ReloadGridEvent(string code)
        {
            this.grid.DataSource = null;
            this.grid.DataSource = traceabilityBLL.GetForView(code);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>() { this.txtCode });

                this.ReloadGridEvent(txtCode.Text);
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
