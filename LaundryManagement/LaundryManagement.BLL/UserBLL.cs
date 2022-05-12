using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class UserBLL : ICrud<UserDTO>
    {
        public void Delete(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public IList<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(UserDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
