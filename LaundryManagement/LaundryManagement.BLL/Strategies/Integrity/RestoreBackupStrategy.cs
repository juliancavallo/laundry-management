using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Strategies
{
    public class RestoreBackupStrategy : IIntegrityActionStrategy
    {
        private BackupRestoreBLL _backupRestoreBLL;

        public RestoreBackupStrategy() =>
            _backupRestoreBLL = new BackupRestoreBLL();

        public void RestoreIntegrity()
        {
            var backups = _backupRestoreBLL.GetBackups();
            if(backups.Count() > 0)
            {
                var backupToRestore = backups.OrderByDescending(x => x.BackupTime).First();
                _backupRestoreBLL.Restore(backupToRestore.BackupPath);
            }
        }
    }
}
