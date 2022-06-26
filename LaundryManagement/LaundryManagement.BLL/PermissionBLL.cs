using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
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
            var entities = permissionDAL.GetAll();
            return entities.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public void SetChilds(ref ComponentDTO componentDTO)
        {
            if(componentDTO is CompositeDTO)
            {

                var composite = mapper.MapToEntity(componentDTO) as Composite;
                var addedPermissions = new List<int>() { composite.Id };
                permissionDAL.AddCompositeChildren(composite, addedPermissions);

                componentDTO = mapper.MapToDTO(composite);
            }
        }

        public List<ComponentDTO> GetLeafs()
        {
            var entities = permissionDAL.GetSinglePermissions(false);
            return entities.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public List<ComponentDTO> GetFamilies()
        {
            var entities = permissionDAL.GetSinglePermissions(true);
            return entities.Select(x => mapper.MapToDTO(x)).ToList();
        }

        public void SaveUserPermissions(int userId, List<ComponentDTO> components)
        {
            var entities = components.Select(x => mapper.MapToEntity(x));
            permissionDAL.SaveUserPermissions(userId, entities);
            Session.Instance.User.Permissions = permissionDAL.GetPermissions(userId).Select(x => mapper.MapToDTO(x)).ToList<IComponentDTO>();
        }

        public void SavePermission(ComponentDTO componentDTO)
        {
            var entity = mapper.MapToEntity(componentDTO);
            permissionDAL.Save(entity);
        }

        public void Delete(ComponentDTO componentDTO)
        {
            var entity = mapper.MapToEntity(componentDTO);
            permissionDAL.Delete(entity);
        }
    }
}
