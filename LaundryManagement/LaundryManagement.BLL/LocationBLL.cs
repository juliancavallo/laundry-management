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

        public IList<LocationDTO> GetAllByType(LocationType locationType, bool isInternal)
        {
            var list = this.dal.GetAll()
                .Where(x => x.LocationType == locationType && x.IsInternal == isInternal)
                .ToList();

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

        public LocationType GetByShippingType(ShippingTypeEnum shippingType, bool origin)
        {
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
