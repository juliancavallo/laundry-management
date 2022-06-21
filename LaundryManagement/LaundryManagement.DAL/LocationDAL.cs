using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using LaundryManagement.Domain.Enums;

namespace LaundryManagement.DAL
{
    public class LocationDAL : ICrud<Location>
    {
        private SqlConnection connection;
        private Configuration configuration;

        public LocationDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public void Delete(Location entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                       $@"
                            DELETE [Location] WHERE Id = {entity.Id}
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

        public IList<Location> GetAll()
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
	                       l.Id,
                            l.Name,
                            l.Address,
                            l.IsInternal,
                            l.IdParentLocation
                    FROM [Location] l");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<Location> users = new List<Location>();
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

        public Location GetById(int id)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                       l.Id,
                            l.Name,
                            l.Address,
                            l.IsInternal,
                            l.IdParentLocation
                    FROM [Location] l
                    WHERE l.Id = {id}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                Location location = new Location();
                while (reader.Read())
                {
                    location = this.MapFromDatabase(reader);
                }
                reader.Close();
                connection.Close();

                return location;
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

        public void Save(Location entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;
            
                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                            INSERT INTO [Location] (Name, Address, IsInternal, IdLocationType, IdParentLocation) 
                            VALUES ('{entity.Name}', '{entity.Address}', {entity.IsInternal}, {(int)entity.LocationType} ,{entity.ParentLocation?.Id})
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [Location] SET
	                            Name = '{entity.Name}',
	                            Address = '{entity.Address}',
	                            IsInternal = {(entity.IsInternal ? 1 : 0)}
	                            IdParentLocation = {entity.ParentLocation?.Id},
	                            IdLocationType = {(int)entity.LocationType}
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

        private Location MapFromDatabase(SqlDataReader reader)
        {
            var idParentLocation = reader["IdParentLocation"].GetType() == typeof(DBNull) ? null : (int?)reader["IdParentLocation"];
            return new Location()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Name = reader["FirstName"].ToString(),
                Address = reader["Address"].ToString(),
                IsInternal = bool.Parse(reader["IsInternal"].ToString()),
                LocationType = (LocationType)int.Parse(reader["IdLocationType"].ToString()),
                ParentLocation = idParentLocation.HasValue ? GetById(idParentLocation.Value) : null,
            };
        }
    }
}