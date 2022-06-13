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

        public ILanguage GetDefaultLanguage()
        {
            return GetAllLanguages().Where(i => i.Default).First();
        }

        public IList<ILanguage> GetAllLanguages() => dal.GetAllLanguages().ToList<ILanguage>();
        public ILanguage GetById(int id) => dal.GetLanguageById(id);
        public IDictionary<string, Translation> GetTranslations(ILanguage idioma = null) => dal.GetTranslations(idioma ?? GetDefaultLanguage());
    }
}
