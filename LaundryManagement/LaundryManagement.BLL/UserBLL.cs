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
        private CheckDigitBLL checkDigitBLL;
        private UserHistoryBLL userHistoryBLL;
        private UserPermissionBLL userPermissionBLL;

        public UserBLL()
        {
            this.permissionDAL = new PermissionDAL();
            this.translatorBLL = new TranslatorBLL();
            this.logBLL = new LogBLL();
            this.checkDigitBLL = new CheckDigitBLL();
            this.userHistoryBLL = new UserHistoryBLL();
            this.userPermissionBLL = new UserPermissionBLL();

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
                list = list.Where(x => x.FirstName.Contains(filter.Name));

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
                .Where(x => userPermissionBLL.HasPermission(x, permissionCode))
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }

        public void Save(UserDTO dto)
        {
            var existingUser = this.dal.GetAll().Where(x => x.UserName == dto.UserName || x.Email == dto.Email);
            if (existingUser.Any(x => x.Id != dto.Id || dto.Id == 0))
                throw new ValidationException(Session.Translations[Tags.UserDuplicate], ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);

            entity.CheckDigit = checkDigitBLL.GenerateHorizontalCheckDigit(entity);

            this.dal.Save(entity);
            checkDigitBLL.SaveVerticalCheckDigit(entity.GetType());

            this.UpdateLoggedUser(dto);

            logBLL.LogInfo(MovementTypeEnum.User, $"The user {entity.FullName} has been created");
            userHistoryBLL.SaveHistory(entity);
        }

        public string ResetPassword(string email, string newPassword)
        {
            var user = this.dal.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
                throw new ValidationException(Session.Translations[Tags.NonexistentUser], ValidationType.Error);

            user.Password = Encryptor.HashToString(newPassword);
            user.CheckDigit = checkDigitBLL.GenerateHorizontalCheckDigit(user);

            dal.Save(user);
            checkDigitBLL.SaveVerticalCheckDigit(user.GetType());

            logBLL.LogInfo(MovementTypeEnum.ManualPasswordReset, $"The user {user.FullName} has reset his password manually");
            userHistoryBLL.SaveHistory(user);

            return newPassword;
        }

        public void Delete(UserDTO dto)
        {
            if (dto.Id == Session.Instance.User.Id)
                throw new ValidationException(Session.Translations[Tags.DeleteLoggedUser], ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);

            checkDigitBLL.SaveVerticalCheckDigit(entity.GetType());

            logBLL.LogInfo(MovementTypeEnum.User, $"The user {entity.FullName} has been deleted");
        }

        private void UpdateLoggedUser(UserDTO dto)
        {
            if (Session.IsLogged() && Session.Instance.User.Id == dto.Id)
            {
                Session.Instance.User.Location = dto.Location;
                Session.SetTranslations(translatorBLL.GetTranslations((Language)dto.Language));
                Session.ChangeLanguage(dto.Language);
            }
        }
    }
}
