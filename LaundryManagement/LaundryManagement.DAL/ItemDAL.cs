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
        private LocationDAL locationDAL;
        public ItemDAL()
        {
            connection = new SqlConnection();
            locationDAL = new LocationDAL();

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        string GetQuery = @$"
                    SELECT 
	                    i.Id as IdItem,
	                    i.Code as ItemCode,
                        i.Created as ItemCreated,
                        i.IdLocation,
                        i.Washes as ItemWashes,
                        st.Id as IdItemStatus,
                        st.Name as ItemStatusName,
	                    a.Id as IdArticle,
	                    a.Name as ArticleName,
                        a.Washes as ArticleWashes,
	                    col.Id as IdColor,
	                    col.Name as ColorName,
	                    s.Id as IdSize,
	                    s.Name as SizeName,
	                    t.Id as IdItemType,
	                    t.Name as ItemTypeName,
	                    cat.Id as IdCategory,
	                    cat.Name as CategoryName
                    FROM Item i
                    INNER JOIN Article a on i.IdArticle = a.Id
                    INNER JOIN Color col on a.IdColor = col.Id
                    INNER JOIN Size s on a.IdSize = s.Id
                    INNER JOIN ItemType t on a.IdItemType = t.Id
                    INNER JOIN Category cat on t.IdCategory = cat.Id
                    INNER JOIN ItemStatus st on i.IdItemStatus = st.Id";

        public Item Get(string? code = null, int? id = null)
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                var condition = string.IsNullOrWhiteSpace(code) ? "1 = 1" : $"i.Code = '{code}'";
                condition += id.HasValue ? $"AND i.Id = {id}" : "";

                SqlCommand cmd = new SqlCommand($@"{GetQuery} WHERE {condition}");

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

        public IList<Item> GetAll()
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(GetQuery);

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<Item> items = new List<Item>();
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

        public void UpdateWashes(IList<int> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = $@"
                    UPDATE Item SET Washes = Washes -1 
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
                ItemStatus = new ItemStatus() 
                { 
                    Id = int.Parse(reader["IdItemStatus"].ToString()) ,
                    Name = reader["ItemStatusName"].ToString()
                },
                Location = locationDAL.GetById(int.Parse(reader["IdLocation"].ToString())),
                Washes = int.Parse(reader["ItemWashes"].ToString()),
                Article = new Article()
                {
                    Id = int.Parse(reader["IdArticle"].ToString()),
                    Name = reader["ArticleName"].ToString(),
                    Washes = int.Parse(reader["ArticleWashes"].ToString()),
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