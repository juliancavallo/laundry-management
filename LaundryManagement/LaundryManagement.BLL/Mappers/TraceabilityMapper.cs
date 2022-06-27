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
    public class TraceabilityMapper
    {
        private LocationMapper locationMapper;
        private UserMapper userMapper; 
        private ItemMapper itemMapper;

        public TraceabilityMapper()
        {
            locationMapper = new LocationMapper();
            userMapper = new UserMapper();  
            itemMapper = new ItemMapper();  
        }

        public Traceability MapToEntity(TraceabilityDTO dto)
        {
            return new Traceability()
            {
                Id = dto.Id,
                Destination = locationMapper.MapToEntity(dto.Destination),
                Origin = locationMapper.MapToEntity(dto.Origin),
                ItemStatus = new ItemStatus() { Id = (int)dto.ItemStatus},
                Date = dto.Date,
                Item = itemMapper.MapToEntity(dto.Item),
                User = userMapper.MapToEntity(dto.User),
                MovementType = new MovementType() {  Id = (int)dto.MovementType}
            };
        }

        public TraceabilityDTO MapToDTO(Traceability entity)
        {
            return new TraceabilityDTO()
            {
                Id = entity.Id,
                Destination = locationMapper.MapToDTO(entity.Destination),
                Origin = locationMapper.MapToDTO(entity.Origin),
                Date = entity.Date,
                Item = itemMapper.MapToDTO(entity.Item),
                User = userMapper.MapToDTO(entity.User),
                ItemStatus = (ItemStatusEnum)entity.ItemStatus.Id,
                ItemStatusName = entity.ItemStatus.Name,
                MovementType = (MovementTypeEnum)entity.MovementType.Id,
                MovementTypeName = entity.MovementType.Name
            };
        }
    }
}
