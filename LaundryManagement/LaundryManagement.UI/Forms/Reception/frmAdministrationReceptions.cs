using LaundryManagement.BLL;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Reception
{
    public partial class frmAdministrationReceptions : Form, ILanguageObserver
    {
        private ReceptionBLL receptionBLL;
        private IList<Control> controls;

        public frmAdministrationReceptions()
        {
            receptionBLL = new ReceptionBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnNewReception };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridReceptions.AllowUserToAddRows = false;
            this.gridReceptions.AllowUserToDeleteRows = false;
            this.gridReceptions.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridReceptions.MultiSelect = false;
            this.gridReceptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridReceptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.btnNewReception.TabIndex = 0;

            this.Tag = "Receptions";
            this.btnNewReception.Tag = "Create";

        }

        private void ReloadGridEvent()
        {
            try
            {
                this.gridReceptions.DataSource = null;
                this.gridReceptions.DataSource = receptionBLL.GetAllForView(new ReceptionFilter() { });
                this.gridReceptions.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
        }

        private void btnNewReception_Click(object sender, EventArgs e)
        {
            var frm = new frmReceptionRoadmaps();
            frm.FormClosing += new FormClosingEventHandler((sender, e) => ReloadGridEvent());
            frm.ShowDialog();
        }


        private void frmAdministrationReceptions_Load(object sender, EventArgs e)
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

        private void frmAdministrationReceptions_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);
    }
}
