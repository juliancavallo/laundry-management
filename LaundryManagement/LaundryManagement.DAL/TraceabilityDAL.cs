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
        private LocationDAL locationDAL;
        private ItemDAL itemDAL;
        private UserDAL userDAL;

        public TraceabilityDAL()
        {
            configuration = new Configuration();
            connection = new SqlConnection();
            locationDAL = new LocationDAL();
            userDAL = new UserDAL();    
            itemDAL = new ItemDAL();

            connection.ConnectionString = configuration.GetValue<string>("connectionString");
        }

        public List<Traceability> Get(string code)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"
                    SELECT t.[Id]
                          ,t.[IdItem]
                          ,t.[Date]
                          ,t.[IdMovementType]
                          ,t.[IdUser]
                          ,t.[IdLocationOrigin]
                          ,t.[IdLocationDestination]
                          ,t.[IdItemStatus]
	                      , st.Name as ItemStatusName
	                      , mt.Name as MovementType
                      FROM [Traceability] t
                      INNER JOIN Item i on t.IdItem = i.Id
                      INNER JOIN ItemStatus st on t.IdItemStatus = st.Id
                      INNER JOIN MovementType mt on t.IdMovementType = mt.Id
                      WHERE i.Code = '{code}'
                    ";
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                List<Traceability> traceabilities = new List<Traceability>();
                while (reader.Read())
                {
                    traceabilities.Add(this.MapFromDatabase(reader));
                }


                return traceabilities;
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

        private Traceability MapFromDatabase(SqlDataReader reader)
        {
            return new Traceability()
            {
                Id = int.Parse(reader["Id"].ToString()),
                Origin = locationDAL.GetById(int.Parse(reader["IdLocationOrigin"].ToString())),
                Destination = locationDAL.GetById(int.Parse(reader["IdLocationDestination"].ToString())),
                User = userDAL.GetById(int.Parse(reader["IdUser"].ToString())),
                Date = DateTime.Parse(reader["Date"].ToString()),
                MovementType = new MovementType()
                {
                    Id = int.Parse(reader["IdMovementType"].ToString()),
                    Name = reader["MovementType"].ToString()
                },
                ItemStatus = new ItemStatus()
                {
                    Id = int.Parse(reader["IdItemStatus"].ToString()),
                    Name = reader["ItemStatusName"].ToString()
                },
                Item = itemDAL.Get(id: int.Parse(reader["IdItem"].ToString()))

            };
        }
    }
}