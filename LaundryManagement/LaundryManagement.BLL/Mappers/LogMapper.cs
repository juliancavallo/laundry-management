﻿using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class LogMapper
    {
        private UserMapper userMapper; 

        public LogMapper()
        {
            userMapper = new UserMapper();  
        }

        public Log MapToEntity(LogDTO dto)
        {
            return new Log()
            {
                Id = dto.Id,
                Date = dto.Date,
                User = userMapper.MapToEntity(dto.User),
                MovementType = new MovementType() {  Id = (int)dto.MovementType},
                Message = dto.Message,
            };
        }

        public LogDTO MapToDTO(Log entity)
        {
            return new LogDTO()
            {
                Id = entity.Id,
                Date = entity.Date,
                User = userMapper.MapToDTO(entity.User),
                MovementType = (MovementTypeEnum)entity.MovementType.Id,
                MovementTypeName = entity.MovementType.Name,
                Message = entity.Message,
            };
        }

        public LogViewDTO MapToViewDTO(Log entity)
        {
            return new LogViewDTO()
            {
                Date = entity.Date,
                Movement = entity.MovementType.Name,
                User = entity.User.UserName,
                Message = entity.Message
            };
        }
    }
}