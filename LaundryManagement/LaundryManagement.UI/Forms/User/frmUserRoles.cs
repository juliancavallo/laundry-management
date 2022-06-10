using LaundryManagement.Domain.DTOs;
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
        private bool isEdit;
        public frmUserRoles(UserDTO paramDTO, bool edit)
        {
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
        }

        private void frmUserRoles_Load(object sender, EventArgs e)
        {
            AddChildrenToTree(_userDTO.Permissions.Cast<ComponentDTO>(), this.treeView1.Nodes);

            this.treeView1.ExpandAll();
        }

        private void AddChildrenToTree(IEnumerable<ComponentDTO> permissions, TreeNodeCollection nodes)
        {
            foreach (var permission in permissions)
            {
                var newNode = new TreeNode(permission.Name);

                if(permission is CompositeDTO)
                    AddChildrenToTree(permission.Children.Cast<ComponentDTO>(), newNode.Nodes);

                nodes.Add(newNode);
            }
        }
    }
}
