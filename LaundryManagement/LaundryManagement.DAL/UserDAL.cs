using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

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

                SqlCommand cmd = new SqlCommand("SELECT * FROM [User]");
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

                SqlCommand cmd = new SqlCommand($"SELECT * FROM [User] WHERE Id = {id}");
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
                            INSERT INTO [User] (Email, Password, UserName, Name, LastName) 
                            VALUES ('{entity.Email}', '{entity.Password}', '{entity.UserName}', '{entity.Name}', '{entity.LastName}')
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [User] SET
	                            Email = '{entity.Email}',
	                            Password = '{entity.Password}',
	                            Name = '{entity.Name}',
	                            LastName = '{entity.LastName}',
	                            UserName = '{entity.UserName}'
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
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
            };
        }
    }
}