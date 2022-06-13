using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface ILanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
    }
}
