using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using LaundryManagement.Domain.Enums;
using System.Linq;

namespace LaundryManagement.DAL
{
    public class ShippingDAL
    {
        private SqlConnection connection;
        private ItemDAL itemDAL;
        private LocationDAL locationDAL;
        private UserDAL userDAL;
        private string GetQuery;
        public ShippingDAL()
        {
            connection = new SqlConnection();
            itemDAL = new ItemDAL();
            locationDAL = new LocationDAL();
            userDAL = new UserDAL();    

            connection.ConnectionString = Session.Settings.ConnectionString;
            GetQuery = @$"
                    SELECT 
	                    s.Id,
	                    s.IdLocationOrigin,
	                    s.IdLocationDestination,
	                    s.CreatedDate,
	                    s.IdShippingStatus,
                        sta.Name as StatusName,
	                    s.IdShippingType,
                        ty.Name as TypeName,
                        s.IdResponsibleUser,
                        s.IdCreatedUser
                    FROM Shipping s
                    INNER JOIN ShippingStatus sta on s.IdShippingStatus = sta.Id
                    INNER JOIN ShippingType ty on s.IdShippingType = ty.Id
                    INNER JOIN Location l on s.IdLocationOrigin = l.Id";
        }

        public List<Shipping> GetByType(ShippingTypeEnum shippingType)
        {
            var userLocationId = Session.Instance.User.Location.Id;
            var filter = @$"
                        WHERE s.IdShippingType = {(int)shippingType} 
                        AND (l.Id = {userLocationId} OR l.IdParentLocation = {userLocationId})";

            return Get(filter);
        }

        public List<Shipping> GetAll() => 
             Get("");

        public List<Shipping> GetByRoadmapId(int roadmapId)
        {
            var filter = @$"
                    INNER JOIN RoadmapShippings rms on rms.IdShipping = s.Id
                    WHERE rms.IdRoadmap = {roadmapId}";

            return Get(filter);
        }

        private List<Shipping> Get(string filter)
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                var userLocationId = Session.Instance.User.Location.Id;
                SqlCommand cmd = new SqlCommand(GetQuery + filter);

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<Shipping> shippings = new List<Shipping>();
                while (reader.Read())
                {
                    shippings.Add(this.MapFromDatabase(reader));
                }

                return shippings;
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

        public List<ShippingDetail> GetDetailByShippingId(int id)
        {
            var subConnection = new SqlConnection();
            subConnection.ConnectionString = Session.Settings.ConnectionString;

            SqlDataReader reader = null;
            try
            {
                if (subConnection.State == System.Data.ConnectionState.Closed)
                    subConnection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
	                    s.IdShipping,
                        s.IdItem
                    FROM ShippingDetail s
                    WHERE s.IdShipping = {id}");

                cmd.Connection = subConnection;
                reader = cmd.ExecuteReader();

                List<ShippingDetail> details = new List<ShippingDetail>();
                while (reader.Read())
                {
                    details.Add(this.MapDetailFromDatabase(reader));
                }


                return details;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                subConnection.Close();
            }
        }

        public int Save(Shipping entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@CheckDigit", entity.CheckDigit);
                sqlParam.DbType = System.Data.DbType.Binary;

                if (entity.Id == 0)
                {
                    cmd.CommandText =
                        $@"
                            INSERT INTO [Shipping] (CreatedDate, IdLocationOrigin, IdLocationDestination, IdShippingType, IdShippingStatus, IdCreatedUser, IdResponsibleUser, CheckDigit) 
                            VALUES ('{entity.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")}', {entity.Origin.Id}, {entity.Destination.Id}, {entity.Type.Id}, {entity.Status.Id}, {entity.CreationUser.Id}, {entity.Responsible.Id}, @CheckDigit);
                            SELECT SCOPE_IDENTITY();
                        ";

                    cmd.Connection = connection;
                    decimal newId = (decimal)cmd.ExecuteScalar();

                    cmd.CommandText = "INSERT INTO ShippingDetail (IdItem, IdShipping) VALUES ";
                    foreach (var item in entity.ShippingDetail)
                    {
                        cmd.CommandText += @$"({item.Item.Id}, {newId}),";
                    }
                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.ExecuteNonQuery();

                    entity.Id = (int)newId;
                }
                else
                {
                    cmd.CommandText = (
                        $@"
                            UPDATE [Shipping] SET
	                            IdShippingStatus = {entity.Status.Id}, CheckDigit = @CheckDigit
                            WHERE Id = {entity.Id};
                        ");
                    cmd.Connection = connection;

                    cmd.ExecuteNonQuery();
                }

                connection.Close();
                return entity.Id;
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

        public void UpdateItems(int newStatus, int newLocation, int idShipping)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = $@"
                    UPDATE i 
                    SET i.IdItemStatus = {newStatus}, i.IdLocation = {newLocation}
                    FROM Item i
                    INNER JOIN ShippingDetail sg ON sg.IdItem = i.Id
                    WHERE sg.IdShipping = {idShipping}";

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

        public string GetStatusName(int id)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"SELECT Name FROM ShippingStatus WHERE Id = {id}");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                string name = "";
                while (reader.Read())
                {
                    name = reader["Name"]?.ToString();
                }

                return name;
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

        private Shipping MapFromDatabase(SqlDataReader reader)
        {
            var id = int.Parse(reader["Id"].ToString());
            return new Shipping()
            {
                Id = id,
                CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                ShippingDetail = this.GetDetailByShippingId(id),
                Type = new ShippingType() 
                { 
                    Id = int.Parse(reader["IdShippingType"].ToString()) ,
                    Name = reader["TypeName"].ToString(),
                },
                Status = new ShippingStatus() 
                { 
                    Id = int.Parse(reader["IdShippingStatus"].ToString()),
                    Name = reader["StatusName"].ToString()
                },
                Origin = locationDAL.GetById(int.Parse(reader["IdLocationOrigin"].ToString())),
                Destination = locationDAL.GetById(int.Parse(reader["IdLocationDestination"].ToString())),
                CreationUser = userDAL.GetById(int.Parse(reader["IdCreatedUser"].ToString())),
                Responsible = userDAL.GetById(int.Parse(reader["IdResponsibleUser"].ToString()))
            };
        }

        private ShippingDetail MapDetailFromDatabase(SqlDataReader reader)
        {
            return new ShippingDetail()
            {
                Item = itemDAL.Get(id: int.Parse(reader["IdItem"].ToString()))
            };
        }
    }
}