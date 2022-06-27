using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using System.Linq;

namespace LaundryManagement.DAL
{
    public class ItemDAL
    {
        private SqlConnection connection;
        private Configuration configuration;

        public ItemDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public Item Get(string? code = null, int? id = null)
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                var condition = string.IsNullOrWhiteSpace(code) ? "1 = 1" : $"i.Code = '{code}'";
                condition += id.HasValue ? $"AND i.Id = {id}" : "";

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                    i.Id as IdItem,
	                    i.Code as ItemCode,
                        i.Created as ItemCreated,
	                    a.Id as IdArticle,
	                    a.Name as ArticleName,
	                    col.Id as IdColor,
	                    col.Name as ColorName,
	                    s.Id as IdSize,
	                    s.Name as SizeName,
	                    t.Id as IdItemType,
	                    t.Name as ItemTypeName,
	                    cat.Id as IdCategory,
	                    cat.Name as CategoryName,
                        i.IdItemStatus
                    FROM Item i
                    INNER JOIN Article a on i.IdArticle = a.Id
                    INNER JOIN Color col on a.IdColor = col.Id
                    INNER JOIN Size s on a.IdSize = s.Id
                    INNER JOIN ItemType t on a.IdItemType = t.Id
                    INNER JOIN Category cat on t.IdCategory = cat.Id
                    WHERE {condition}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                Item item = null;
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

        public void Save(Item entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;
            
                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                            INSERT INTO [Item] (Code, Created, IdArticle) 
                            VALUES ('{entity.Code}', '{entity.Created}', {entity.Article.Id})
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [Item] SET
	                            Code = '{entity.Code}',
                                IdArticle = {entity.Article.Id}
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

        public void UpdateStatus(IList<int> list, int newStatus)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = $@"
                    UPDATE Item SET IdItemStatus = {newStatus} 
                    WHERE Id in ({string.Join(',', list)})
                    ";

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

        private Item MapFromDatabase(SqlDataReader reader)
        {
            return new Item()
            {
                Id = int.Parse(reader["IdItem"].ToString()),
                Code = reader["ItemCode"].ToString(),
                Created = DateTime.Parse(reader["ItemCreated"].ToString()),
                ItemStatus = new ItemStatus() { Id = int.Parse(reader["IdItemStatus"].ToString()) },
                Article = new Article()
                {
                    Id = int.Parse(reader["IdArticle"].ToString()),
                    Name = reader["ArticleName"].ToString(),
                    Color = new Color()
                    {
                        Id = int.Parse(reader["IdColor"].ToString()),
                        Name = reader["ColorName"].ToString(),
                    },
                    Size = new Size()
                    {
                        Id = int.Parse(reader["IdSize"].ToString()),
                        Name = reader["SizeName"].ToString(),
                    },
                    Type = new ItemType()
                    {
                        Id = int.Parse(reader["IdItemType"].ToString()),
                        Name = reader["ItemTypeName"].ToString(),
                        Category = new Category()
                        {
                            Id = int.Parse(reader["IdCategory"].ToString()),
                            Name = reader["CategoryName"].ToString(),
                        },
                    },
                }
            };
        }
    }
}