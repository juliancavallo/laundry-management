using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;

namespace LaundryManagement.DAL
{
    public class UserDAL : ICrud<User>
    {
        private SqlConnection connection;
        private Configuration configuration;

        public UserDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public void Delete(User entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                       $@"
                            DELETE [User] WHERE Id = {entity.Id}
                        ");
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

        public IList<User> GetAll()
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
	                       u.[Id]
                          ,[Email]
                          ,[Password]
                          ,[UserName]
                          ,[FirstName]
                          ,[LastName]
                          ,l.Id as IdLanguage
	                      ,l.Name as LanguageName
	                      ,l.[Default] as LanguageDefault
                      FROM [User] u
                      INNER JOIN [Language] L ON U.IdLanguage = l.Id");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(this.MapFromDatabase(reader));
                }


                return users;
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

        public User GetById(int id)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
                        u.[Id]
                        ,[Email]
                        ,[Password]
                        ,[UserName]
                        ,[FirstName]
                        ,[LastName]
                        , l.Id as IdLanguage
                        , l.Name as LanguageName
                        , l.[Default] as LanguageDefault
                    FROM[User] u
                    INNER JOIN[Language] L ON U.IdLanguage = l.Id
                    WHERE u.Id = {id}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                User user = new User();
                while (reader.Read())
                {
                    user = this.MapFromDatabase(reader);
                }
                reader.Close();
                connection.Close();

                return user;
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

        public void Save(User entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;
            
                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                            INSERT INTO [User] (Email, Password, UserName, FirstName, LastName, IdLanguage) 
                            VALUES ('{entity.Email}', '{entity.Password}', '{entity.UserName}', '{entity.Name}', '{entity.LastName}', '{entity.Language.Id}')
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [User] SET
	                            Email = '{entity.Email}',
	                            Password = '{entity.Password}',
	                            FirstName = '{entity.Name}',
	                            LastName = '{entity.LastName}',
	                            UserName = '{entity.UserName}',
                                IdLanguage = '{entity.Language.Id}'
                            WHERE Id = {entity.Id}
                            ");
                }
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();

                connection.Close();
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

        private User MapFromDatabase(SqlDataReader reader)
        {
            return new User()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("FirstName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                Language = new Language()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("IdLanguage")),
                    Name = reader.GetString(reader.GetOrdinal("LanguageName")),
                    Default = reader.GetBoolean(reader.GetOrdinal("LanguageDefault"))
                }
            };
        }
    }
}