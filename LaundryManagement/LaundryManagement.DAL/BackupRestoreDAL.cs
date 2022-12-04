using LaundryManagement.DAL.Scripts;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.DAL
{
    public class BackupRestoreDAL
    {
        private SqlConnection connection;
        public BackupRestoreDAL()
        {
            connection = new SqlConnection();

            connection.ConnectionString = Session.Settings.DatabaseSettings.ConnectionString;
		}

        public void Backup()
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"BACKUP DATABASE LaundryManagement TO DISK = 'C:/Backups/{DateTime.Now.ToString("yyyyMMddhhmmss")}.bak'";

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

        public void Restore(string backupPath)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $@"
                    USE [master];
                    ALTER DATABASE LaundryManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    RESTORE DATABASE LaundryManagement FROM DISK = '{backupPath}' WITH RECOVERY, REPLACE;
                ";

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

        public void CreateIfNotExists()
        {
            SqlConnection masterConnection = new SqlConnection();
            try
            {
                masterConnection = new SqlConnection($"server={Session.Settings.DatabaseSettings.Server};Trusted_Connection=yes");
                masterConnection.Open();
                
                var cmd = new SqlCommand(
                    $"SELECT database_id FROM sys.databases WHERE Name = '{Session.Settings.DatabaseSettings.Database}'", 
                    masterConnection);

                object result = cmd.ExecuteScalar();
                int databaseId = 0;

                if(result != null)
                    int.TryParse(result.ToString(), out databaseId);

                if(databaseId == 0)
                {
                    var scripts = CreateDatabaseScript.Content.Split("GO", StringSplitOptions.RemoveEmptyEntries);

                    foreach(var script in scripts)
                    {
                        var createCmd = new SqlCommand(script, masterConnection);
                        createCmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                masterConnection.Close();
            }
        }

    }
}
