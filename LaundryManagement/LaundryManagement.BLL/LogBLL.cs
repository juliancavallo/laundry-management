using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class LogBLL
    {
        private LogMapper logMapper;
        private LogDAL logDAL;
        public LogBLL()
        {
            this.logDAL = new LogDAL();
            this.logMapper  = new LogMapper();
        }

        public void Save(MovementTypeEnum movementType, string message) 
        {
            var dto = new LogDTO()
            {
                MovementType = movementType,
                Message = message,
                Date = DateTime.Now,
                User = (UserDTO)Session.Instance.User
            };
            logDAL.Save(logMapper.MapToEntity(dto));
        }
        
        public List<LogViewDTO> GetForView(LogFilter filter)
        {
            var list = logDAL.Get().AsEnumerable();

            if (filter.DateFrom != DateTime.MinValue)
                list = list.Where(x => x.Date > filter.DateFrom);

            if (filter.DateTo != DateTime.MinValue)
                list = list.Where(x => x.Date < filter.DateTo);

            if (filter.MovementType.HasValue && filter.MovementType.Value != 0)
                list = list.Where(x => x.MovementType.Id == (int)filter.MovementType);

            if (!string.IsNullOrWhiteSpace(filter.Message))
                list = list.Where(x => x.Message.Contains(filter.Message));

            return list.Select(x => logMapper.MapToViewDTO(x)).ToList();
        }
        
    }
}
