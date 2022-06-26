using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
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

namespace LaundryManagement.UI.Forms.Roles
{
    public partial class frmNewPermission : Form, ILanguageObserver
    {
        private PermissionBLL permissionBLL;
        private IList<Control> controls;
        private ComponentDTO componentDTO;
        private IList<ComponentDTO> childs;

        public frmNewPermission(ComponentDTO _componentDTO)
        {
            componentDTO = _componentDTO;
            childs = new List<ComponentDTO>();
            
            permissionBLL = new PermissionBLL();

            InitializeComponent();
            ApplySetup();
            PopulateComboLeafs();
            PopulateComboFamilies();

            controls = new List<Control>() { this, this.btnSave, this.btnRemove, this.lblParent, this.lblPermissionCode, this.lblPermissionName, this.lblFamilies, this.lblLeafs, this.btnAddLeaf};
            Translate();
        }

        private void ApplySetup()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Tag = "Permissions";
            this.btnSave.Tag = "Save";
            this.lblParent.Tag = "Childs";
            this.lblPermissionCode.Tag = "PermissionCode";
            this.lblPermissionName.Tag = "Name";
            this.lblLeafs.Tag = "Leafs";
            this.lblFamilies.Tag = "Families";
            this.btnAddLeaf.Tag = "Add";
            this.btnAddFamily.Tag = "Add";
            this.btnRemove.Tag = "Remove";

            this.txtPermissionCode.Text = componentDTO?.Permission;
            this.txtPermissionName.Text = componentDTO?.Name;
        }

        #region Language

        public void UpdateLanguage(ILanguage language) => Translate();

        private void Translate() => FormValidation.Translate(Session.Translations, controls);

        private void frmNewPermission_Load(object sender, EventArgs e)
        {
            try
            {
                if (componentDTO != null)
                {
                    this.childs = componentDTO.Children.Cast<ComponentDTO>().ToList();
                    AddChildrenToTree(childs, this.treeView1.Nodes);
                    this.treeView1.ExpandAll();
                }

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

        private void frmNewPermission_FormClosing(object sender, FormClosingEventArgs e) => Session.UnsubscribeObserver(this);
        #endregion

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

        private void PopulateComboLeafs()
        {
            try
            {
                this.comboLeafs.DataSource = null;
                this.comboLeafs.DataSource = permissionBLL.GetLeafs();
                this.comboLeafs.DisplayMember = "Name";
                this.comboLeafs.ValueMember = "Id";

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

        private void PopulateComboFamilies()
        {
            try
            {
                this.comboFamilies.DataSource = null;
                this.comboFamilies.DataSource = permissionBLL.GetFamilies();
                this.comboFamilies.DisplayMember = "Name";
                this.comboFamilies.ValueMember = "Id";

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

        private void btnAddFamily_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateComboSelected(comboFamilies);

                var selection = (ComponentDTO)comboFamilies.SelectedItem;

                if (!childs.Select(x => x.Id).Contains(selection.Id))
                {
                    permissionBLL.SetChilds(ref selection);
                    childs.Add(selection);

                    treeView1.Nodes.Clear();
                    AddChildrenToTree(childs, this.treeView1.Nodes);
                    treeView1.ExpandAll();
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

        private void btnAddLeaf_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateComboSelected(comboFamilies);

                var selection = (ComponentDTO)comboLeafs.SelectedItem;

                if (!childs.Select(x => x.Id).Contains(selection.Id))
                {
                    childs.Add(selection);

                    treeView1.Nodes.Clear();
                    AddChildrenToTree(childs, this.treeView1.Nodes);
                    treeView1.ExpandAll();
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                FormValidation.ValidateTreeViewSelected(this.treeView1);

                var selection = (ComponentDTO)this.treeView1.SelectedNode.Tag;

                if (!childs.Select(x => x.Id).Contains(selection.Id))
                    throw new ValidationException("You can only delete top-level permissions", ValidationType.Warning);

                childs.Remove(selection);                

                treeView1.Nodes.Clear();
                AddChildrenToTree(childs, this.treeView1.Nodes);
                treeView1.ExpandAll();

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
                FormValidation.ValidateTextBoxCompleted(new List<TextBox>() { this.txtPermissionCode, this.txtPermissionName });

                ComponentDTO component;

                if (treeView1.Nodes.Count > 0)
                    component = new CompositeDTO();
                else
                    component = new LeafDTO();

                component.Id = componentDTO?.Id ?? 0;
                component.Name = this.txtPermissionName.Text;
                component.Permission = this.txtPermissionCode.Text;

                foreach (var item in childs)
                {
                    component.AddChildren(item);
                }

                permissionBLL.SavePermission(component);
                this.Close();

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
