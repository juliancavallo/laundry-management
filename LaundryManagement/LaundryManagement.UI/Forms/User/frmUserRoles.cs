using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public partial class frmUserRoles : Form, ILanguageObserver
    {
        private PermissionBLL permissionBLL;
        private UserBLL userBLL;
        private LogBLL logBLL;

        private IList<Control> controls;
        private bool isEdit;
        private UserDTO _userDTO;
        public frmUserRoles(UserDTO paramDTO, bool edit)
        {
            permissionBLL = new PermissionBLL();
            logBLL = new LogBLL();
            userBLL = new UserBLL();
            _userDTO = paramDTO;
            isEdit = edit;

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnSave };
            Translate();
        }

        private void ApplySetup()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.lblUser.Text = _userDTO.UserName;
            this.treeView1.CheckBoxes = isEdit;

            this.Tag = "Roles";
            this.btnSave.Tag = "Save";
        }

        private void AddChildrenToTree(IEnumerable<ComponentDTO> permissions, TreeNodeCollection nodes)
        {
            try 
            { 
                foreach (var permission in permissions)
                {
                    var newNode = new TreeNode(permission.Name);
                    newNode.Tag = permission;
                    newNode.Checked = userBLL.HasPermission(_userDTO, permission.Permission);

                    if(permission is CompositeDTO)
                        AddChildrenToTree(permission.Children.Cast<ComponentDTO>(), newNode.Nodes);

                    nodes.Add(newNode);
                }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var components = new List<ComponentDTO>();
                components.AddRange(GetCheckedNodesRecursively(this.treeView1.Nodes));

                permissionBLL.SaveUserPermissions(_userDTO.Id, components);
                this.Close();
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);

                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.UserRoles, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.UserRoles, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                logBLL.LogError(MovementTypeEnum.UserRoles, ex.Message);
            }
        }

        public List<ComponentDTO> GetCheckedNodesRecursively(TreeNodeCollection nodes)
        {
            var components = new List<ComponentDTO>();
            try
            {
                
                foreach(TreeNode node in nodes)
                {
                    if (node.Checked)
                        components.Add(node.Tag as ComponentDTO);

                    components.AddRange(GetCheckedNodesRecursively(node.Nodes));
                }
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
            }
            return components;
        }

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmUserRoles_Load(object sender, EventArgs e)
        {
            try
            {
                var permissions = isEdit ? permissionBLL.GetAll() : _userDTO.Permissions.Cast<ComponentDTO>();
                AddChildrenToTree(permissions, this.treeView1.Nodes);

                this.treeView1.ExpandAll();

                Session.SubscribeObserver(this);
            }
            catch (ValidationException ex)
            {
                FormValidation.ShowMessage(ex.Message, ex.ValidationType);
                switch (ex.ValidationType)
                {
                    case ValidationType.Warning:
                        logBLL.LogWarning(MovementTypeEnum.UserRoles, ex.Message);
                        break;

                    case ValidationType.Error:
                        logBLL.LogError(MovementTypeEnum.UserRoles, ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                FormValidation.ShowMessage(ex.Message, ValidationType.Error);
                logBLL.LogError(MovementTypeEnum.UserRoles, ex.Message);
            }
        }

        private void frmUserRoles_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                SetNodesCheckedStatus(e.Node);
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

        private void SetNodesCheckedStatus(TreeNode node)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = node.Checked;
                SetNodesCheckedStatus(item);
            }
        }
    }
}
