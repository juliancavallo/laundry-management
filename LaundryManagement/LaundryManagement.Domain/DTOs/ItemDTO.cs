using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
        public ArticleDTO Article { get; set; }
        public ItemStatusEnum ItemStatus { get; set; }
    }
}
