using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ValidationException("Cannot delete the user with which you are logged in", ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);
        }

        public IList<UserDTO> GetAll()
        {
            var list = this.dal.GetAll().ToList();
            list.ForEach(x => permissionDAL.SetPermissions(x));

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public UserDTO GetById(int id)
        {
            var entity = this.dal.GetById(id);
            return mapper.MapToDTO(entity);
        }

        public UserDTO GetByEmail(string email)
        {
            var user = this.dal.GetAll().FirstOrDefault(x => x.Email == email);

            if(user == null)
                return null;

            permissionDAL.SetPermissions(user);

            return mapper.MapToDTO(user);
        }

        public void Save(UserDTO dto)
        {
            if (dto.Id == Session.Instance.User.Id)
                throw new ValidationException("Cannot edit the user with which you are logged in", ValidationType.Warning);

            var entity = mapper.MapToEntity(dto);
            this.dal.Save(entity);
        }

        public string ResetPassword(UserDTO dto)
        {
            var newPassword = Encryptor.GenerateRandom();

            var entity = this.dal.GetById(dto.Id);
            entity.Password = Encryptor.Hash(newPassword);
            this.dal.Save(entity);

            return newPassword;
        }

        public IList<UserViewDTO> GetAllForView()
        {
            return this.GetAll()
                .Select(x => mapper.MapToViewDTO(x))
                .ToList();
        }
    }
}
