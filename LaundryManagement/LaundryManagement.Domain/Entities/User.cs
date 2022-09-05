using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class User
    {
        public User()
        {
            _permissions = new List<Component>();
        }
        private List<Component> _permissions { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<Component> Permissions { get { return _permissions; } set { this._permissions = value; } }
        public Language Language { get; set; }
        public Location Location { get; set; }


        public string FullName { get { return Name + " " + LastName; } }
    }
}
