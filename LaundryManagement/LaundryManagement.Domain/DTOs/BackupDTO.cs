using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class BackupDTO
    {
        public DateTime BackupTime { get; set; }
        public string BackupPath { get; set; }
    }
}
