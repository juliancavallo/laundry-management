using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public static class FormValidation
    {
        private static Dictionary<ValidationType, MessageBoxIcon> validationTypesDictionary = new Dictionary<ValidationType, MessageBoxIcon>()
        {
            { ValidationType.Error, MessageBoxIcon.Error },
            { ValidationType.Warning, MessageBoxIcon.Warning },
            { ValidationType.Info, MessageBoxIcon.Information},
        };

        public static void ValidateTextBoxCompleted(List<TextBox> list)
        {
            if (list.Where(x => x != null).Any(x => string.IsNullOrWhiteSpace(x.Text)))
                throw new ValidationException("Must complete all textboxes", ValidationType.Warning);
        }

        public static void ValidateGridSelectedRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null)
                throw new ValidationException("Must select a row", ValidationType.Warning);
        }

        public static void ValidatePasswordMatch(string pwd, string pwd2)
        {
            if(pwd != pwd2)
                throw new ValidationException("The passwords must match", ValidationType.Error);
        }

        public static void ShowMessage(string message, ValidationType type)
        {
            MessageBox.Show(message, "Notification", MessageBoxButtons.OK, validationTypesDictionary[type]);
        }

        public static void ValidateEmailFormat(string email)
        {
            MailAddress.TryCreate(email, out var result);

            if(result == null)
                throw new ValidationException("The email is not in a valid format", ValidationType.Error);
        }
    }
}
