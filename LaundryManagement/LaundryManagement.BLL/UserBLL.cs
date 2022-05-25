using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
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

        public UserBLL()
        {
            this.dal = new UserDAL();
            this.mapper = new UserMapper();
        }

        public void Delete(UserDTO dto)
        {
            var entity = mapper.MapToEntity(dto);
            this.dal.Delete(entity);
        }

        public IList<UserDTO> GetAll()
        {
            var list= this.dal.GetAll();
            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public UserDTO GetById(int id)
        {
            var entity = this.dal.GetById(id);
            return mapper.MapToDTO(entity);
        }

        public void Save(UserDTO dto)
        {
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
