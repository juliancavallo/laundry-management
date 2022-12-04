using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class BackupRestoreBLL
    {
        private readonly BackupRestoreDAL dal;
        private readonly LogBLL logBLL;

        public BackupRestoreBLL()
        {
            this.dal = new BackupRestoreDAL();
            this.logBLL = new LogBLL();
        }

        public void Backup()
        {
            Directory.CreateDirectory(Session.Settings.BackupPath);

            var files = new DirectoryInfo(Session.Settings.BackupPath).GetFiles().OrderBy(x => x.CreationTime).ToList();

            while (files.Count > Session.Settings.BackupsLimit - 1)
            {
                File.Delete(files.First().FullName);
                files.Remove(files.First());
            }

            dal.Backup();

            logBLL.LogInfo(MovementTypeEnum.Backup, $"The user {Session.Instance.User.FullName} has made a backup of the database");
        }

        public void Restore(string path)
        {
            dal.Restore(path);
            logBLL.LogInfo(MovementTypeEnum.Restore, $"The user {Session.Instance?.User?.FullName ?? "-No user-"} has restored the backup from {path}");
        }

        public IEnumerable<BackupDTO> GetBackups()
        {
            if (!Directory.Exists(Session.Settings.BackupPath))
                Directory.CreateDirectory(Session.Settings.BackupPath);

            var files = new DirectoryInfo(Session.Settings.BackupPath).GetFiles().OrderByDescending(x => x.CreationTime).ToList();

            return files.Select(x => new BackupDTO()
            {
                BackupTime = x.CreationTime,
                BackupPath = x.FullName
            });
        }

        public void CreateIfNotExists() =>
            dal.CreateIfNotExists();
    }
}
