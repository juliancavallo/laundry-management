using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace LaundryManagement.DAL
{
    public class ItemTypeDAL : ICrud<ItemType>
    {
        private SqlConnection connection;
        private Configuration configuration;
        public ItemTypeDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public void Delete(ItemType entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                       $@"
                            DELETE [ItemType] WHERE Id = {entity.Id}
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

        public IList<ItemType> GetAll()
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                    i.Id,
                        i.Name,
                        i.IdCategory,
                        c.Name as CategoryName
                    FROM ItemType i
                    INNER JOIN Category c on i.IdCategory = c.Id");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<ItemType> items = new List<ItemType>();
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

        public ItemType GetById(int id)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                     SELECT 
	                    i.Id,
                        i.Name,
                        i.IdCategory,
                        c.Name as CategoryName
                    FROM ItemType i
                    INNER JOIN Category c on i.IdCategory = c.Id
                    WHERE i.Id = {id}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                ItemType item = new ItemType();
                while (reader.Read())
                {
                    item = this.MapFromDatabase(reader);
                }

                return item;
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

        public void Save(ItemType entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;

                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                            INSERT INTO [ItemType] (Name, IdCategory) 
                            VALUES ('{entity.Name}', {entity.Category.Id})
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [Item] SET
	                            Name = '{entity.Name}',
                                IdCategory = {entity.Category.Id}
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

        private ItemType MapFromDatabase(SqlDataReader reader)
        {
            return new ItemType()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
                Category = new Category()
                {
                    Id = int.Parse(reader["IdCategory"].ToString()),
                    Name = reader["CategoryName"].ToString(),
                },

            };
        }

    }
}