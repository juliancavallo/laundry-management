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
            this.logMapper = new LogMapper();
        }

        public void LogInfo(MovementTypeEnum movementType, string message) =>
            this.Save(movementType, message, LogLevelEnum.Information);

        public void LogWarning(MovementTypeEnum movementType, string message) =>
            this.Save(movementType, message, LogLevelEnum.Warning);

        public void LogError(MovementTypeEnum movementType, string message) =>
            this.Save(movementType, message, LogLevelEnum.Error);

        private void Save(MovementTypeEnum movementType, string message, LogLevelEnum logLevel)
        {
            var globalLogLevel = (LogLevelEnum)Session.Settings.LogLevel;

            bool shouldLog = logLevel >= globalLogLevel;

            if (shouldLog)
            {
                var dto = new LogDTO()
                {
                    MovementType = movementType,
                    Message = message,
                    Date = DateTime.Now,
                    User = (UserDTO)Session.Instance.User,
                    LogLevel = logLevel
                };
                logDAL.Save(logMapper.MapToEntity(dto));
            }
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

            if (filter.LogLevel.HasValue && filter.LogLevel.Value != 0)
                list = list.Where(x => x.LogLevel.Id == (int)filter.LogLevel);

            if (!string.IsNullOrWhiteSpace(filter.Message))
                list = list.Where(x => x.Message.Contains(filter.Message));

            return list.Select(x => logMapper.MapToViewDTO(x)).ToList();
        }

        public List<EnumTypeDTO> GetAllLogLevels() =>
            this.logDAL
            .GetAllLogLevels()
            .Select(x => new EnumTypeDTO()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();

    }
}
