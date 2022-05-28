using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public static class FormValidation
    {
        private static Dictionary<ValidationType, MessageBoxIcon> validationTypesDictionary = new Dictionary<ValidationType, MessageBoxIcon>()
        {
            { ValidationType.Error, MessageBoxIcon.Error },
            { ValidationType.Warning, MessageBoxIcon.Warning },
        };

        public static void ValidateTextBoxCompleted(List<TextBox> list)
        {
            if (list.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                throw new ValidationException("Must complete all textboxes", ValidationType.Warning);
        }

        public static void ValidateGridSelectedRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null)
                throw new ValidationException("Must select a row", ValidationType.Warning);
        }

        public static void ShowMessage(string message, ValidationType type)
        {
            MessageBox.Show(message, "Notification", MessageBoxButtons.OK, validationTypesDictionary[type]);
        }
    }
}
