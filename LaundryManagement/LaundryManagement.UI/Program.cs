using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Services;
using LaundryManagement.UI.Forms.Integrity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    internal static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var path = "appsettings.json".GetRelativePath();
                var builder = new ConfigurationBuilder()
                  .AddJsonFile(path, optional: true, reloadOnChange: true);
                Configuration = builder.Build();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SetSettings();
                CreateDatabase();
                CheckIntegrity();

                Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
                File.Create("Error.txt").Dispose();
                using (StreamWriter sw = File.AppendText("Error.txt"))
                {
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);

                }
            }
        }

        private static void SetSettings()
        {
            try
            {
                Session.Settings = new SettingsDTO()
                {
                    ImagePath = Program.Configuration.GetSection("ImagePath").Value.ToString(),
                    HelperPath = Program.Configuration.GetSection("HelperPath").Value.ToString(),
                    ReportTemplatesPath = Program.Configuration.GetSection("ReportTemplatesPath").Value.ToString(),
                    PasswordPolicy = Program.Configuration.GetSection("PasswordPolicy").Get<PasswordPolicy>(),
                    EmailSettings = Program.Configuration.GetSection("EmailSettings").Get<EmailSettings>(),
                    ReportsPath = Program.Configuration.GetSection("ReportsPath").Value.ToString(),
                    BackupPath = Program.Configuration.GetSection("BackupPath").Value.ToString(),
                    LogLevel = int.Parse(Program.Configuration.GetSection("LogLevel").Value),
                    BackupsLimit = int.Parse(Program.Configuration.GetSection("BackupsLimit").Value),
                    IntegrityAdminUser = Program.Configuration.GetSection("IntegrityAdminUser").Value.ToString(),
                    IntegrityAdminPassword = Program.Configuration.GetSection("IntegrityAdminPassword").Value.ToString(),
                    DatabaseSettings = new DatabaseSettings()
                    {
                        Database = Program.Configuration.GetSection("DatabaseConnection:Database").Value.ToString(),
                        Server = Program.Configuration.GetSection("DatabaseConnection:Server").Value.ToString(),
                        ConnectionString = new SqlConnectionStringBuilder()
                        {
                            DataSource = Program.Configuration.GetSection("DatabaseConnection:Server").Value.ToString(),
                            InitialCatalog = Program.Configuration.GetSection("DatabaseConnection:Database").Value.ToString(),
                            IntegratedSecurity = true
                        }.ConnectionString
                    },
                };
            }
            catch(Exception)
            {
                FormValidation.ShowMessage("An error occurred creating the settings", Domain.Enums.ValidationType.Error);
            }
        }

        private static void CreateDatabase()
        {
            try
            {
                var backupBLL = new BackupRestoreBLL();
                backupBLL.CreateIfNotExists();
            }
            catch (Exception)
            {
                FormValidation.ShowMessage("An error occurred checking the database", Domain.Enums.ValidationType.Error);
            }
        }

        private static void CheckIntegrity()
        {
            try
            {
                var checkDigitBLL = new CheckDigitBLL();
                var horizontalCorruptedEntities = checkDigitBLL.GetHorizontalCorruptedEntities();
                var verticalCorruptedEntities = checkDigitBLL.GetVerticalCorruptedEntities();

                if(horizontalCorruptedEntities.Count() > 0|| verticalCorruptedEntities.Count() > 0)   
                    Application.Run(new frmIntegrity(horizontalCorruptedEntities, verticalCorruptedEntities));
            }
            catch (Exception)
            {
                FormValidation.ShowMessage("An error occurred checking the integrity of the database", Domain.Enums.ValidationType.Error);
            }
        }
    }
}