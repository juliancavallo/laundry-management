using LaundryManagement.Domain.DTOs;
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
                throw new ValidationException(Session.Translations[Tags.FormValidationTextboxes], ValidationType.Warning);
        }

        public static void ValidateGridSelectedRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow == null)
                throw new ValidationException(Session.Translations[Tags.FormValidationGridRow], ValidationType.Warning);
        }

        public static void ValidateComboSelected(ComboBox combo)
        {
            if (combo.SelectedItem == null)
                throw new ValidationException(Session.Translations[Tags.FormValidationCombo], ValidationType.Warning);
        }

        public static void ValidateTreeViewSelected(TreeView treeView)
        {
            if (treeView.SelectedNode == null)
                throw new ValidationException(Session.Translations[Tags.FormValidationTreeView], ValidationType.Warning);
        }

        public static void ValidatePasswordMatch(string pwd, string pwd2)
        {
            if(pwd != pwd2)
                throw new ValidationException(Session.Translations[Tags.PasswordMatch], ValidationType.Error);
        }

        public static void ShowMessage(string message, ValidationType type)
        {
            MessageBox.Show(message, Session.Translations[Tags.Notification], MessageBoxButtons.OK, validationTypesDictionary[type]);
        }

        public static void ValidateEmailFormat(string email)
        {
            MailAddress.TryCreate(email, out var result);

            if(result == null)
                throw new ValidationException(Session.Translations[Tags.EmailFormat], ValidationType.Error);
        }

        public static void ShowPasswordUnsecureMessage()
        {
            string message = Session.Translations[Tags.PasswordPolicyHeader];
            var policies = Session.Settings.PasswordPolicy;

            if (policies.PasswordMinLength > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyLength], policies.PasswordMinLength);

            if (policies.PasswordMinLowercase > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyLowercase], policies.PasswordMinLowercase);

            if (policies.PasswordMinUppercase > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyUppercase], policies.PasswordMinUppercase);

            if (policies.PasswordMinSpecialCharacters > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicySpecialCharacters], policies.PasswordMinSpecialCharacters);

            if (policies.PasswordMinNumbers > 0)
                message += Environment.NewLine + string.Format(Session.Translations[Tags.PasswordPolicyNumbers], policies.PasswordMinNumbers);

            MessageBox.Show(message, Session.Translations[Tags.Notification], MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Translate(IDictionary<string, string> translations, IList<Control> controls)
        {
            foreach (var control in controls)
            {
                if (control.Tag != null)
                    control.Text = translations.ContainsKey(control.Tag.ToString()) ? 
                        translations[control.Tag.ToString()] : 
                        control.Tag.ToString();
            }
        }
        public static void Translate(IDictionary<string, string> translations, IList<ToolStripItem> controls)
        {
            foreach (var control in controls)
            {
                var tagValue = control.Tag?.GetType()?.GetProperty("TagName")?.GetValue(control.Tag, null);
                if (control.Tag != null && translations.ContainsKey(tagValue.ToString()))
                    control.Text = translations[tagValue.ToString()];
            }
        }

        public static void ShowValidationMessages(ValidationResponseDTO validationResult)
        {
            if (validationResult.Messages.Count > 0)
                ShowMessage(string.Join(Environment.NewLine, validationResult.Messages), ValidationType.Info);
        }
    }
}
