using LaundryManagement.BLL.Mappers;
using LaundryManagement.BLL.Validators;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class ItemBLL
    {
        private ItemDAL itemDAL;
        private ItemMapper itemMapper;
        private ItemValidator itemValidator;

        public ItemBLL()
        {
            itemMapper = new ItemMapper();
            itemDAL = new ItemDAL();
            itemValidator = new ItemValidator();
        }


        public ItemDTO GetByCode(string code)
        {
            var item = itemDAL.Get(code: code);
            if (item == null)
                throw new ValidationException("The item does not exists", ValidationType.Info);
            return itemMapper.MapToDTO(item);
        }

        public void Save(ItemDTO dto) => itemDAL.Save(itemMapper.MapToEntity(dto));

        public ValidationResponseDTO ApplyValidationForShipping(ItemDTO item, ShippingTypeEnum shippingType, LocationDTO originLocation)
        {
            var result = new ValidationResponseDTO();            

            switch (shippingType)
            {
                case ShippingTypeEnum.ToLaundry:
                    itemValidator.StatusValidation(item.ItemStatus);
                    itemValidator.LocationValidation(item, originLocation);
                    break;

                case ShippingTypeEnum.ToClinic:
                    itemValidator.StatusValidation(item.ItemStatus);
                    itemValidator.LocationValidation(item, originLocation);
                    itemValidator.WashesValidation(item, result);
                    break;
            }

            return result;
        }
    }
}
