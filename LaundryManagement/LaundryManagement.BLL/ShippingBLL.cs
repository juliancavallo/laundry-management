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
    public class ShippingBLL
    {
        private ShippingMapper mapper;
        private ShippingDAL dal;

        public ShippingBLL()
        {
            this.dal = new ShippingDAL();
            this.mapper = new ShippingMapper();
        }

        public List<ShippingDTO> GetByType(ShippingTypeEnum shippingType)
        {
            var list = dal.GetByType(shippingType);
            return list.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public List<ShippingViewDTO> GetByTypeForView(ShippingTypeEnum shippingType)
        {
            return this.GetByType(shippingType)
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }

        public void Save(ShippingDTO shipping) => dal.Save(mapper.MapToEntity(shipping));
    }
}
