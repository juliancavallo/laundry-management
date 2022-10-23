using LaundryManagement.Domain.DataAnnotations;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class User : ICheckDigitEntity
    {
        public User()
        {
            _permissions = new List<Component>();
        }
        private List<Component> _permissions { get; set; }

        public int Id { get; set; }
        [IntegrityProperty]
        public string FirstName { get; set; }
        [IntegrityProperty]
        public string LastName { get; set; }
        [IntegrityProperty]
        public string Email { get; set; }
        [IntegrityProperty]
        public string Password { get; set; }
        [IntegrityProperty]
        public string UserName { get; set; }
        public byte[] CheckDigit { get; set; }
        public List<Component> Permissions { get { return _permissions; } set { this._permissions = value; } }
        public Language Language { get; set; }
        public Location Location { get; set; }


        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
