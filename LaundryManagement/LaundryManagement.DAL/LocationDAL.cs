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
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
	                       l.Id,
                            l.Name,
                            l.Address,
                            l.IsInternal,
                            l.IdLocationType,
                            l.IdParentLocation
                    FROM [Location] l");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                IList<Location> locations = new List<Location>();
                while (reader.Read())
                {
                    var location = this.MapFromDatabase(reader);
                    locations.Add(location);
                }


                return locations;
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
            var connection2 = new SqlConnection();

            connection2.ConnectionString = configuration.GetValue<string>("connectionString");
            SqlDataReader reader = null;
            try
            {
                if(connection2.State == System.Data.ConnectionState.Closed)
                    connection2.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                       l.Id,
                            l.Name,
                            l.Address,
                            l.IsInternal,
                            l.IdLocationType,
                            l.IdParentLocation
                    FROM [Location] l
                    WHERE l.Id = {id}");

                cmd.Connection = connection2;
                reader = cmd.ExecuteReader();

                Location location = new Location();
                while (reader.Read())
                {
                    location = this.MapFromDatabase(reader);
                }

                return location;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection2.Close();
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
                Name = reader["Name"].ToString(),
                Address = reader["Address"].ToString(),
                IsInternal = bool.Parse(reader["IsInternal"].ToString()),
                LocationType = (LocationType)int.Parse(reader["IdLocationType"].ToString()),
                ParentLocation = idParentLocation.HasValue ? GetById(idParentLocation.Value) : null,
            };
        }
    }
}