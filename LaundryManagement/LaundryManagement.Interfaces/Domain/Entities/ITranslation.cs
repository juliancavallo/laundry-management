using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface ITranslation
    {
        ITag Tag { get; set; }
        string Text { get; }
    }
}
