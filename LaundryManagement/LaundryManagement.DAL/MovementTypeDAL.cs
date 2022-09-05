using LaundryManagement.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.DAL
{
    public class MovementTypeDAL
    {
        private SqlConnection connection;

        public MovementTypeDAL()
        {
            connection = new SqlConnection();

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        public IList<MovementType> GetAll()
        {
            SqlDataReader reader = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT m.Id, m.Name FROM [MovementType] m");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<MovementType> movements = new List<MovementType>();
                while (reader.Read())
                {
                    var movement = this.MapFromDatabase(reader);
                    movements.Add(movement);
                }


                return movements;
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

        private MovementType MapFromDatabase(SqlDataReader reader)
        {
            return new MovementType()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
            };
        }
    }
}
