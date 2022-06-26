using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
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

        public UserBLL()
        {
            this.permissionDAL = new PermissionDAL();
            this.dal = new UserDAL();
            this.mapper = new UserMapper();
        }

        public void Delete(UserDTO dto)
        {
            if (dto.Id == Session.Instance.User.Id)
                throw new ValidationException(Session.Translations[Tags.DeleteLoggedUser].Text, ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);
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

        public void Save(UserDTO dto)
        {
            var existingUser = this.dal.GetAll().Where(x => x.UserName == dto.UserName || x.Email == dto.Email);
            if (existingUser.Any(x => x.Id != dto.Id || dto.Id == 0))
                throw new ValidationException(Session.Translations[Tags.UserDuplicate].Text, ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Save(entity);
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

            foreach(var item in list)
            {
                item.Permissions = permissionDAL.GetPermissions(item.Id);
                dtoList.Add(mapper.MapToDTO(item));
            }

            return dtoList;
        }

        public string ResetPassword(string email, string newPassword)
        {
            var user = this.dal.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
                throw new ValidationException(Session.Translations[Tags.NonexistentUser].Text, ValidationType.Error);

            user.Password = Encryptor.Hash(newPassword);
            dal.Save(user);

            return newPassword;
        }

        public IList<UserViewDTO> GetAllForView()
        {
            return this.GetAll()
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }

        public bool HasPermission(UserDTO userDto, string permissionCode)
        {
            foreach (var item in userDto.Permissions)
            {
                if (permissionCode == "" || CheckPermissionRecursively((ComponentDTO)item, permissionCode))
                    return true;
            }
            return false;
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
