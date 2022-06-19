using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface ITranslation
    {
        public int Id { get; set; }
        ITag Tag { get; set; }
        string Text { get; }
        public int IdLanguage { get; set; }
    }
}
