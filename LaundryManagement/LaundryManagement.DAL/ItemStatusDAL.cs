using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;

namespace LaundryManagement.DAL
{
    public class ItemStatusDAL
    {
        private SqlConnection connection;
        public ItemStatusDAL()
        {
            connection = new SqlConnection();

            connection.ConnectionString = Session.Settings.DatabaseSettings.ConnectionString;
        }

        public IList<ItemStatus> GetAll()
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                    i.Id,
                        i.Name
                    FROM ItemStatus i");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<ItemStatus> items = new List<ItemStatus>();
                while (reader.Read())
                {
                    items.Add(this.MapFromDatabase(reader));
                }
                return items;
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

        private ItemStatus MapFromDatabase(SqlDataReader reader)
        {
            return new ItemStatus()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
            };
        }

    }
}