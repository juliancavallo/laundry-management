using LaundryManagement.Domain;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.DAL
{
    public class TranslationDAL
    {
        private SqlConnection connection;
        private Configuration configuration;
        private string connectionString;

        public TranslationDAL()
        {
            configuration = new Configuration();

            connection = new SqlConnection();
            connectionString = configuration.GetValue<string>("connectionString");
            connection.ConnectionString = connectionString;
        }

        public IList<Language> GetAllLanguages()
        {
            SqlDataReader reader = null; 
            IList<Language> languages = new List<Language>();
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT Id, Name, [Default] FROM [Language]");
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    languages.Add(
                     new Language()
                     {
                         Id = int.Parse(reader["Id"].ToString()),
                         Name = reader["Name"].ToString(),
                         Default = bool.Parse(reader["Default"].ToString())

                     });
                }
                return languages;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        public Language GetLanguageById(int id)
        {
            SqlDataReader reader = null;
            Language language = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($@"SELECT Id, Name, [Default] FROM [Language] WHERE Id = {id}");
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                     language = new Language()
                     {
                         Id = int.Parse(reader["Id"].ToString()),
                         Name = reader["Name"].ToString(),
                         Default = bool.Parse(reader["Default"].ToString())

                     };
                }
                return language;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        public IDictionary<string, ITranslation> GetTranslations(ILanguage language)
        {
            SqlDataReader reader = null;
            IDictionary<string, ITranslation> translations = new Dictionary<string, ITranslation>();
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($@"
                    SELECT 
                        t.IdLanguage,
                        t.Description,
                        Tag.Id as TagId,
                        Tag.Name as TagName
                    FROM Translations t
                    INNER JOIN Tag on t.IdTag = Tag.Id
                    WHERE t.IdLanguage = {language.Id}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var tag = reader["TagName"].ToString();
                    translations.Add(tag,
                    new Translation()
                    {
                        Text = reader["Description"].ToString(),
                        Tag = new Tag()
                        {
                            Id = int.Parse(reader["TagId"].ToString()),
                            Name = tag
                        }
                    });
                }
                return translations;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }
    }
}
