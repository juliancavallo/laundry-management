﻿using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class ItemMapper
    {
        public Item MapToEntity(ItemDTO dto)
        {
            return new Item()
            {
                Id = dto.Id,
                Code = dto.Code,
                Created = dto.Created,
                Article = new Article()
                {
                    Id = dto.Article.Id,
                    Name = dto.Article.Name,
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
                Article = new ArticleDTO()
                {
                    Id = entity.Article.Id,
                    Name = entity.Article.Name,
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
    }
}