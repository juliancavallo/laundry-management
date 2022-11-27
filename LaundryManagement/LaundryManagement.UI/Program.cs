using LaundryManagement.BLL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Services;
using LaundryManagement.UI.Forms.Integrity;
using Microsoft.Extensions.Configuration;
using System;
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
                var appPath = AppDomain.CurrentDomain.BaseDirectory;
                var path = Path.Combine(appPath, "appsettings.json");
                var builder = new ConfigurationBuilder()
                  .AddJsonFile(path, optional: true, reloadOnChange: true);
                Configuration = builder.Build();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SetSettings();
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
            Session.Settings = new SettingsDTO()
            {
                ConnectionString = Program.Configuration.GetSection("ConnectionString").Value.ToString(),
                ImagePath = Program.Configuration.GetSection("ImagePath").Value.ToString(),
                PasswordPolicy = Program.Configuration.GetSection("PasswordPolicy").Get<PasswordPolicy>(),
                EmailSettings = Program.Configuration.GetSection("EmailSettings").Get<EmailSettings>(),
                ReportsPath = Program.Configuration.GetSection("ReportsPath").Value.ToString(),
                BackupPath = Program.Configuration.GetSection("BackupPath").Value.ToString(),
                LogLevel = int.Parse(Program.Configuration.GetSection("LogLevel").Value),
                BackupsLimit = int.Parse(Program.Configuration.GetSection("BackupsLimit").Value),
                IntegrityAdminUser = Program.Configuration.GetSection("IntegrityAdminUser").Value.ToString(),
                IntegrityAdminPassword = Program.Configuration.GetSection("IntegrityAdminPassword").Value.ToString(),
            };
        }

        private static void CheckIntegrity()
        {
            var checkDigitBLL = new CheckDigitBLL();
            var horizontalCorruptedEntities = checkDigitBLL.GetHorizontalCorruptedEntities();
            var verticalCorruptedEntities = checkDigitBLL.GetVerticalCorruptedEntities();

            if(horizontalCorruptedEntities.Count() > 0|| verticalCorruptedEntities.Count() > 0)   
                Application.Run(new frmIntegrity(horizontalCorruptedEntities, verticalCorruptedEntities));
        }
    }
}