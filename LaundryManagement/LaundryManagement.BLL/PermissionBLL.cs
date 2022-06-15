using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class PermissionBLL
    {
        private PermissionMapper mapper;
        private PermissionDAL permissionDAL;

        public PermissionBLL()
        {
            mapper = new PermissionMapper();
            permissionDAL = new PermissionDAL();
        }

        public List<ComponentDTO> GetAll()
        {
            var entities = permissionDAL.GetAllPermissions();
            return entities.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public void SavePermissions(UserDTO userDTO)
        {
            var entities = userDTO.Permissions.Select(x => mapper.MapToEntity(x as ComponentDTO));
            permissionDAL.SavePermissions(userDTO.Id, entities);
        }
    }
}
