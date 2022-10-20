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
    public class UserBLL : ICrud<UserDTO>
    {
        private UserMapper mapper;
        private UserDAL dal;
        private PermissionDAL permissionDAL;
        private TranslatorBLL translatorBLL;
        private LogBLL logBLL;

        public UserBLL()
        {
            this.permissionDAL = new PermissionDAL();
            this.translatorBLL = new TranslatorBLL();
            this.logBLL = new LogBLL();
            this.dal = new UserDAL();
            this.mapper = new UserMapper();
        }

        public IList<UserDTO> GetAll()
        {
            var list = this.dal.GetAll().ToList();
            list.ForEach(x => x.Permissions = permissionDAL.GetPermissions(x.Id));

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public UserDTO GetById(int id)
        {
            var entity = this.dal.GetById(id);
            entity.Permissions = permissionDAL.GetPermissions(id);
            return mapper.MapToDTO(entity);
        }

        public IList<UserDTO> GetByFilter(UserFilter filter)
        {
            var list = this.dal.GetAll().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filter.UserName))
                list = list.Where(x => x.UserName.Contains(filter.UserName));

            if (!string.IsNullOrWhiteSpace(filter.Name))
                list = list.Where(x => x.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                list = list.Where(x => x.LastName.Contains(filter.LastName));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                list = list.Where(x => x.Email.Contains(filter.Email));

            var dtoList = new List<UserDTO>();

            foreach (var item in list)
            {
                item.Permissions = permissionDAL.GetPermissions(item.Id);
                dtoList.Add(mapper.MapToDTO(item));
            }

            return dtoList;
        }

        public IList<UserViewDTO> GetAllForView()
        {
            return this.GetAll()
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }

        public IList<UserViewDTO> GetShippingResponsibles(string permissionCode)
        {
            return this.GetAll()
                .Where(x => HasPermission(x, permissionCode))
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
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

        public void Save(UserDTO dto)
        {
            var existingUser = this.dal.GetAll().Where(x => x.UserName == dto.UserName || x.Email == dto.Email);
            if (existingUser.Any(x => x.Id != dto.Id || dto.Id == 0))
                throw new ValidationException(Session.Translations[Tags.UserDuplicate], ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Save(entity);

            if (Session.IsLogged() && Session.Instance.User.Id == entity.Id)
            {
                Session.Instance.User.Location = dto.Location;
                Session.SetTranslations(translatorBLL.GetTranslations(entity.Language));
                Session.ChangeLanguage(entity.Language);
            }

            logBLL.LogInfo(MovementTypeEnum.User, $"The user {entity.FullName} has been created");
            SaveHistory(entity);
        }

        public string ResetPassword(string email, string newPassword)
        {
            var user = this.dal.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
                throw new ValidationException(Session.Translations[Tags.NonexistentUser], ValidationType.Error);

            user.Password = Encryptor.Hash(newPassword);
            dal.Save(user);

            logBLL.LogInfo(MovementTypeEnum.ManualPasswordReset, $"The user {user.FullName} has reset his password manually");
            SaveHistory(user);

            return newPassword;
        }

        public void Delete(UserDTO dto)
        {
            if (dto.Id == Session.Instance.User.Id)
                throw new ValidationException(Session.Translations[Tags.DeleteLoggedUser], ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);

            logBLL.LogInfo(MovementTypeEnum.User, $"The user {entity.FullName} has been deleted");
        }

        public void ApplyHistory(UserHistoryDTO historyDTO)
        {
            var history = mapper.MapToHistory(historyDTO);
            dal.ApplyHistory(history);

            logBLL.LogInfo(MovementTypeEnum.UserHistory, $"The user {historyDTO.UserName} has changed its state to IdUserHistory {historyDTO.Id}");
        }

        public bool HasPermission(IUserDTO userDto, string permissionCode)
        {
            foreach (var item in userDto.Permissions)
            {
                if (permissionCode == "" || CheckPermissionRecursively((ComponentDTO)item, permissionCode))
                    return true;
            }
            return false;
        }

        private void SaveHistory(User user)
        {
            var history = new UserHistory()
            {
                Date = System.DateTime.Now,
                Email = user.Email,
                IdUser = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                LastName = user.LastName,
                Language = user.Language,
                Location = user.Location,
                Password = user.Password
            };

            dal.SaveHistory(history);
        }

        private bool CheckPermissionRecursively(ComponentDTO permission, string permissionCode)
        {
            bool exists = false;
            if (permission.Permission == permissionCode)
                exists = true;
            else
            {
                if (permission is CompositeDTO)
                {
                    foreach (var child in permission.Children)
                    {
                        exists = CheckPermissionRecursively((ComponentDTO)child, permissionCode);
                        if (exists) return true;
                    }
                }
            }

            return exists;
        }
    }
}
