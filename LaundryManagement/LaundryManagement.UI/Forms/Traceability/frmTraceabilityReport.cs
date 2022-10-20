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

namespace LaundryManagement.UI.Forms.Traceability
{
    public partial class frmTraceabilityReport : Form, ILanguageObserver
    {
        private TraceabilityBLL traceabilityBLL;
        private MovementTypeBLL movementTypeBLL;
        private ItemBLL itemBLL;
        private IList<Control> controls;
        public frmTraceabilityReport()
        {
            traceabilityBLL = new TraceabilityBLL();
            movementTypeBLL = new MovementTypeBLL();
            itemBLL = new ItemBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblItemCode, this.btnSearch, this.lblMovementType, this.lblItemStatus };
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
            this.comboMovementType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboItemStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Tag = "Traceability";
            this.lblItemCode.Tag = "ItemCode";
            this.btnSearch.Tag = "Search";
            this.lblItemStatus.Tag = "ItemStatus";
            this.lblMovementType.Tag = "Movement";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmTraceabilityReport_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmTraceabilityReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void PopulateCombos()
        {
            var allOption = new EnumTypeDTO() { Id = 0, Name = Session.Translations["All"] };

            var movementSource = movementTypeBLL.GetAll();
            movementSource.Add(allOption);
            this.comboMovementType.DataSource = null;
            this.comboMovementType.DataSource = movementSource.OrderBy(x => x.Id).ToList();
            this.comboMovementType.DisplayMember = "Name";
            this.comboMovementType.ValueMember = "Id";

            var itemStatusSource = itemBLL.GetAllItemStatus();
            itemStatusSource.Add(new EnumTypeDTO() { Id = 0, Name = Session.Translations["All"] });
            this.comboItemStatus.DataSource = null;
            this.comboItemStatus.DataSource = itemStatusSource.OrderBy(x => x.Id).ToList();
            this.comboItemStatus.DisplayMember = "Name";
            this.comboItemStatus.ValueMember = "Id";
        }

        private void ReloadGridEvent(TraceabilityFilter filter)
        {
            this.grid.DataSource = null;
            this.grid.DataSource = traceabilityBLL.GetForView(filter);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>() { this.txtCode });

                var filter = new TraceabilityFilter()
                {
                    Code = this.txtCode.Text,
                    MovementType = (MovementTypeEnum?)(int?)this.comboMovementType.SelectedValue,
                    ItemStatus = (ItemStatusEnum?)(int?)this.comboItemStatus.SelectedValue
                };

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
    }
}
