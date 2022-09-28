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

namespace LaundryManagement.UI.Forms.Roadmap
{
    public partial class frmNewRoadmap : Form, ILanguageObserver
    {
        private ShippingBLL shippingBLL;
        private RoadmapBLL roadmapBLL;
        private LocationBLL locationBLL;
        private LogBLL logBLL;
        private List<Control> controls;

        public frmNewRoadmap()
        {
            shippingBLL = new ShippingBLL();
            locationBLL = new LocationBLL();
            roadmapBLL = new RoadmapBLL();
            logBLL = new LogBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.lblDestination, this.lblOrigin, this.btnSearchShippings, this.btnSave };
            Translate();
            PopulateComboLocation();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridShippings.AllowUserToAddRows = false;
            this.gridShippings.AllowUserToDeleteRows = false;
            this.gridShippings.ReadOnly = false;
            this.gridShippings.Enabled = true;
            this.gridShippings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridShippings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.comboOrigin.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboDestination.DropDownStyle = ComboBoxStyle.DropDownList;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "Roadmap";
            this.btnSave.Tag = "Save";
            this.lblDestination.Tag = "Destination";
            this.lblOrigin.Tag = "Origin";
            this.btnSearchShippings.Tag = "SearchShippings";

            this.gridShippings.DataSource = null;
        }

        private void PopulateComboLocation()
        {
            try
            {
                this.comboOrigin.DataSource = null;
                this.comboOrigin.DataSource = new List<LocationDTO>() { (LocationDTO)Session.Instance.User.Location };
                this.comboOrigin.DisplayMember = "Name";
                this.comboOrigin.ValueMember = "Id";
                this.comboOrigin.SelectedIndex = 0;
                this.comboOrigin.Enabled = false;

                this.comboDestination.DataSource = null;
                this.comboDestination.DataSource = roadmapBLL.GetLocations();
                this.comboDestination.DisplayMember = "Name";
                this.comboDestination.ValueMember = "Id";
                this.comboDestination.SelectedIndex = -1;
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

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void LoadGrid()
        {
            var destination = (LocationDTO)this.comboDestination.SelectedItem;

            var filter = new ShippingFilter()
            {
                Origin = (LocationDTO)Session.Instance.User.Location,
                Destination = destination,
                ShippingStatus = ShippingStatusEnum.Created
            };

            this.gridShippings.DataSource = null;
            this.gridShippings.DataSource = shippingBLL.GetForRoadMapView(filter);
            this.gridShippings.Columns["Id"].Visible = false;
        }

        private void frmNewRoadmap_Load(object sender, EventArgs e) => Session.SubscribeObserver(this);

        private void frmNewRoadmap_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var shippingIds = new List<int>();
                
                foreach(DataGridViewRow row in gridShippings.Rows)
                {
                    var selectedShipping = (RoadmapShippingViewDTO)row.DataBoundItem;
                    if(selectedShipping.Selected)
                        shippingIds.Add(selectedShipping.Id);
                }

                if (gridShippings.Rows.Count == 0 || shippingIds.Count == 0)
                {
                    FormValidation.ShowMessage("There are no items to save", ValidationType.Warning);
                    return;
                }

                var roadmap = new RoadmapDTO();
                roadmap.Shippings = shippingBLL.GetByFilter(new ShippingFilter() { ShippingIds = shippingIds });
                roadmap.CreationUser = (UserDTO)Session.Instance.User;
                roadmap.CreatedDate = DateTime.Now;
                roadmap.Status = RoadmapStatusEnum.Sent;
                roadmap.Origin = (LocationDTO)this.comboOrigin.SelectedItem;
                roadmap.Destination = (LocationDTO)this.comboDestination.SelectedItem;

                roadmapBLL.Send(roadmap);

                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.RoadMap, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.RoadMap, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                logBLL.LogError(MovementTypeEnum.RoadMap, ex.Message);
            }
        }

        private void btnSearchShippings_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGrid();
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
