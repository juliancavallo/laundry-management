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

namespace LaundryManagement.UI.Forms.Reception
{
    public partial class frmReceptionDetailReport : Form, ILanguageObserver
    {
        private ReceptionBLL receptionBLL;
        private IList<Control> controls;
        private int idReception;
        public frmReceptionDetailReport(int _idReception)
        {
            idReception = _idReception;
            receptionBLL = new ReceptionBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridReceptionDetail.AllowUserToAddRows = false;
            this.gridReceptionDetail.AllowUserToDeleteRows = false;
            this.gridReceptionDetail.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridReceptionDetail.MultiSelect = false;
            this.gridReceptionDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridReceptionDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "ReceptionDetail";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmReceptionDetailReport_Load(object sender, EventArgs e)
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

        private void frmReceptionDetailReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void ReloadGridEvent()
        {
            this.gridReceptionDetail.DataSource = null;
            this.gridReceptionDetail.DataSource = receptionBLL.GetDetailForView(idReception);
            this.gridReceptionDetail.Columns["ArticleId"].Visible = false;
        }
    }
}
