using LaundryManagement.Domain.Entities;
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

            connection.ConnectionString = Session.Settings.DatabaseSettings.ConnectionString;
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
	                    l.Message,
                        l.IdLogLevel,
                        ll.Description as LogLevelName
                    FROM [Log] l
                    INNER JOIN MovementType mt ON mt.Id = l.IdMovementType
                    INNER JOIN LogLevel ll ON ll.Id = l.IdLogLevel
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
        public List<LogLevel> GetAllLogLevels()
        {
            SqlDataReader reader = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT l.Id, l.Description FROM [LogLevel] l");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                var levels = new List<LogLevel>();
                while (reader.Read())
                {
                    levels.Add(new LogLevel()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Description"].ToString(),
                    });
                }


                return levels;
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
                               ,[Message]
                               ,[IdLogLevel])
                         VALUES
                               ('{item.Date.ToString("yyyy-MM-ddTHH:mm:ss")}'
                               ,{item.MovementType.Id}
                               ,{(item.User != null ? item.User.Id : "NULL")}
                               ,'{item.Message}'
                               ,{item.LogLevel.Id})";

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
            var hasUser = reader["IdUser"] != DBNull.Value;
            return new Log()
            {
                Id = int.Parse(reader["Id"].ToString()),
                User = hasUser ? userDAL.GetById(int.Parse(reader["IdUser"].ToString())) : null,
                Date = DateTime.Parse(reader["Date"].ToString()),
                MovementType = new MovementType()
                {
                    Id = int.Parse(reader["IdMovementType"].ToString()),
                    Name = reader["MovementTypeName"].ToString()
                },
                Message = reader["Message"].ToString(),
                LogLevel = new LogLevel()
                {
                    Id = int.Parse(reader["IdLogLevel"].ToString()),
                    Name = reader["LogLevelName"].ToString()
                }
            };
        }
    }
}