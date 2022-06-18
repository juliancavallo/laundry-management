﻿using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
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
       
        public IList<TranslationViewDTO> GetTranslationsForView(Language idioma = null)
        {
            var translations = dal.GetTranslations(idioma ?? GetDefaultLanguage());
            return translations.Select(x => new TranslationViewDTO()
            {
                IdTranslation = x.Value.Id,
                IdTag = x.Value.Tag.Id,
                Tag = x.Key,
                Description = x.Value.Text
            }).ToList();
        }

        public void Save(List<TranslationViewDTO> list, int languageId)
        {
            var translations = list.Select(x => new Translation()
            {
                Id = x.IdTranslation,
                Text = x.Description,
                Tag = new Tag()
                {
                    Id = x.IdTag,
                    Name = x.Tag
                }
            }).ToList();

            dal.SaveTags(translations.Select(x => x.Tag).ToList());
            dal.SaveTranslations(translations, languageId);
        }

        public void Delete(List<TranslationViewDTO> list, int languageId)
        {
            if(list.Count > 0)
            {
                var translations = list.Where(x => x.IdTag != 0).Select(x => new Translation()
                {
                    Id = x.IdTranslation,
                    Text = x.Description,

                    Tag = new Tag()
                    {
                        Id = x.IdTag,
                        Name = x.Tag
                    }
                }).ToList();

                dal.DeleteTranslations(translations);
                dal.DeleteTags(translations.Select(x => x.Tag).ToList(), languageId);
            }
        }
    }
}
