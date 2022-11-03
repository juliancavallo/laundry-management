using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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
    public partial class frmNewReception : Form, ILanguageObserver
    {
        private IList<Control> controls;
        
        private RoadmapBLL roadmapBLL;
        private ReceptionBLL receptionBLL;
        private ItemBLL itemBLL;

        private IEnumerable<int> roadmapIds;
        private List<ReceptionDetailDTO> receptionDetails;

        public frmNewReception(IEnumerable<int> _roadmapIds)
        {
            roadmapIds = _roadmapIds;

            receptionBLL = new ReceptionBLL();
            roadmapBLL = new RoadmapBLL();
            itemBLL = new ItemBLL();
            receptionDetails = new List<ReceptionDetailDTO>();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave, this.btnAdd, this.btnRemove, this.lblItem};
            Translate();
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridItems.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.gridItems.AllowUserToAddRows = false;
            this.gridItems.AllowUserToDeleteRows = false;
            this.gridItems.MultiSelect = false;
            this.gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Tag = "Reception";
            this.btnAdd.Tag = "Accept";
            this.btnRemove.Tag = "Remove";
            this.btnSave.Tag = "Save";
        }

        private void frmNewReception_Load(object sender, EventArgs e)
        {
            try
            {
                Session.SubscribeObserver(this);
                //this.receptionDetails = receptionBLL.GetDetailByRoadmaps(roadmapIds);
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

        private void frmNewReception_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void LoadGrid()
        {
            this.gridItems.DataSource = null;
            this.gridItems.DataSource = receptionBLL.MapToView(receptionDetails);
            this.gridItems.Columns["ArticleId"].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var code = txtItem.Text;
                if (string.IsNullOrWhiteSpace(code) || receptionDetails.Any(x => x.Item.Code == code))
                {
                    txtItem.Clear();
                    return;
                }

                itemBLL.ApplyFormatValidation(code);

                var item = itemBLL.GetByCode(code);

                var validationResult = receptionBLL.ApplyValidationForReception(item, roadmapBLL.GetById(roadmapIds.First()).Origin);
                FormValidation.ShowValidationMessages(validationResult);

                receptionDetails.Add(new ReceptionDetailDTO()
                {
                    Item = item,
                });

                LoadGrid();
                this.txtItem.Clear();
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

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
