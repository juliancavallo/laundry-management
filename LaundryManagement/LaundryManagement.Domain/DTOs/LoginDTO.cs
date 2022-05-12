using LaundryManagement.Interfaces.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class LoginDTO : ILoginDTO
    {
        public LoginDTO(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
