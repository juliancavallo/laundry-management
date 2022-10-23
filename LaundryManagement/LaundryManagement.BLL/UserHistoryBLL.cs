using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class UserHistoryBLL
    {
        private UserMapper mapper;
        private UserDAL dal;
        private LogBLL logBLL;

        public UserHistoryBLL()
        {
            this.logBLL = new LogBLL();
            this.dal = new UserDAL();
            this.mapper = new UserMapper();
        }

        public List<UserHistoryViewDTO> GetHistoryForView(int idUser)
        {
            return dal.GetHistory(idUser, null)
                .Select(x => new UserHistoryViewDTO()
                {
                    Date = x.Date,
                    Email = x.Email,
                    Id = x.Id,
                    LastName = x.LastName,
                    Name = x.Name,
                    UserName = x.UserName,
                })
                .ToList();
        }

        public UserHistoryDTO GetHistoryById(int idHistory)
        {
            return dal.GetHistory(null, idHistory)
                .Select(x => mapper.MapToHistoryDTO(x))
                .First();
        }

        public void ApplyHistory(UserHistoryDTO historyDTO)
        {
            var history = mapper.MapToHistory(historyDTO);
            dal.ApplyHistory(history);

            logBLL.LogInfo(MovementTypeEnum.UserHistory, $"The user {historyDTO.UserName} has changed its state to IdUserHistory {historyDTO.Id}");
        }

        public void SaveHistory(User user)
        {
            var history = new UserHistory()
            {
                Date = System.DateTime.Now,
                Email = user.Email,
                IdUser = user.Id,
                Name = user.FirstName,
                UserName = user.UserName,
                LastName = user.LastName,
                Language = user.Language,
                Location = user.Location,
                Password = user.Password
            };

            dal.SaveHistory(history);
        }
    }
}
