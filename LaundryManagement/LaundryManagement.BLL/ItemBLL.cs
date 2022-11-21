using LaundryManagement.BLL.IO;
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
        private JsonImportBLL jsonImportBLL;

        public ItemBLL()
        {
            itemMapper = new ItemMapper();
            itemDAL = new ItemDAL();
            itemStatusDAL = new ItemStatusDAL();
            itemValidator = new ItemValidator();
            jsonImportBLL = new JsonImportBLL();
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
                .Select(x => this.itemMapper.MapToViewDTO(x))
                .ToList();
        }

        public List<EnumTypeDTO> GetAllItemStatus()
        {
            return itemStatusDAL.GetAll().Select(x => new EnumTypeDTO()
            {
                Id = x.Id,
                Name = Session.Translations[x.Name],
            }).ToList();
        }

        public void ApplyFormatValidation(string code) =>
            itemValidator.FormatValidation(code);

        public void UpdateWashes(IList<int> list) => 
            itemDAL.UpdateWashes(list);

        public List<ItemViewDTO> ImportStockFromJson(string json)
        {
            var result = jsonImportBLL.Import<List<ItemImportDTO>>(json);

            if(result.Count == 0 || result.All(x => x.Article == null))
                throw new ValidationException("Error converting file", ValidationType.Error);

            var codes = itemDAL.Import(result);

            return this.itemDAL
                .GetAll()
                .Where(x => codes.Contains(x.Code))
                .Select(x => this.itemMapper.MapToViewDTO(x))
                .ToList();
        }
    }
}
