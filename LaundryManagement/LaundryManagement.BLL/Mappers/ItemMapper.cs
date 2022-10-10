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
    public class ItemMapper
    {
        private LocationMapper locationMapper;

        public ItemMapper()
        {
            this.locationMapper = new LocationMapper();
        }

        public Item MapToEntity(ItemDTO dto)
        {
            return new Item()
            {
                Id = dto.Id,
                Code = dto.Code,
                Created = dto.Created,
                ItemStatus = new ItemStatus() { Id = dto.Id },
                Location = locationMapper.MapToEntity(dto.Location),
                Washes = dto.Washes,
                Article = new Article()
                {
                    Id = dto.Article.Id,
                    Name = dto.Article.Name,
                    Washes = dto.Article.Washes,
                    Color = new Color()
                    {
                        Id = dto.Article.Color.Id,
                        Name = dto.Article.Color.Name
                    },
                    Size = new Size()
                    {
                        Id = dto.Article.Size.Id,
                        Name = dto.Article.Size.Name
                    },
                    Type = new ItemType()
                    {
                        Id = dto.Article.Type.Id,
                        Name = dto.Article.Type.Name,
                        Category = new Category()
                        {
                            Id = dto.Article.Type.Category.Id,
                            Name = dto.Article.Type.Category.Name
                        }
                    }
                }
            };
        }

        public ItemDTO MapToDTO(Item entity)
        {
            return new ItemDTO()
            {
                Id = entity.Id,
                Code = entity.Code,
                Created = entity.Created,
                ItemStatus = (ItemStatusEnum)entity.ItemStatus.Id,
                Location = locationMapper.MapToDTO(entity.Location),
                Washes = entity.Washes,
                Article = new ArticleDTO()
                {
                    Id = entity.Article.Id,
                    Name = entity.Article.Name,
                    Washes = entity.Article.Washes,
                    Color = new ColorDTO()
                    {
                        Id = entity.Article.Color.Id,
                        Name = entity.Article.Color.Name
                    },
                    Size = new SizeDTO()
                    {
                        Id = entity.Article.Size.Id,
                        Name = entity.Article.Size.Name
                    },
                    Type = new ItemTypeDTO()
                    {
                        Id = entity.Article.Type.Id,
                        Name = entity.Article.Type.Name,
                        Category = new CategoryDTO()
                        {
                            Id = entity.Article.Type.Category.Id,
                            Name = entity.Article.Type.Category.Name
                        }
                    }
                }
            };
        }

        public ProcessDetailViewDTO MapToViewDTO(Item entity)
        {
            return new ProcessDetailViewDTO()
            {
                ArticleId = entity.Article.Id,
                Color = entity.Article.Color.Name,
                ItemType = entity.Article.Type.Name,
                Size = entity.Article.Size.Name,
                Quantity = 1
            };
        }
    }
}
