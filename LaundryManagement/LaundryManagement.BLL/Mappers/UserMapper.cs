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
            };

        }

        public UserDTO MapToDTO(User entity)
        {
            return new UserDTO()
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Password = entity.Password,
                UserName = entity.UserName,
                LastName = entity.LastName, 
            };

        }
    }
}
