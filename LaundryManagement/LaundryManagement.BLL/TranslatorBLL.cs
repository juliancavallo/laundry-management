using LaundryManagement.DAL;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class TranslatorBLL
    {
        private TranslationDAL dal;

        public TranslatorBLL()
        {
            dal = new TranslationDAL();
        }

        public Language GetDefaultLanguage()
        {
            return GetAllLanguages().Where(i => i.Default).First();
        }

        public IList<Language> GetAllLanguages() => dal.GetAllLanguages();
        public Language GetById(int id) => dal.GetLanguageById(id);
        public IDictionary<string, ITranslation> GetTranslations(Language idioma = null) => dal.GetTranslations(idioma ?? GetDefaultLanguage());
    }
}
