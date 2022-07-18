using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class ItemTypeBLL : ICrud<ItemTypeDTO>
    {
        private ItemTypeMapper mapper;
        private ItemTypeDAL dal;

        public ItemTypeBLL()
        {
            this.dal = new ItemTypeDAL();
            this.mapper = new ItemTypeMapper();
        }

        public void Delete(ItemTypeDTO dto)
        {
            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);
        }

        public IList<ItemTypeDTO> GetAll()
        {
            var list = this.dal.GetAll();

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public ItemTypeDTO GetById(int id)
        {
            var entity = this.dal.GetById(id);
            return mapper.MapToDTO(entity);
        }

        public void Save(ItemTypeDTO dto)
        {
            var entity = mapper.MapToEntity(dto);
            this.dal.Save(entity);
        }
    }
}
