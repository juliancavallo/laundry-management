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

namespace LaundryManagement.UI.Forms.Roadmap
{
    public partial class frmRoadmapDetailReport : Form, ILanguageObserver
    {
        private RoadmapBLL roadmapBLL;
        private IList<Control> controls;
        private int idRoadmap;
        public frmRoadmapDetailReport(int _idRoadmap)
        {
            idRoadmap = _idRoadmap;
            roadmapBLL = new RoadmapBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this };
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridRoadmapDetail.AllowUserToAddRows = false;
            this.gridRoadmapDetail.AllowUserToDeleteRows = false;
            this.gridRoadmapDetail.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridRoadmapDetail.MultiSelect = false;
            this.gridRoadmapDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRoadmapDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "RoadmapDetail";
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmRoadmapDetailReport_Load(object sender, EventArgs e)
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

        private void frmRoadmapDetailReport_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void ReloadGridEvent()
        {
            this.gridRoadmapDetail.DataSource = null;
            this.gridRoadmapDetail.DataSource = roadmapBLL.GetDetailForView(idRoadmap);
            this.gridRoadmapDetail.Columns["ArticleId"].Visible = false;
        }

    }
}
