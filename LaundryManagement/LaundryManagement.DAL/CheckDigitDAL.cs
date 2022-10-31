using LaundryManagement.Domain.DataAnnotations;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.DAL
{
    public class CheckDigitDAL
    {
        private SqlConnection connection;

        public CheckDigitDAL()
        {
            connection = new SqlConnection();

            connection.ConnectionString = Session.Settings.ConnectionString;
        }

        public IList<ICheckDigitEntity> GetAllRows(Type type, IEnumerable<string> allowedPropertyNames)
        {
            SqlDataReader reader = null;
            try
            {
                var properties = type.GetProperties()
                    .Where(x => Attribute.IsDefined(x, typeof(IntegrityProperty)) || allowedPropertyNames.Contains(x.Name))
                    .ToList();

                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand($@"SELECT * FROM [{type.Name}]");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                var list = new List<ICheckDigitEntity>();
                while (reader.Read())
                {
                    ICheckDigitEntity obj = Activator.CreateInstance(type) as ICheckDigitEntity;
                    
                    foreach(var property in properties)
                    {
                        obj.GetType().GetProperty(property.Name).SetValue(obj, reader[property.Name]);
                    }

                    list.Add(obj);
                }

                return list;
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

        public IEnumerable<VerticalCheckDigit> GetAllVerticalCheckDigits()
        {
            SqlDataReader reader = null;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand($@"SELECT * FROM [VerticalCheckDigit]");

                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                var list = new List<VerticalCheckDigit>();
                while (reader.Read())
                {
                    list.Add(new VerticalCheckDigit()
                    {
                        CheckDigit = (byte[])reader["CheckDigit"],
                        TableName = reader["TableName"].ToString()
                    });
                }

                return list;
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

        public void UpdateHorizontalCheckDigit(ICheckDigitEntity entity, byte[] checkDigit)
        {
            try
            {
                var id = int.Parse(entity.GetType().GetProperty("Id").GetValue(entity).ToString());

                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand($@"UPDATE [{entity.GetType().Name}] SET CheckDigit = @CheckDigit WHERE Id = {id}");
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@CheckDigit", checkDigit);
                sqlParam.DbType = System.Data.DbType.Binary;

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

        public void SaveVerticalCheckDigit(Type entityType, byte[] checkDigit)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand($@"
                    IF((SELECT 1 FROM [VerticalCheckDigit] WHERE TableName = '{entityType.Name}') IS NULL)
	                    INSERT INTO [VerticalCheckDigit] (TableName, CheckDigit)
                            VALUES ('{entityType.Name}', @CheckDigit)
                    ELSE
                        UPDATE [VerticalCheckDigit] 
                        SET CheckDigit = @CheckDigit 
                        WHERE TableName = '{entityType.Name}'");

                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@CheckDigit", checkDigit);
                sqlParam.DbType = System.Data.DbType.Binary;

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

        public void DeleteVerticalCheckDigit(Type entityType)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                SqlCommand cmd = new SqlCommand($@"DELETE [VerticalCheckDigit] WHERE TableName = '{entityType.Name}'");

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
    }
}
