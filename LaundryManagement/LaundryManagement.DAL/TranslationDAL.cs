using LaundryManagement.Domain;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
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
        private string connectionString;

        public TranslationDAL()
        {
            connection = new SqlConnection();
            connectionString = Session.Settings.ConnectionString;
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
                        t.Id as IdTranslation,
                        t.IdLanguage,
                        t.Description,
                        Tag.Id as IdTag,
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
                        Id = int.Parse(reader["IdTranslation"].ToString()),
                        Text = reader["Description"].ToString(),
                        Tag = new Tag()
                        {
                            Id = int.Parse(reader["IdTag"].ToString()),
                            Name = tag
                        },
                        IdLanguage = int.Parse(reader["IdLanguage"].ToString())
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

        public IList<ITranslation> GetAllTranslations()
        {
            SqlDataReader reader = null;
            IList<ITranslation> translations = new List<ITranslation>();
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($@"
                    SELECT 
                        t.Id as IdTranslation,
                        t.IdLanguage,
                        t.Description,
                        Tag.Id as IdTag,
                        Tag.Name as TagName
                    FROM Translations t
                    INNER JOIN Tag on t.IdTag = Tag.Id");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    translations.Add(
                        new Translation()
                        {
                            Id = int.Parse(reader["IdTranslation"].ToString()),
                            Text = reader["Description"].ToString(),
                            Tag = new Tag()
                            {
                                Id = int.Parse(reader["IdTag"].ToString()),
                                Name = reader["TagName"].ToString()
                            },
                            IdLanguage = int.Parse(reader["IdLanguage"].ToString())
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

        public void SaveTags(IList<ITag> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach(var item in list)
                {
                    cmd.CommandText += $@"
                        if ({item.Id} = 0 and (select Id from Tag where Name = '{item.Name}') is null)
                            insert into Tag (Name) values ('{item.Name}')
                        else
	                        update Tag set Name = '{item.Name}' where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteTags(IList<ITag> list, int languageId)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach (var item in list)
                {
                    cmd.CommandText += $@"
                        if not exists (select Id from Translations where IdTag = {item.Id})
                            delete Tag where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void SaveTranslations(IList<Translation> list, int languageId)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach (var item in list)
                {
                    cmd.CommandText += $@"
                        if ({item.Id} = 0)
                            insert into Translations (IdTag, IdLanguage, Description) 
                            values (
                                (select Id from Tag where Name = '{item.Tag.Name}'), 
                                {languageId}, 
                                '{item.Text}')
                        else
	                        update Translations set Description = '{item.Text}' where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteTranslations(IList<Translation> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach (var item in list)
                {
                    cmd.CommandText += $@"delete Translations where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void SaveLanguages(IList<Language> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach (var item in list)
                {
                    cmd.CommandText += $@"
                        if ({item.Id} = 0)
                            begin
                                insert into Language (Name, [Default]) values ('{item.Name}', {(item.Default ? 1 : 0 )})
                                insert into Translations (IdTag, IdLanguage, Description)
                                select IdTag, (select Id from Language where Name = '{item.Name}'), Description 
                                from Translations t
                                inner join Language l on t.IdLanguage = l.Id
                                where l.[Default] = 1
                            end
                        else
	                        update Language set Name = '{item.Name}', [Default] = {(item.Default ? 1 : 0)} where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteLanguages(IList<Language> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                foreach (var item in list)
                {
                    cmd.CommandText += $@"
                        delete Translations where IdLanguage = {item.Id}
                        delete Language where Id = {item.Id}";
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
