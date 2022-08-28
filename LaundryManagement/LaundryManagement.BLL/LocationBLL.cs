using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class LocationBLL : ICrud<LocationDTO>
    {
        private LocationMapper mapper;
        private LocationDAL dal;

        public LocationBLL()
        {
            this.dal = new LocationDAL();
            this.mapper = new LocationMapper();
        }

        public void Delete(LocationDTO dto)
        {
            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);
        }

        public IList<LocationDTO> GetAll()
        {
            var list = this.dal.GetAll().ToList();

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public IList<LocationDTO> GetShippingOriginByShippingType(ShippingTypeEnum shippingType)
        {
            var userLocation = Session.Instance.User.Location;
            var locationOriginType = this.GetLocationTypeByShippingType(shippingType, true);

            var list = this.dal.GetAll();

            switch (shippingType)
            {
                case ShippingTypeEnum.ToLaundry:
                case ShippingTypeEnum.ToClinic:
                    list = list.Where(x => x.LocationType == locationOriginType && x.Equals(userLocation) && !x.IsInternal).ToList();
                    break;

                case ShippingTypeEnum.Internal:
                    list = list.Where(x => x.Equals(userLocation) || (x.IsChild(userLocation))).ToList();
                    break;
            }

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public IList<LocationDTO> GetShippingDestinationByShippingType(ShippingTypeEnum shippingType)
        {
            var userLocation = Session.Instance.User.Location;
            var locationDestinationType = this.GetLocationTypeByShippingType(shippingType, false);

            var list = this.dal.GetAll();

            switch (shippingType)
            {
                case ShippingTypeEnum.ToLaundry:
                case ShippingTypeEnum.ToClinic:
                    list = list.Where(x => x.LocationType == locationDestinationType && !x.IsInternal).ToList();
                    break;

                case ShippingTypeEnum.Internal:
                    list = list.Where(x => x.LocationType == (LocationType)userLocation.LocationType 
                    && (x.IsChild(userLocation) || userLocation.IsChild(x) || x.IsChild(x.ParentLocation))).ToList();
                    break;
            }

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public LocationDTO GetById(int id)
        {
            var entity = this.dal.GetById(id);
            return mapper.MapToDTO(entity);
        }

        public void Save(LocationDTO dto)
        {
            var entity = mapper.MapToEntity(dto);
            this.dal.Save(entity);
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
