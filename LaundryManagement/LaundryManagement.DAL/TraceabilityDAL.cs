using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using LaundryManagement.Services;
using System.Linq;

namespace LaundryManagement.DAL
{
    public class TraceabilityDAL
    {
        private SqlConnection connection;
        private Configuration configuration;

        public TraceabilityDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public void Save(List<Traceability> list)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"
                    INSERT INTO [Traceability]
                               ([IdItem]
                               ,[Date]
                               ,[IdMovementType]
                               ,[IdUser]
                               ,[IdLocationOrigin]
                               ,[IdLocationDestination]
                               ,[IdItemStatus])
                         VALUES
                    ";

                foreach (var item in list)
                {
                    cmd.CommandText += @$"
                               ({item.Item.Id}
                               ,'{item.Date.ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,{item.MovementType.Id}
                               ,{item.User.Id}
                               ,{item.Origin.Id}
                               ,{item.Destination.Id}
                               ,{item.ItemStatus.Id}),";
                }
                cmd.CommandText = cmd.CommandText.TrimEnd(',');

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
    }
}