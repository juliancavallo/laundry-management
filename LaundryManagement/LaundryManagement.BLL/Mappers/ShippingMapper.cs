using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class ShippingMapper
    {
        private LocationMapper locationMapper;

        public ShippingMapper()
        {
            locationMapper = new LocationMapper();
        }

        public Shipping MapToEntity(ShippingDTO dto)
        {
            return new Shipping()
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                Destination = locationMapper.MapToEntity(dto.Destination),
                Origin = locationMapper.MapToEntity(dto.Origin),
                Status = new ShippingStatus() { Id = (int)dto.Status },
                Type = new ShippingType() { Id = (int)dto.Status },
                ShippingDetail = dto.ShippingDetail.Select(x => new ShippingDetail()
                {
                    Item = new Item()
                    {
                        Id = x.Item.Id,
                    }
                }).ToList(),
            };
        }

        public ShippingDTO MapToDTO(Shipping entity)
        {
            return new ShippingDTO()
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                Destination = locationMapper.MapToDTO(entity.Destination),
                Origin = locationMapper.MapToDTO(entity.Origin),
                Status = (ShippingStatusEnum)entity.Status.Id,
                StatusName = entity.Status.Name,
                Type = (ShippingTypeEnum)entity.Type.Id,
                TypeName = entity.Type.Name,
                ShippingDetail = entity.ShippingDetail.Select(x => new ShippingDetailDTO()
                {
                    Item = new ItemDTO()
                    {
                        Id = x.Item.Id,
                    }
                }).ToList(),
            };
        }

        public ShippingViewDTO MapToViewDTO(ShippingDTO dto)
        {
            return new ShippingViewDTO()
            {
                CreatedDate = dto.CreatedDate.ToString(),
                Destination = dto.Destination.Name,
                Origin = dto.Origin.Name,
                Type = dto.TypeName,
                Status = dto.StatusName,
                Id = dto.Id,
            };
        }
    }
}
