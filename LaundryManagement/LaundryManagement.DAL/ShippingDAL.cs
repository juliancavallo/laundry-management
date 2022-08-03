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
        public ShippingDAL()
        {
            connection = new SqlConnection();
            itemDAL = new ItemDAL();
            locationDAL = new LocationDAL();
            userDAL = new UserDAL();    

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        public List<Shipping> GetByType(ShippingTypeEnum shippingType)
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
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
                    WHERE s.IdShippingType = {(int)shippingType}");

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

                SqlCommand cmd = null;
            
                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                            INSERT INTO [Shipping] (CreatedDate, IdLocationOrigin, IdLocationDestination, IdShippingType, IdShippingStatus, IdCreatedUser, IdResponsibleUser) 
                            VALUES ('{entity.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")}', {entity.Origin.Id}, {entity.Destination.Id}, {entity.Type.Id}, {entity.Status.Id}, {entity.CreationUser.Id}, {entity.Responsible.Id});
                            SELECT SCOPE_IDENTITY();
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [Shipping] SET
	                            IdShippingStatus = {entity.Status.Id},
                            WHERE Id = {entity.Id}
                        ");
                }
                cmd.Connection = connection;
                decimal newId = (decimal)cmd.ExecuteScalar();

                if(entity.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO ShippingDetail (IdItem, IdShipping) VALUES ";
                    foreach(var item in entity.ShippingDetail)
                    {
                        cmd.CommandText += @$"({item.Item.Id}, {newId}),";
                    }
                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
                return (int)newId;
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