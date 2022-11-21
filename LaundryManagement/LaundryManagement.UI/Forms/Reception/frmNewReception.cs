﻿using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Extensions;
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
        private LogBLL logBLL;

        private LocationDTO locationOrigin;

        private IEnumerable<int> roadmapIds;
        private List<ReceptionDetailViewDTO> roadmapItems;
        private List<ReceptionDetailDTO> receptionDetail;

        public frmNewReception(IEnumerable<int> _roadmapIds)
        {
            roadmapIds = _roadmapIds;

            receptionBLL = new ReceptionBLL();
            roadmapBLL = new RoadmapBLL();
            itemBLL = new ItemBLL();
            logBLL = new LogBLL();
            roadmapItems = new List<ReceptionDetailViewDTO>();
            receptionDetail = new List<ReceptionDetailDTO>();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave, this.btnAdd, this.btnRemove, this.lblItem};
            Translate();

            locationOrigin = roadmapBLL.GetById(roadmapIds.First()).Origin;
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
            this.lblItem.Tag = "Item";
        }

        private void frmNewReception_Load(object sender, EventArgs e)
        {
            try
            {
                Session.SubscribeObserver(this);
                this.roadmapItems = receptionBLL.GetDetailByRoadmaps(roadmapIds);
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
            this.gridItems.DataSource = roadmapItems;
            this.gridItems.Columns["ArticleId"].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (receptionDetail.Count == 0)
                {
                    FormValidation.ShowMessage("There are no items to save", ValidationType.Warning);
                    return;
                }

                var reception = new ReceptionDTO();
                reception.Origin = locationOrigin;
                reception.Destination = (LocationDTO)Session.Instance.User.Location;
                reception.ReceptionDetail = receptionDetail;
                reception.CreationDate = DateTime.Now;
                reception.CreationUser = (UserDTO)Session.Instance.User;
                reception.Roadmaps = roadmapBLL.GetByIds(roadmapIds);

                receptionBLL.Save(reception);
                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();

                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.Reception, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.Reception, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
                logBLL.LogError(MovementTypeEnum.Reception, ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) => AddItem();

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateGridSelectedRow(this.gridItems);
                var selectedItem = (ReceptionDetailViewDTO)this.gridItems.CurrentRow.DataBoundItem;

                roadmapItems.RemoveAll(x => x.ArticleId == selectedItem.ArticleId);
                receptionDetail.RemoveAll(x => x.Item.Article.Id == selectedItem.ArticleId);

                LoadGrid();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
            }
        }

        private void txtItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddItem();
        }

        private void AddItem()
        {
            try
            {
                var code = txtItem.Text;
                if (string.IsNullOrWhiteSpace(code) || receptionDetail.Any(x => x.Item.Code == code))
                {
                    txtItem.Clear();
                    return;
                }

                itemBLL.ApplyFormatValidation(code);

                var item = itemBLL.GetByCode(code);

                var validationResult = receptionBLL.ApplyValidationForReception(item, locationOrigin);
                FormValidation.ShowValidationMessages(validationResult);

                receptionDetail.Add(new ReceptionDetailDTO()
                {
                    Item = item,
                });

                roadmapItems.AddOrUpdate(item);

                LoadGrid();
                this.txtItem.Clear();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                this.txtItem.Clear();
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                this.txtItem.Clear();
            }
        }
    }
}