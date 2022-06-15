using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.DTOs;
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

namespace LaundryManagement.UI
{
    public partial class frmUserRoles : Form
    {
        private UserDTO _userDTO;
        private PermissionBLL permissionBLL;
        private UserBLL userBLL;
        private bool isEdit;
        public frmUserRoles(UserDTO paramDTO, bool edit)
        {
            permissionBLL = new PermissionBLL();
            userBLL = new UserBLL();

            _userDTO = paramDTO;
            isEdit = edit;

            InitializeComponent();
            ApplySetup();
        }

        private void ApplySetup()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.lblUser.Text = _userDTO.UserName;
            this.treeView1.CheckBoxes = isEdit;
        }

        private void frmUserRoles_Load(object sender, EventArgs e)
        {
            var permissions = isEdit ? permissionBLL.GetAll() : _userDTO.Permissions.Cast<ComponentDTO>();
            AddChildrenToTree(permissions, this.treeView1.Nodes);

            this.treeView1.ExpandAll();
        }

        private void AddChildrenToTree(IEnumerable<ComponentDTO> permissions, TreeNodeCollection nodes)
        {
            foreach (var permission in permissions)
            {
                var newNode = new TreeNode(permission.Name);
                newNode.Tag = permission;
                newNode.Checked = userBLL.HasPermission(_userDTO, permission.Id);

                if(permission is CompositeDTO)
                    AddChildrenToTree(permission.Children.Cast<ComponentDTO>(), newNode.Nodes);

                nodes.Add(newNode);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var components = new List<ComponentDTO>();

            components.AddRange(GetCheckedNodesRecursively(this.treeView1.Nodes));
            _userDTO.Permissions = components.ToList<IComponentDTO>();
            permissionBLL.SavePermissions(_userDTO);
            this.Close();
        }

        public List<ComponentDTO> GetCheckedNodesRecursively(TreeNodeCollection nodes)
        {
            var components = new List<ComponentDTO>();
            foreach(TreeNode node in nodes)
            {
                if (node.Checked)
                    components.Add(node.Tag as ComponentDTO);

                components.AddRange(GetCheckedNodesRecursively(node.Nodes));
            }
            return components;
        }
    }
}
