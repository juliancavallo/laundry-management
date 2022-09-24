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

            connection.ConnectionString = Session.Settings.ConnectionString;
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

    }
}
