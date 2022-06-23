using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class UserDTO : IUserDTO
    {
        public UserDTO()
        {
            Permissions = new List<IComponentDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public IList<IComponentDTO> Permissions { get; set; }
        public ILanguage Language { get; set; }

        public string FullName { get { return Name + " " + LastName; } }
    }
}
