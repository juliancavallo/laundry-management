using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class ShippingBLL
    {
        private ShippingMapper mapper;
        private ShippingDAL dal;
        private ItemDAL itemDAL;
        private TraceabilityBLL traceabilityBLL;

        public ShippingBLL()
        {
            this.dal = new ShippingDAL();
            this.itemDAL = new ItemDAL();
            this.traceabilityBLL = new TraceabilityBLL();
            this.mapper = new ShippingMapper();
        }

        public IEnumerable<ShippingDTO> GetByType(ShippingTypeEnum shippingType)
        {
            var list = dal.GetByType(shippingType);
            return list.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public List<ShippingViewDTO> GetByTypeForView(ShippingFilter filter)
        {
            var result = this.GetByType(filter.ShippingType);

            if (filter.DateFrom != DateTime.MinValue)
                result = result.Where(x => x.CreatedDate > filter.DateFrom);

            if (filter.DateTo != DateTime.MinValue)
                result = result.Where(x => x.CreatedDate < filter.DateTo);

            return result
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }

        public List<ShippingDetailViewDTO> GetDetailForView(int shippingId)
        {
            var detail = dal.GetDetailByShippingId(shippingId);
            var listView = detail.Select(x => mapper.MapToViewDTO(x)).ToList();

            var result = new List<ShippingDetailViewDTO>();
            foreach (var item in listView)
            {
                result.AddOrUpdate(item);
            }
            return result;
        }

        public void Save(ShippingDTO shipping) 
        { 
            dal.Save(mapper.MapToEntity(shipping));

            var itemIds = shipping.ShippingDetail.Select(x => x.Item.Id).ToList();
            itemDAL.UpdateStatus(itemIds, (int)ItemStatus.ItemStatusByShippingStatus[shipping.Status]);

            var traceabilityList = shipping.ShippingDetail.Select(x => new TraceabilityDTO()
            {
                Date = DateTime.Now,
                Destination = shipping.Destination,
                Origin = shipping.Origin,
                Item = x.Item,
                User = shipping.CreationUser,
                ItemStatus = ItemStatus.ItemStatusByShippingStatus[shipping.Status],
                MovementType = MovementType.MovementByShippingType[shipping.Type],
            }).ToList();

            traceabilityBLL.Save(traceabilityList);

            if (shipping.Type == ShippingTypeEnum.ToClinic)
                itemDAL.UpdateWashes(itemIds);
        }

        public List<ShippingDetailViewDTO> MapToView(List<ShippingDetailDTO> shippingDetailDTO)
        {
            var result = new List<ShippingDetailViewDTO>();
            foreach(var item in shippingDetailDTO)
            {
                result.AddOrUpdate(mapper.MapToViewDTO(item));
            }
            return result;
        }
    }
}
