﻿using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using LaundryManagement.Domain.Enums;
using System.Linq;

namespace LaundryManagement.DAL
{
    public class RoadmapDAL
    {
        private SqlConnection connection;
        private LocationDAL locationDAL;
        private ShippingDAL shippingDAL;
        private UserDAL userDAL;
        public RoadmapDAL()
        {
            connection = new SqlConnection();
            shippingDAL = new ShippingDAL();
            locationDAL = new LocationDAL();
            userDAL = new UserDAL();    

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        public List<Roadmap> GetAll()
        {
            SqlDataReader reader = null;
            try 
            { 
                connection.Open();

                var userLocationId = Session.Instance.User.Location.Id;
                SqlCommand cmd = new SqlCommand(@$"
                    SELECT r.Id
                      ,r.CreatedDate
                      ,r.IdCreationUser
                      ,r.IdRoadmapStatus 
	                  ,rs.Name as StatusName
                      ,r.IdLocationOrigin
                      ,r.IdLocationDestination
                  FROM [Roadmap] r
                  INNER JOIN RoadmapStatus rs on r.IdRoadmapStatus = rs.Id");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<Roadmap> roadmaps = new List<Roadmap>();
                while (reader.Read())
                {
                    roadmaps.Add(this.MapFromDatabase(reader));
                }

                return roadmaps;
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

        public int Save(Roadmap entity)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = null;
            
                if (entity.Id == 0)
                {
                    cmd = new SqlCommand(
                        $@"
                        INSERT INTO [Roadmap]
                                   ([CreatedDate]
                                   ,[IdCreationUser]
                                   ,[IdRoadmapStatus]
                                   ,[IdLocationOrigin]
                                   ,[IdLocationDestination])
                             VALUES
                                   ('{entity.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")}'
                                   ,{entity.CreationUser.Id}
                                   ,{entity.Status.Id}
                                   ,{entity.Origin.Id}
                                   ,{entity.Destination.Id})

                        SELECT SCOPE_IDENTITY();
                        ");
                }
                else
                {
                    cmd = new SqlCommand(
                        $@"
                            UPDATE [Roadmap] SET
	                            [IdRoadmapStatus] = {entity.Status.Id},
                            WHERE Id = {entity.Id}
                        ");
                }
                cmd.Connection = connection;
                decimal newId = (decimal)cmd.ExecuteScalar();

                if(entity.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO RoadmapShippings (IdRoadmap, IdShipping) VALUES ";
                    foreach(var item in entity.Shippings)
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

        public void UpdateItems(int newStatus, int newLocation, int idRoadmap)
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
                    INNER JOIN RoadmapShippings rms on sg.IdShipping = rms.IdShipping
                    WHERE rms.IdRoadmap = {idRoadmap}";

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

        private Roadmap MapFromDatabase(SqlDataReader reader)
        {
            var id = int.Parse(reader["Id"].ToString());
            return new Roadmap()
            {
                Id = id,
                CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                Status = new RoadmapStatus()
                {
                    Id = int.Parse(reader["IdRoadmapStatus"].ToString()),
                    Name = reader["StatusName"].ToString()
                },
                Origin = locationDAL.GetById(int.Parse(reader["IdLocationOrigin"].ToString())),
                Destination = locationDAL.GetById(int.Parse(reader["IdLocationDestination"].ToString())),
                CreationUser = userDAL.GetById(int.Parse(reader["IdCreationUser"].ToString())),
                Shippings = shippingDAL.GetByRoadmapId(id)
            };
        }
    }
}