using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class TranslationViewDTO
    {
        public int IdTranslation { get; set; }
        public int IdTag { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
    }
}
