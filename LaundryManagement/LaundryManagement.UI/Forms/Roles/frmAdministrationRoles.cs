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

namespace LaundryManagement.UI.Forms.Roles
{
    public partial class frmAdministrationPermissions : Form, ILanguageObserver
    {
        private PermissionBLL permissionBLL;
        private IList<Control> controls;

        public frmAdministrationPermissions()
        {
            permissionBLL = new PermissionBLL();

            InitializeComponent();
            ApplySetup();

            controls = new List<Control>() { this, this.btnEdit, this.btnAdd};
            Translate();
        }

        private void ApplySetup()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.treeView1.CheckBoxes = false;

            this.Tag = "Permissions";
            this.btnEdit.Tag = "Edit";
            this.btnAdd.Tag = "Add";
        }

        private void AddChildrenToTree(IEnumerable<ComponentDTO> permissions, TreeNodeCollection nodes)
        {
            try
            {
                foreach (var permission in permissions)
                {
                    var newNode = new TreeNode(permission.Name);
                    newNode.Tag = permission;

                    if (permission is CompositeDTO)
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

        public void LoadPermissions()
        {
            var permissions = permissionBLL.GetAll();
            AddChildrenToTree(permissions, this.treeView1.Nodes);

            this.treeView1.ExpandAll();
        }

        #region Language

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmAdministrationPermissions_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPermissions();

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

        private void frmAdministrationPermissions_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);
        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedItem = this.treeView1.SelectedNode.Tag as ComponentDTO;
            var frm = new frmNewPermission(selectedItem);
            frm.FormClosing += new FormClosingEventHandler((sender, e) => LoadPermissions());
            frm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmNewPermission(null);
            frm.FormClosing += new FormClosingEventHandler((sender, e) => LoadPermissions());
            frm.Show();
        }
    }
}
