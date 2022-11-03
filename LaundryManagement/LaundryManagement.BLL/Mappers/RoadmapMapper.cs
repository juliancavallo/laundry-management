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
    public class RoadmapMapper
    {
        private LocationMapper locationMapper;
        private UserMapper userMapper;
        private ItemMapper itemMapper;

        public RoadmapMapper()
        {
            locationMapper = new LocationMapper();
            userMapper = new UserMapper();  
            itemMapper = new ItemMapper();
        }

        public Roadmap MapToEntity(RoadmapDTO dto)
        {
            return new Roadmap()
            {
                Id = dto.Id,
                CreatedDate = dto.CreatedDate,
                Destination = locationMapper.MapToEntity(dto.Destination),
                Origin = locationMapper.MapToEntity(dto.Origin),
                Status = new RoadmapStatus() { Id = (int)dto.Status },
                Shippings = dto.Shippings.Select(x => new Shipping() { Id = x.Id }).ToList(),
                CreationUser = userMapper.MapToEntity(dto.CreationUser),
            };
        }

        public RoadmapDTO MapToDTO(Roadmap entity)
        {
            return new RoadmapDTO()
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                Destination = locationMapper.MapToDTO(entity.Destination),
                Origin = locationMapper.MapToDTO(entity.Origin),
                Status = (RoadmapStatusEnum)entity.Status.Id,
                StatusName = entity.Status.Name,
                Shippings = entity.Shippings.Select(x => new ShippingDTO()
                {
                    Id = x.Id,   
                    ShippingDetail = x.ShippingDetail.Select(d => new ShippingDetailDTO()
                    {
                        Item = itemMapper.MapToDTO(d.Item)
                    }).ToList()
                }).ToList(),
                CreationUser = userMapper.MapToDTO(entity.CreationUser)
            };
        }

        public RoadmapViewDTO MapToViewDTO(RoadmapDTO dto)
        {
            return new RoadmapViewDTO()
            {
                Number = dto.Id.ToString(),
                CreatedDate = dto.CreatedDate.ToString(),
                Destination = dto.Destination.Name,
                Origin = dto.Origin.Name,
                Status = dto.StatusName,
                Id = dto.Id
            };
        }
    }
}
