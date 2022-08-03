using LaundryManagement.BLL.Mappers;
using LaundryManagement.BLL.Validators;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
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
        private ItemStatusDAL itemStatusDAL;
        private ItemMapper itemMapper;
        private ItemValidator itemValidator;

        public ItemBLL()
        {
            itemMapper = new ItemMapper();
            itemDAL = new ItemDAL();
            itemStatusDAL = new ItemStatusDAL();
            itemValidator = new ItemValidator();
        }

        public ItemDTO GetByCode(string code)
        {
            var item = itemDAL.Get(code: code);
            if (item == null)
                throw new ValidationException("The item does not exists", ValidationType.Info);
            return itemMapper.MapToDTO(item);
        }

        public List<ItemViewDTO> GetByFilter(ItemFilter filter)
        {
            var items = this.itemDAL.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Code))
                items = items.Where(x => x.Code.Contains(filter.Code));

            if (filter.LocationDTO != null)
                items = items.Where(x => x.Location.Id == filter.LocationDTO.Id || filter.LocationDTO.Id == 0);

            if (filter.ItemStatus != null)
                items = items.Where(x => x.ItemStatus.Id == (int)filter.ItemStatus || (int)filter.ItemStatus == 0);

            if (filter.ItemType != null)
                items = items.Where(x => x.Article.Type.Id == filter.ItemType.Id || filter.ItemType.Id == 0);


            return items
                .Select(x => new ItemViewDTO()
                {
                    Code = x.Code,
                    Article = x.Article.Name,
                    ItemType = x.Article.Type.Name,
                    Location = x.Location.Name,
                    Status = Session.Translations[x.ItemStatus.Name],
                    Washes = x.Washes
                })
                .ToList();
        }

        public List<ItemStatusDTO> GetAllItemStatus()
        {
            return itemStatusDAL.GetAll().Select(x => new ItemStatusDTO()
            {
                Id = x.Id,
                Name = Session.Translations[x.Name],
            }).ToList();
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
