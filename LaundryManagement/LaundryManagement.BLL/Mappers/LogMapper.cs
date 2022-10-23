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
                User = dto.User != null ? userMapper.MapToEntity(dto.User) : null,
                MovementType = new MovementType() {  Id = (int)dto.MovementType},
                Message = dto.Message,
                LogLevel = new LogLevel() { Id = (int)dto.LogLevel}
            };
        }

        public LogDTO MapToDTO(Log entity)
        {
            return new LogDTO()
            {
                Id = entity.Id,
                Date = entity.Date,
                User = entity.User != null ? userMapper.MapToDTO(entity.User) : null,
                MovementType = (MovementTypeEnum)entity.MovementType.Id,
                MovementTypeName = entity.MovementType.Name,
                Message = entity.Message,
                LogLevel = (LogLevelEnum)entity.LogLevel.Id
            };
        }

        public LogViewDTO MapToViewDTO(Log entity)
        {
            return new LogViewDTO()
            {
                Date = entity.Date,
                Movement = entity.MovementType.Name,
                User = entity.User?.UserName ?? "No user",
                Message = entity.Message,
                Level = entity.LogLevel.Name
            };
        }
    }
}
