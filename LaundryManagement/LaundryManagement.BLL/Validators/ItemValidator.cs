using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Validators
{
    public class ItemValidator
    {
        public void StatusValidation(ItemStatusEnum status)
        {
            if (status != ItemStatusEnum.OnLocation)
                throw new ValidationException("The item is not in a valid state", ValidationType.Warning);
        }

        public void LocationValidation(ItemDTO item, LocationDTO shippingLocation)
        {
            if (item.ItemStatus == ItemStatusEnum.OnLocation && !item.Location.Equals(shippingLocation))
                throw new ValidationException("The item is not in the current location", ValidationType.Warning);
        }

        public void WashesValidation(ItemDTO item, ValidationResponseDTO validationResponse)
        {
            if (item.Washes == 1)
                validationResponse.Messages.Add("The item has only one wash left. Consider replacing it as soon as posible");
        }
    }
}
