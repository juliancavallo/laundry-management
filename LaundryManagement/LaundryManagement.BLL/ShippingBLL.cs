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
        private ItemBLL itemBLL;
        private TraceabilityBLL traceabilityBLL;
        private LocationBLL locationBLL;
        private EmailService emailService;
        private ItemValidator itemValidator;

        public ShippingBLL()
        {
            this.dal = new ShippingDAL();
            this.itemBLL = new ItemBLL();
            this.traceabilityBLL = new TraceabilityBLL();
            this.locationBLL = new LocationBLL();
            this.mapper = new ShippingMapper();
            this.emailService = new EmailService();
            itemValidator = new ItemValidator();
        }

        public List<RoadmapShippingViewDTO> GetForRoadMapView(ShippingFilter filter) =>
            this.GetByFilter(filter).Select(x => mapper.MapToRoadmapViewDTO(x)).ToList();

        public List<ShippingViewDTO> GetForView(ShippingFilter filter) =>
            this.GetByFilter(filter).Select(x => mapper.MapToViewDTO(x)).ToList();


        public List<ShippingDTO> GetByFilter(ShippingFilter filter)
        {
            IEnumerable<Shipping> result;

            if (filter.ShippingType.HasValue)
                result = dal.GetByType(filter.ShippingType.Value);
            else
                result = dal.GetAll();

            if (filter.DateFrom != DateTime.MinValue)
                result = result.Where(x => x.CreatedDate > filter.DateFrom);

            if (filter.DateTo != DateTime.MinValue)
                result = result.Where(x => x.CreatedDate < filter.DateTo);

            if(filter.Origin != null)
                result = result.Where(x => x.Origin.Equals(filter.Origin) || filter.Origin.IsChild(x.Origin.ParentLocation));

            if (filter.Destination != null)
                result = result.Where(x => x.Destination.Equals(filter.Destination));

            if (filter.ShippingStatus.HasValue)
                result = result.Where(x => x.Status.Id == (int)filter.ShippingStatus);

            if (filter.ShippingIds != null)
                result = result.Where(x => filter.ShippingIds.Contains(x.Id));

            return result
                .Select(x => mapper.MapToDTO(x))
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
            dal.UpdateItems((int)ItemStatus.ItemStatusByShippingStatus[shipping.Status], shipping.Destination.Id, id);

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
            var itemIds = shipping.ShippingDetail.Select(x => x.Item.Id).ToList();
            if (shipping.Type == ShippingTypeEnum.ToClinic)
                itemBLL.UpdateWashes(itemIds);

            //Send email
            SendEmail(shipping);
        }

        public void Send(List<ShippingDTO> shippings)
        {
            foreach (var shipping in shippings)
            {
                shipping.Status = ShippingStatusEnum.Sent;
                dal.Save(mapper.MapToEntity(shipping));
            }
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

        public string GetStatusName(int idStatus) => 
            dal.GetStatusName(idStatus);

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


        public IList<LocationDTO> GetShippingOriginByShippingType(ShippingTypeEnum shippingType)
        {
            var userLocation = Session.Instance.User.Location;
            var locationOriginType = this.GetLocationTypeByShippingType(shippingType, true);

            var list = locationBLL.GetAll();

            switch (shippingType)
            {
                case ShippingTypeEnum.ToLaundry:
                case ShippingTypeEnum.ToClinic:
                    list = list.Where(x => (LocationType)x.LocationType == locationOriginType && x.Equals(userLocation) && !x.IsInternal).ToList();
                    break;

                case ShippingTypeEnum.Internal:
                    list = list.Where(x => x.Equals(userLocation) || (x.IsChild(userLocation))).ToList();
                    break;
            }

            return list;
        }

        public IList<LocationDTO> GetShippingDestinationByShippingType(ShippingTypeEnum shippingType)
        {
            var userLocation = Session.Instance.User.Location;
            var locationDestinationType = this.GetLocationTypeByShippingType(shippingType, false);

            var list = locationBLL.GetAll();

            switch (shippingType)
            {
                case ShippingTypeEnum.ToLaundry:
                case ShippingTypeEnum.ToClinic:
                    list = list.Where(x => (LocationType)x.LocationType == locationDestinationType && !x.IsInternal).ToList();
                    break;

                case ShippingTypeEnum.Internal:
                    list = list.Where(x => x.LocationType == userLocation.LocationType
                    && (x.IsChild(userLocation) || userLocation.IsChild(x) || x.IsChild(x.ParentLocation))).ToList();
                    break;
            }

            return list;
        }

        internal LocationType GetLocationTypeByShippingType(ShippingTypeEnum shippingType, bool origin)
        {
            var userLocation = Session.Instance.User.Location;

            if (shippingType == ShippingTypeEnum.Internal)
                return (LocationType)userLocation.LocationType;

            var dictionaryOrigin = new Dictionary<ShippingTypeEnum, LocationType>()
            {
                {ShippingTypeEnum.ToLaundry, LocationType.Clinic},
                {ShippingTypeEnum.ToClinic, LocationType.Laundry},
            };

            var dictionaryDestination = new Dictionary<ShippingTypeEnum, LocationType>()
            {
                {ShippingTypeEnum.ToLaundry, LocationType.Laundry},
                {ShippingTypeEnum.ToClinic, LocationType.Clinic},
            };

            return origin ? dictionaryOrigin[shippingType] : dictionaryDestination[shippingType];

        }
    }
}
