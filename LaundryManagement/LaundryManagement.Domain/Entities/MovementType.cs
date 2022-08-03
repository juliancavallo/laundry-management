using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class MovementType
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static Dictionary<ShippingTypeEnum, MovementTypeEnum> MovementByShippingType
        {
            get
            {
                return new Dictionary<ShippingTypeEnum, MovementTypeEnum>()
                {
                    { ShippingTypeEnum.ToLaundry, MovementTypeEnum.LaundryShipping },
                    { ShippingTypeEnum.ToClinic, MovementTypeEnum.ClinicShipping },
                    { ShippingTypeEnum.Internal, MovementTypeEnum.InternalShipping },
                };
            }
        }
    }
}
