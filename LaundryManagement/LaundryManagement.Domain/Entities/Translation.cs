using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Translation : ITranslation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ITag Tag { get; set; }
    }
}
