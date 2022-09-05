using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public MovementType MovementType { get; set; }
        public string Message { get; set; }
    }
}
