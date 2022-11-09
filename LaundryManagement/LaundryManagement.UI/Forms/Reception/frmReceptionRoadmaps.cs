using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI.Forms.Reception
{
    public partial class frmReceptionRoadmaps : Form, ILanguageObserver
    {
        private RoadmapBLL roadmapBLL;
        private IList<Control> controls;

        public frmReceptionRoadmaps()
        {
            roadmapBLL = new RoadmapBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnAccept, this.btnSearchRoadmaps, this.lblOrigin, this.lblDestination };
            Translate();
            PopulateCombos();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridRoadmaps.AllowUserToAddRows = false;
            this.gridRoadmaps.AllowUserToDeleteRows = false;
            this.gridRoadmaps.MultiSelect = false;
            this.gridRoadmaps.Enabled = true;
            this.gridRoadmaps.ReadOnly = false;
            this.gridRoadmaps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRoadmaps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.comboOrigin.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Tag = "ReceptionRoadmaps";
            this.btnAccept.Tag = "Accept";
            this.btnSearchRoadmaps.Tag = "SearchRoadmaps";
            this.lblDestination.Tag = "Destination";
            this.lblOrigin.Tag = "Origin";

        }

        private void frmReceptionRoadmaps_Load(object sender, EventArgs e)
        {
            try
            {
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

        private void frmReceptionRoadmaps_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void PopulateCombos()
        {
            try
            {
                this.comboOrigin.DataSource = null;
                this.comboOrigin.DataSource = roadmapBLL.GetLocations();
                this.comboOrigin.DisplayMember = "Name";
                this.comboOrigin.ValueMember = "Id";
                this.comboOrigin.SelectedIndex = 0;

                this.txtDestination.Text = Session.Instance.User.Location.CompleteName;
                this.txtDestination.Enabled = false;
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

        private void btnSearchRoadmaps_Click(object sender, System.EventArgs e)
        {
            try
            {
                FormValidation.ValidateComboSelected(this.comboOrigin);

                var origin = (LocationDTO)this.comboOrigin.SelectedItem;
                var roadmaps = roadmapBLL.GetForCreateReception(new RoadmapFilter()
                {
                    IdLocationOrigin = origin.Id,
                    IdLocationDestination = Session.Instance.User.Location.Id,
                    Status = RoadmapStatusEnum.Sent
                });

                this.gridRoadmaps.DataSource = null;
                this.gridRoadmaps.DataSource = roadmaps;
                this.gridRoadmaps.Columns["Id"].Visible = false;

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

        private void btnAccept_Click(object sender, System.EventArgs e)
        {
            try
            {
                var roadmapIds = new List<int>();

                foreach (DataGridViewRow row in gridRoadmaps.Rows)
                {
                    var selectedRoadmap = (ReceptionRoadmapViewDTO)row.DataBoundItem;
                    if (selectedRoadmap.Selected)
                        roadmapIds.Add(selectedRoadmap.Id);
                }

                if (gridRoadmaps.Rows.Count == 0 || roadmapIds.Count == 0)
                {
                    FormValidation.ShowMessage("There are no items to save", ValidationType.Warning);
                    return;
                }

                var frm = new frmNewReception(roadmapIds);
                frm.FormClosed += new FormClosedEventHandler((sender, e) => this.Close());
                frm.ShowDialog();
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
