using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Services;
using System.Collections.Generic;
using System.Linq;

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

        public void SavePermissions(int userId, List<ComponentDTO> components)
        {
            var entities = components.Select(x => mapper.MapToEntity(x));
            permissionDAL.SavePermissions(userId, entities);
            Session.Instance.User.Permissions = permissionDAL.GetPermissions(userId).Select(x => mapper.MapToDTO(x)).ToList<IComponentDTO>();
        }
    }
}
