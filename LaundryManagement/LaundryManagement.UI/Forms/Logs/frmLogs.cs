using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Logs
{
    public partial class frmLogs : Form, ILanguageObserver
    {
        private LogBLL logBLL;
        private MovementTypeBLL movementTypeBLL;
        private IList<Control> controls;
        public frmLogs()
        {
            logBLL = new LogBLL();
            movementTypeBLL = new MovementTypeBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSearch, this.lblDateFrom, this.lblDateTo, this.lblMessage, this.lblMovementType };
            Translate();

            PopulateComboMovmentTypes();
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
            this.comboMovementType.DropDownStyle = ComboBoxStyle.DropDownList;

            this.dateTimeFrom.Value = DateTime.Now.AddDays(-7);
            this.dateTimeTo.Value = DateTime.Now;
            this.txtMessageView.Enabled = false;
            this.txtMessageView.Multiline = true;

            this.Tag = "Logs";
            this.btnSearch.Tag = "Search";
            this.lblDateTo.Tag = "DateTo";
            this.lblDateFrom.Tag = "DateFrom";
            this.lblMessage.Tag = "Message";
            this.lblMovementType.Tag = "Movement";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmTraceabilityReport_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmTraceabilityReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void PopulateComboMovmentTypes()
        {
            var source  = movementTypeBLL.GetAll();
            source.Add(new EnumTypeDTO() { Id = 0, Name = Session.Translations["All"] });
            
            this.comboMovementType.DataSource = null;
            this.comboMovementType.DataSource = source.OrderBy(x => x.Id).ToList();
            this.comboMovementType.DisplayMember = "Name";
            this.comboMovementType.ValueMember = "Id";
        }

        private void ReloadGridEvent(LogFilter filter)
        {
            this.grid.DataSource = null;
            this.grid.DataSource = logBLL.GetForView(filter);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = new LogFilter();
                filter.DateFrom = this.dateTimeFrom.Value;
                filter.DateTo = this.dateTimeTo.Value;
                filter.Message = this.txtMessage.Text;
                filter.MovementType = (MovementTypeEnum?)(int?)this.comboMovementType.SelectedValue;
                
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

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            var row = this.grid.CurrentRow.DataBoundItem as LogViewDTO;
            this.txtMessageView.Text = row.Message;
        }
    }
}
