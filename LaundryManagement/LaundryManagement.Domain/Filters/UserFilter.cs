using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class UserFilter
    {
        public UserFilter(string name = null, string lastname = null, string username = null, string email = null)
        {
            this.Name = name;
            this.UserName = username;
            this.Email = email;
            this.LastName = lastname;
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
