using LaundryManagement.Domain.DataAnnotations;
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

namespace LaundryManagement.UI.Forms.Integrity
{
    public partial class frmCorruptedEntities : Form
    {
        private IList<Control> controls;
        private IEnumerable<ICheckDigitEntity> _horizontalCorruptedEntities;
        private IEnumerable<Type> _verticalCorruptedEntities;
        public frmCorruptedEntities(IEnumerable<ICheckDigitEntity> horizontalCorruptedEntities, IEnumerable<Type> verticalCorruptedEntities)
        {
            InitializeComponent();
            ApplySetup();
            controls = new List<Control>() { this };

            FormValidation.Translate(Session.Translations, controls);

            _horizontalCorruptedEntities = horizontalCorruptedEntities;
            _verticalCorruptedEntities = verticalCorruptedEntities;
        }

        private void ApplySetup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.textBox1.Enabled = false;
            this.textBox1.Multiline = true;
            this.textBox1.Size = new System.Drawing.Size(279, 267);
            this.textBox1.ScrollBars = ScrollBars.Vertical;

            this.Tag = "CorruptedEntities";
        }

        private void frmCorruptedEntities_Load(object sender, EventArgs e)
        {
            foreach(var entity in this._horizontalCorruptedEntities)
            {
                this.textBox1.Text += "HorizontalCorruptedEntities:";
                var entityType = entity.GetType();
                this.textBox1.Text += $"Entity name: {entityType.Name}" + Environment.NewLine;
                this.textBox1.Text += "Entity properties:" + Environment.NewLine;

                foreach(var prop in entityType.GetProperties())
                {
                    if(Attribute.IsDefined(prop, typeof(IntegrityProperty)))
                        this.textBox1.Text += $"-{prop.Name}: {prop.GetValue(entity)}" + Environment.NewLine; 
                }

                this.textBox1.Text += Environment.NewLine;

            }

            foreach (var entity in this._verticalCorruptedEntities)
            {
                this.textBox1.Text += "VerticalCorruptedEntities:" + Environment.NewLine;
                this.textBox1.Text += $"Entity name: {entity.Name}";

                this.textBox1.Text += Environment.NewLine;

            }
        }
    }
}
