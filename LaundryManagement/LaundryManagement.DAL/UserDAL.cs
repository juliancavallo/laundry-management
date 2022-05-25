using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;

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
            connection.ConnectionString = configuration.GetValue("connectionString");
            connection.Open();
        }

        public void Delete(User entity)
        {
            SqlCommand cmd = new SqlCommand(
                   $@"
                        DELETE [User] WHERE Id = {entity.Id}
                    ");
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
        }

        public IList<User> GetAll()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User]");
            cmd.Connection = connection;
            SqlDataReader reader = cmd.ExecuteReader();

            IList<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(this.MapFromDatabase(reader));
            }
            reader.Close();

            return users;
        }

        public User GetById(int id)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM [User] WHERE Id = {id}");
            cmd.Connection = connection;
            SqlDataReader reader = cmd.ExecuteReader();

            User user = new User();
            while (reader.Read())
            {
                user = this.MapFromDatabase(reader);
            }
            reader.Close();

            return user;
        }

        public void Save(User entity)
        {
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