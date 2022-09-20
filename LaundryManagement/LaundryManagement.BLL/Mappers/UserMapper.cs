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
        private LocationMapper locationMapper;

        public UserMapper()
        {
            permissionMapper = new PermissionMapper();
            locationMapper = new LocationMapper();
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
                Language = dto.Language as Language,
                Location = locationMapper.MapToEntity(dto.Location as LocationDTO)
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
                Location = locationMapper.MapToDTO(entity.Location)
            };

            foreach(var item in entity.Permissions)
            {
                result.Permissions.Add(permissionMapper.MapToDTO(item));
            }
            return result;
        }

        public UserHistoryDTO MapToHistoryDTO(UserHistory entity)
        {
            var result = new UserHistoryDTO()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Password = entity.Password,
                UserName = entity.UserName,
                LastName = entity.LastName,
                Language = entity.Language,
                Location = locationMapper.MapToDTO(entity.Location),
                IdUser = entity.IdUser,
                Date = entity.Date
            };

            return result;

        }

        public UserHistory MapToHistory(UserHistoryDTO dto)
        {
            var result = new UserHistory()
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password,
                UserName = dto.UserName,
                LastName = dto.LastName,
                Language = dto.Language as Language,
                Location = locationMapper.MapToEntity(dto.Location as LocationDTO),
                IdUser = dto.IdUser,
                Date = dto.Date
            };

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
