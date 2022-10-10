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
        private UserMapper userMapper;
        private ItemMapper itemMapper;

        public ShippingMapper()
        {
            locationMapper = new LocationMapper();
            userMapper = new UserMapper();  
            itemMapper = new ItemMapper();
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
                Type = new ShippingType() { Id = (int)dto.Type },
                ShippingDetail = dto.ShippingDetail.Select(x => new ShippingDetail()
                {
                    Item = itemMapper.MapToEntity(x.Item)
                }).ToList(),
                CreationUser = userMapper.MapToEntity(dto.CreationUser),
                Responsible = userMapper.MapToEntity(dto.Responsible),
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
                    Item = itemMapper.MapToDTO(x.Item)
                }).ToList(),
                Responsible = userMapper.MapToDTO(entity.Responsible),
                CreationUser = userMapper.MapToDTO(entity.CreationUser)
            };
        }

        public ShippingViewDTO MapToViewDTO(ShippingDTO dto)
        {
            return new ShippingViewDTO()
            {
                Number = dto.Id.ToString(),
                CreatedDate = dto.CreatedDate.ToString(),
                Destination = dto.Destination.Name,
                Origin = dto.Origin.Name,
                Status = dto.StatusName,
                Id = dto.Id,
                Responsible = dto.Responsible.FullName
            };
        }

        public ProcessDetailViewDTO MapToViewDTO(ShippingDetailDTO dto)
        {
            return new ProcessDetailViewDTO()
            {
                ArticleId = dto.Item.Article.Id,
                Color = dto.Item.Article.Color.Name,
                ItemType = dto.Item.Article.Type.Name,
                Size = dto.Item.Article.Size.Name,
                Quantity = 1
            };
        }

        public ProcessDetailViewDTO MapToViewDTO(ShippingDetail entity)
        {
            return new ProcessDetailViewDTO()
            {
                ArticleId = entity.Item.Article.Id,
                Color = entity.Item.Article.Color.Name,
                ItemType = entity.Item.Article.Type.Name,
                Size = entity.Item.Article.Size.Name,
                Quantity = 1
            };
        }

        public RoadmapShippingViewDTO MapToRoadmapViewDTO(ShippingDTO dto)
        {
            return new RoadmapShippingViewDTO()
            {
                Number = dto.Id.ToString(),
                CreatedDate = dto.CreatedDate.ToString(),
                Id = dto.Id,
                Selected = false
            };
        }
    }
}
