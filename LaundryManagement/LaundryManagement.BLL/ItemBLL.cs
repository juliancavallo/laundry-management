using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
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

        public ItemBLL()
        {
            itemMapper = new ItemMapper();
            itemDAL = new ItemDAL();
        }

        public ItemDTO GetByCode(string code)
        {
            var item = itemDAL.Get(code: code);
            if (item == null)
                throw new ValidationException("The item does not exists", Domain.Enums.ValidationType.Info);
            return itemMapper.MapToDTO(item);
        }

        public void Save(ItemDTO dto) => itemDAL.Save(itemMapper.MapToEntity(dto));
    }
}
