using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using System.Linq;

namespace LaundryManagement.BLL.Mappers
{
    public class ReceptionMapper
    {
        private LocationMapper locationMapper;
        private UserMapper userMapper;
        private ItemMapper itemMapper;

        public ReceptionMapper()
        {
            locationMapper = new LocationMapper();
            userMapper = new UserMapper();
            itemMapper = new ItemMapper();
        }

        public Reception MapToEntity(ReceptionDTO dto)
        {
            return new Reception()
            {
                Id = dto.Id,
                CreatedDate = dto.CreationDate,
                Destination = locationMapper.MapToEntity(dto.Destination),
                Origin = locationMapper.MapToEntity(dto.Origin),
                CreationUser = userMapper.MapToEntity(dto.CreationUser),
                ReceptionDetail = dto.ReceptionDetail.Select(x => new ReceptionDetail()
                {
                    Item = itemMapper.MapToEntity(x.Item)
                }),
                Roadmaps = dto.Roadmaps.Select(x => new Roadmap()
                {
                    Id = x.Id
                })
            };
        }

        public ReceptionDTO MapToDTO(Reception entity)
        {
            return new ReceptionDTO()
            {
                Id = entity.Id,
                CreationDate = entity.CreatedDate,
                Destination = locationMapper.MapToDTO(entity.Destination),
                Origin = locationMapper.MapToDTO(entity.Origin),
                CreationUser = userMapper.MapToDTO(entity.CreationUser),
                ReceptionDetail = entity.ReceptionDetail.Select(x => new ReceptionDetailDTO()
                {
                    Item = itemMapper.MapToDTO(x.Item)
                })
            };
        }

        public ReceptionViewDTO MapToViewDTO(ReceptionDTO dto)
        {
            return new ReceptionViewDTO()
            {
                Number = dto.Id.ToString(),
                CreationDate = dto.CreationDate.ToString(),
                Destination = dto.Destination.Name,
                Origin = dto.Origin.Name,
                Id = dto.Id
            };
        }

        public ReceptionDetailViewDTO MapToViewDTO(ReceptionDetailDTO dto)
        {
            return new ReceptionDetailViewDTO()
            {
                ArticleId = dto.Item.Article.Id,
                Color = dto.Item.Article.Color.Name,
                ItemType = dto.Item.Article.Type.Name,
                Size = dto.Item.Article.Size.Name,
                Quantity = 0,
                ExpectedQuantity = 1
            };
        }
    }
}
