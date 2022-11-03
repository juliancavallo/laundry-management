using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;

namespace LaundryManagement.BLL.Validators
{
    public class ItemValidator
    {
        public void StatusValidation(ItemStatusEnum actualStatus, ItemStatusEnum exceptedStatus)
        {
            if (actualStatus != exceptedStatus)
                throw new ValidationException("The item is not in a valid state", ValidationType.Warning);
        }

        public void LocationValidation(ItemDTO item, LocationDTO location)
        {
            if (!item.Location.Equals(location))
                throw new ValidationException("The item is not in the correct location", ValidationType.Warning);
        }

        public void WashesValidation(ItemDTO item, ValidationResponseDTO validationResponse)
        {
            if (item.Washes == 1)
                validationResponse.Messages.Add("The item has only one wash left. Consider replacing it as soon as posible");

            if (item.Washes < 1)
                throw new ValidationException("The item has no washes left, so it cannot be used anymore", ValidationType.Warning);
        }
        
        public void FormatValidation(string code)
        {
            if (code.Length != 8 || !long.TryParse(code, System.Globalization.NumberStyles.HexNumber, null, out _))
                throw new ValidationException("The code is not in a valid format. It must be 8 characters long and in hexadecimal format", ValidationType.Warning);
        }
    }
}
