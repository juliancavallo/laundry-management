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
    public class ReceptionDAL
    {
        private SqlConnection connection;
        private LocationDAL locationDAL;
        private RoadmapDAL roadmapDAL;
        private UserDAL userDAL;
        private ItemDAL itemDAL;
        public ReceptionDAL()
        {
            connection = new SqlConnection();
            locationDAL = new LocationDAL();
            roadmapDAL = new RoadmapDAL();
            userDAL = new UserDAL();    
            itemDAL = new ItemDAL();

            connection.ConnectionString = Session.Settings.DatabaseSettings.ConnectionString;
        }

        public List<Reception> GetAll()
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT r.Id
                      ,r.CreationDate
                      ,r.IdCreationUser
                      ,r.IdLocationOrigin
                      ,r.IdLocationDestination
                  FROM [Reception] r");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<Reception> receptions = new List<Reception>();
                while (reader.Read())
                {
                    receptions.Add(this.MapFromDatabase(reader));
                }

                return receptions;
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

        public List<ReceptionDetail> GetDetailByReceptionId(int id)
        {
            var subConnection = new SqlConnection();
            subConnection.ConnectionString = Session.Settings.DatabaseSettings.ConnectionString;

            SqlDataReader reader = null;
            try
            {
                if (subConnection.State == System.Data.ConnectionState.Closed)
                    subConnection.Open();

                SqlCommand cmd = new SqlCommand(@$"
                    SELECT 
                        r.IdItem
                    FROM ReceptionDetail r
                    WHERE r.IdReception = {id}");

                cmd.Connection = subConnection;
                reader = cmd.ExecuteReader();

                List<ReceptionDetail> details = new List<ReceptionDetail>();
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

        public int Save(Reception entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    $@"
                    INSERT INTO [Reception]
                                ([CreationDate]
                                ,[IdCreationUser]
                                ,[IdLocationOrigin]
                                ,[IdLocationDestination])
                            VALUES
                                ('{entity.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")}'
                                ,{entity.CreationUser.Id}
                                ,{entity.Origin.Id}
                                ,{entity.Destination.Id})

                    SELECT SCOPE_IDENTITY();
                    ");

                cmd.Connection = connection;
                decimal newId = (decimal)cmd.ExecuteScalar();

                cmd.CommandText = "INSERT INTO ReceptionDetail (IdItem, IdReception) VALUES ";
                foreach (var item in entity.ReceptionDetail)
                {
                    cmd.CommandText += @$"({item.Item.Id}, {newId}),";
                }
                cmd.CommandText = cmd.CommandText.TrimEnd(',');
                cmd.ExecuteNonQuery();

                if (entity.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO RoadmapReception (IdReception, IdRoadmap) VALUES ";
                    foreach(var item in entity.Roadmaps)
                    {
                        cmd.CommandText += @$"({newId}, {item.Id}),";
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

        public void UpdateItems(int newStatus, int newLocation, int idReception)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = $@"
                    UPDATE i 
                    SET i.IdItemStatus = {newStatus}, i.IdLocation = {newLocation}
                    FROM Item i
                    INNER JOIN ReceptionDetail rd on rd.IdItem = i.Id
                    WHERE rd.IdReception = {idReception}";

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

        private Reception MapFromDatabase(SqlDataReader reader)
        {
            var id = int.Parse(reader["Id"].ToString());
            return new Reception()
            {
                Id = id,
                CreatedDate = DateTime.Parse(reader["CreationDate"].ToString()),
                Origin = locationDAL.GetById(int.Parse(reader["IdLocationOrigin"].ToString())),
                Destination = locationDAL.GetById(int.Parse(reader["IdLocationDestination"].ToString())),
                CreationUser = userDAL.GetById(int.Parse(reader["IdCreationUser"].ToString())),
                Roadmaps = roadmapDAL.GetByReception(id),
                ReceptionDetail = this.GetDetailByReceptionId(id)
            };
        }

        private ReceptionDetail MapDetailFromDatabase(SqlDataReader reader)
        {
            return new ReceptionDetail()
            {
                Item = itemDAL.Get(id: int.Parse(reader["IdItem"].ToString()))
            };
        }
    }
}