using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class LogDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public UserDTO User { get; set; }
        public MovementTypeEnum MovementType { get; set; }
        public string MovementTypeName { get; set; }
        public string Message { get; set; }
        public LogLevelEnum LogLevel { get; set; }
    }
}
