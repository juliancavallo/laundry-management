﻿using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using System.Linq;

namespace LaundryManagement.DAL
{
    public class LogDAL
    {
        private SqlConnection connection;
        private ItemDAL itemDAL;
        private UserDAL userDAL;

        public LogDAL()
        {
            connection = new SqlConnection();
            userDAL = new UserDAL();    
            itemDAL = new ItemDAL();

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        public List<Log> Get()
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"
                    SELECT 
	                    l.Id,
	                    l.Date,
	                    l.IdMovementType,
	                    mt.Name as MovementTypeName,
	                    l.IdUser,
	                    l.Message
                    FROM [Log] l
                    INNER JOIN MovementType mt ON mt.Id = l.IdMovementType
                    ORDER BY l.Date DESC
                    ";
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<Log> logs = new List<Log>();
                while (reader.Read())
                {
                    logs.Add(this.MapFromDatabase(reader));
                }


                return logs;
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

        public void Save(Log item)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"
                    INSERT INTO [Log]
                               ([Date]
                               ,[IdMovementType]
                               ,[IdUser]
                               ,[Message])
                         VALUES
                               ('{item.Date.ToString("yyyy-MM-ddTHH:mm:ss")}'
                               ,{item.MovementType.Id}
                               ,{item.User.Id}
                               ,'{item.Message}')";

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

        private Log MapFromDatabase(SqlDataReader reader)
        {
            return new Log()
            {
                Id = int.Parse(reader["Id"].ToString()),
                User = userDAL.GetById(int.Parse(reader["IdUser"].ToString())),
                Date = DateTime.Parse(reader["Date"].ToString()),
                MovementType = new MovementType()
                {
                    Id = int.Parse(reader["IdMovementType"].ToString()),
                    Name = reader["MovementTypeName"].ToString()
                },
                Message = reader["Message"].ToString(),
            };
        }
    }
}