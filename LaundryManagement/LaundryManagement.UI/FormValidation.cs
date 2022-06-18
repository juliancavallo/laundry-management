using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
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
                throw new ValidationException(Session.Translations[Tags.FormValidationTextboxes].Text, ValidationType.Warning);
        }

        public static void ValidateGridSelectedRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null)
                throw new ValidationException(Session.Translations[Tags.FormValidationGridRow].Text, ValidationType.Warning);
        }

        public static void ValidatePasswordMatch(string pwd, string pwd2)
        {
            if(pwd != pwd2)
                throw new ValidationException(Session.Translations[Tags.PasswordMatch].Text, ValidationType.Error);
        }

        public static void ShowMessage(string message, ValidationType type)
        {
            MessageBox.Show(message, Session.Translations[Tags.Notification].Text, MessageBoxButtons.OK, validationTypesDictionary[type]);
        }

        public static void ValidateEmailFormat(string email)
        {
            MailAddress.TryCreate(email, out var result);

            if(result == null)
                throw new ValidationException(Session.Translations[Tags.PasswordMatch].Text, ValidationType.Error);
        }

        public static void ShowPasswordUnsecureMessage(IPasswordPolicies policies)
        {
            string message = Session.Translations[Tags.PasswordPolicyHeader].Text;

            if (policies.MinLength > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyLength].Text, policies.MinLength);

            if (policies.MinLowercase > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyLowercase].Text, policies.MinLowercase);

            if (policies.MinUppercase > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyUppercase].Text, policies.MinUppercase);

            if (policies.MinSpecialCharacters > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicySpecialCharacters].Text, policies.MinSpecialCharacters);

            if (policies.MinNumbers > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyNumbers].Text, policies.MinNumbers);

            MessageBox.Show(message, Session.Translations[Tags.Notification].Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Translate(IDictionary<string, ITranslation> translations, IList<Control> controls)
        {
            foreach(var control in controls)
            {
                if (control.Tag != null && translations.ContainsKey(control.Tag.ToString()))
                    control.Text = translations[control.Tag.ToString()].Text;
            }
        }
        public static void Translate(IDictionary<string, ITranslation> translations, IList<ToolStripItem> controls)
        {
            foreach (var control in controls)
            {
                if (control.Tag != null && translations.ContainsKey(control.Tag.ToString()))
                    control.Text = translations[control.Tag.ToString()].Text;
            }
        }
    }
}
