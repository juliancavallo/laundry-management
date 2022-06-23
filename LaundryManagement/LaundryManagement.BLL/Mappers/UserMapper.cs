using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class UserMapper
    {
        private PermissionMapper permissionMapper;

        public UserMapper()
        {
            permissionMapper = new PermissionMapper();
        }
        public User MapToEntity(UserDTO dto)
        {
            return new User()
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Language = dto.Language as Language
            };

        }

        public UserDTO MapToDTO(User entity)
        {
            var result = new UserDTO()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Password = entity.Password,
                UserName = entity.UserName,
                LastName = entity.LastName,
                Language = entity.Language,
            };

            foreach(var item in entity.Permissions)
            {
                result.Permissions.Add(permissionMapper.MapToDTO(item));
            }
            return result;
        }

        public UserViewDTO MapToViewDTO(UserDTO dto)
        {
            return new UserViewDTO()
            {
                Email = dto.Email,
                FullName = dto.FullName,
                UserName = dto.UserName,
                Id = dto.Id,
            };
        }
    }
}
