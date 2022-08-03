using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.DTOs
{
    public interface IUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; }
        public IList<IComponentDTO> Permissions { get; set; }
        public ILanguage Language { get; set; }
    }
}
