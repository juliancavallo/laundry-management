using LaundryManagement.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.DTOs;

namespace LaundryManagement.BLL
{
    public class UserPermissionBLL
    {
        public bool HasPermission(IUserDTO userDto, string permissionCode)
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
