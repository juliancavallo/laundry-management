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
        private LocationDAL locationDAL;

        public UserDAL()
        {
            connection = new SqlConnection();
            locationDAL = new LocationDAL();

            connection.ConnectionString = Session.Settings.ConnectionString;
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
                          ,u.[IdLocation]
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
                        ,u.[IdLocation]
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

        public List<UserHistory> GetHistory(int? idUser, int? idHistory)
        {
            SqlDataReader reader = null;

            var filter = "WHERE " + (idUser.HasValue ? $"u.IdUser = {idUser}" : "1 = 1") + " AND ";
            filter += idHistory.HasValue ? $"u.Id = {idHistory}" : "1 = 1";

            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
                       u.[Id]
                        ,[IdUser]
                        ,[Email]
                        ,[Password]
                        ,[UserName]
                        ,[FirstName]
                        ,[LastName]
                        , l.Id as IdLanguage
                        , l.Name as LanguageName
                        , l.[Default] as LanguageDefault
                        ,u.[IdLocation]
                        ,u.[Date]
                    FROM [UserHistory] u
                    INNER JOIN[Language] L ON U.IdLanguage = l.Id
                    {filter}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<UserHistory> history = new List<UserHistory>();
                while (reader.Read())
                {
                    history.Add(this.MapHistoryFromDatabase(reader));
                }
                reader.Close();
                connection.Close();

                return history;
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
                            INSERT INTO [User] (Email, Password, UserName, FirstName, LastName, IdLanguage, IdLocation) 
                            VALUES ('{entity.Email}', '{entity.Password}', '{entity.UserName}', '{entity.Name}', '{entity.LastName}', '{entity.Language.Id}', '{entity.Location.Id}')
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
                                IdLanguage = '{entity.Language.Id}',
                                IdLocation = '{entity.Location.Id}'
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

        public void SaveHistory(UserHistory entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                        $@"
                            INSERT INTO [UserHistory] (Email, Password, UserName, FirstName, LastName, IdLanguage, IdLocation, IdUser, Date) 
                            VALUES ('{entity.Email}', '{entity.Password}', '{entity.UserName}', '{entity.Name}', '{entity.LastName}', '{entity.Language.Id}', '{entity.Location.Id}', {entity.IdUser}, '{entity.Date.ToString("yyyy-MM-ddTHH:mm:ss")}')
                        ");
                
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

        public void ApplyHistory(UserHistory userHistory)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;

                cmd = new SqlCommand(
                    $@"
                        UPDATE [User] SET
	                        Email = '{userHistory.Email}',
	                        Password = '{userHistory.Password}',
	                        FirstName = '{userHistory.Name}',
	                        LastName = '{userHistory.LastName}',
	                        UserName = '{userHistory.UserName}',
                            IdLanguage = '{userHistory.Language.Id}',
                            IdLocation = '{userHistory.Location.Id}'
                        WHERE Id = {userHistory.IdUser}
                        ");

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
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["FirstName"].ToString(),
                Email = reader["Email"].ToString(),
                LastName = reader["LastName"].ToString(),
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString(),
                Language = new Language()
                {
                    Id = int.Parse(reader["IdLanguage"].ToString()),
                    Name = reader["LanguageName"].ToString(),
                    Default = bool.Parse(reader["LanguageDefault"].ToString())
                },
                Location = locationDAL.GetById(int.Parse(reader["IdLocation"].ToString())),
            };
        }

        private UserHistory MapHistoryFromDatabase(SqlDataReader reader)
        {
            return new UserHistory()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["FirstName"].ToString(),
                Email = reader["Email"].ToString(),
                LastName = reader["LastName"].ToString(),
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString(),
                Language = new Language()
                {
                    Id = int.Parse(reader["IdLanguage"].ToString()),
                    Name = reader["LanguageName"].ToString(),
                    Default = bool.Parse(reader["LanguageDefault"].ToString())
                },
                Location = locationDAL.GetById(int.Parse(reader["IdLocation"].ToString())),
                IdUser = int.Parse(reader["IdUser"].ToString()),
                Date = DateTime.Parse(reader["Date"].ToString()),
            };
        }
    }
}