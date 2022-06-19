using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Enums
{
    public static class Tags
    {
        public const string SessionAlreadyOpen = "SessionAlreadyOpen";
        public const string NonexistentUser = "NonexistentUser";
        public const string IncorrectPassword = "IncorrectPassword";
        public const string DeleteLoggedUser = "DeleteLoggedUser";
        public const string EditLoggedUser = "EditLoggedUser";
        public const string UserDuplicate = "UserDuplicate";
        public const string PasswordReset = "PasswordReset";
        public const string PasswordPlaceholder = "PasswordPlaceholder";
        public const string FormValidationTextboxes = "FormValidationTextboxes";
        public const string FormValidationGridRow = "FormValidationGridRow";
        public const string PasswordMatch = "PasswordMatch";
        public const string EmailFormat = "EmailFormat";
        public const string Notification = "Notification";
        public const string PasswordPolicyHeader = "PasswordPolicyHeader";
        public const string PasswordPolicyLength = "PasswordPolicyLength";
        public const string PasswordPolicyLowercase = "PasswordPolicyLowercase";
        public const string PasswordPolicyUppercase = "PasswordPolicyUppercase";
        public const string PasswordPolicySpecialCharacters = "PasswordPolicySpecialCharacters";
        public const string PasswordPolicyNumbers = "PasswordPolicyNumbers";
        public const string DeleteDefaultLanguage = "DeleteDefaultLanguage";
    }
}
