using LaundryManagement.BLL.Mappers;
using LaundryManagement.BLL.Validators;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
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
        private EmailService emailService;
        private ItemValidator itemValidator;

        public ShippingBLL()
        {
            this.dal = new ShippingDAL();
            this.itemDAL = new ItemDAL();
            this.traceabilityBLL = new TraceabilityBLL();
            this.mapper = new ShippingMapper();
            this.emailService = new EmailService();
            itemValidator = new ItemValidator();
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
            //Shipping
            int id = dal.Save(mapper.MapToEntity(shipping));
            shipping.Id = id;

            //Item Status
            var itemIds = shipping.ShippingDetail.Select(x => x.Item.Id).ToList();
            itemDAL.UpdateStatus(itemIds, (int)ItemStatus.ItemStatusByShippingStatus[shipping.Status], shipping.Destination.Id);

            //Traceability
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

            //Washes
            if (shipping.Type == ShippingTypeEnum.ToClinic)
                itemDAL.UpdateWashes(itemIds);

            //Send email
            SendEmail(shipping);
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

        public ValidationResponseDTO ApplyValidationForShipping(ItemDTO item, ShippingTypeEnum shippingType, LocationDTO originLocation)
        {
            var result = new ValidationResponseDTO();

            itemValidator.StatusValidation(item.ItemStatus);
            itemValidator.LocationValidation(item, originLocation);

            switch (shippingType)
            {
                case ShippingTypeEnum.ToClinic:
                    itemValidator.WashesValidation(item, result);
                    break;

                case ShippingTypeEnum.Internal:
                    if (originLocation.LocationType == Interfaces.Enums.ILocationType.Laundry)
                        itemValidator.WashesValidation(item, result);
                    break;
            }

            return result;
        }

        private void SendEmail(ShippingDTO shipping)
        {
            var statusName = dal.GetStatusName((int)shipping.Status);
            var message = string.Format(Session.Translations[Tags.ShippingEmailBody],
                shipping.Id, Session.Translations[statusName], DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), Session.Instance.User.FullName);
            
            emailService.SendMail(
                shipping.Responsible.Email,
                $"{Session.Translations["Shipping"]} {shipping.Id} - { Session.Translations[statusName] }",
                message);
        }
    }
}
