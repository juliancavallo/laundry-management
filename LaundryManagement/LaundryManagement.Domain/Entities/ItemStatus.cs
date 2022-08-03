using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class ItemStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static Dictionary<ShippingStatusEnum, ItemStatusEnum> ItemStatusByShippingStatus
        {
            get
            {
                return new Dictionary<ShippingStatusEnum, ItemStatusEnum>()
                {
                    { ShippingStatusEnum.Created, ItemStatusEnum.PendingSent },
                    { ShippingStatusEnum.Sent, ItemStatusEnum.Sent },
                    { ShippingStatusEnum.Received, ItemStatusEnum.OnLocation }
                };
            }
        }
    }
}
