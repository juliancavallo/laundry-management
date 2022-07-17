using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Validators
{
    public class LanguageValidator
    {
        public void DeleteDefaultValidation(Language language)
        {
            if (language.Default)
                throw new ValidationException(Session.Translations[Tags.DeleteDefaultLanguage].Text, ValidationType.Warning);
        }

        public void DeleteUsedLanguageValidation(Language language, IList<User> allUsers)
        {
            if (allUsers.Any(x => x.Language.Id == language.Id))
                throw new ValidationException($"The language {language.Name} cannot be deleted because a user has it", ValidationType.Warning);
        }
    }
}
