using LaundryManagement.Domain;
using LaundryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.DAL
{
    public class PermissionDAL
    {
        private SqlConnection connection;
        private Configuration configuration;
        private string connectionString;

        public PermissionDAL()
        {
            configuration = new Configuration();

            connection = new SqlConnection();
            connectionString = configuration.GetValue<string>("connectionString");
            connection.ConnectionString = connectionString;
        }

        public IList<Component> GetAllPermissions()
        {
            SqlDataReader reader = null;
            List<Component> permissions = new List<Component>();
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    $@"
                    SELECT DISTINCT p.Id, p.Name as PermissionName, p.Permission as Permission, pf.IdPermissionParent, 
	                    case when pf2.IdPermission is not null then 1 else 0 end as IsFamily
                    FROM Permission p 
                    LEFT JOIN PermissionFamily pf on p.Id = pf.IdPermission
                    LEFT JOIN PermissionFamily pf2 on p.Id = pf2.IdPermissionParent"
                );
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var addedPermissions = new List<int>();
                    var isFamily = reader.GetInt32(reader.GetOrdinal("IsFamily")) == 1;
                    var permissionId = reader.GetInt32(reader.GetOrdinal("Id"));

                    if (!PermissionExists(permissions, permissionId))
                    {
                        Component component = null;
                        if (isFamily)
                        {
                            component = new Composite();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();

                            addedPermissions.Add(permissionId);

                            AddCompositeChildren((Composite)component, addedPermissions);

                            foreach (var item in component.Children)
                            {
                                permissions.RemoveAll(x => x.Id == item.Id);
                            }
                        }
                        else
                        {
                            component = new Leaf();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();
                        }

                        permissions.Add(component);
                    }
                }

                return permissions;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        public List<Component> GetPermissions(int userId)
        {
            var result = new List<Component>();
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    $@"
                    SELECT DISTINCT up.IdPermission, p.Name as PermissionName, p.Permission as Permission, pf.IdPermissionParent, 
	                    case when pf2.IdPermission is not null then 1 else 0 end as IsFamily
	                    FROM [User] u
	                    INNER JOIN UserPermission up on u.Id = up.IdUser
	                    INNER JOIN Permission p on up.IdPermission = p.Id
	                    LEFT JOIN PermissionFamily pf on p.Id = pf.IdPermission
	                    LEFT JOIN PermissionFamily pf2 on p.Id = pf2.IdPermissionParent
                    WHERE u.Id = {userId}"
                );
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                var addedPermissions = new List<int>();

                while (reader.Read())
                {
                    var isFamily = int.Parse(reader["IsFamily"].ToString()) == 1;
                    var permissionId = int.Parse(reader["IdPermission"].ToString());

                    if (!PermissionExists(result, permissionId))
                    {
                        Component component = null;
                        if (isFamily)
                        {
                            component = new Composite();
                            component.Name = reader["PermissionName"].ToString();
                            component.Id = permissionId;
                            component.Permission = reader["Permission"].ToString();

                            addedPermissions.Add(permissionId);

                            AddCompositeChildren((Composite)component, addedPermissions, userId);

                            foreach (var item in component.Children)
                            {
                                result.RemoveAll(x => x.Id == item.Id);
                            }
                        }
                        else
                        {
                            component = new Leaf();
                            component.Name = reader["PermissionName"].ToString();
                            component.Id = permissionId;
                            component.Permission = reader["Permission"].ToString();
                        }

                        result.Add(component);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        public void AddCompositeChildren(Composite composite, List<int> addedPermissions, int? userId = null)
        {
            SqlConnection newConnection = null;
            SqlDataReader reader = null;
            try 
            { 
                newConnection = new SqlConnection(connectionString);
                newConnection.Open();

                string permissionsString = string.Join(',', addedPermissions);
                string addedPermissionsQuery = addedPermissions.Count > 0 ? @$"and p.Id not in ({permissionsString})" : "";
                string userQuery = userId.HasValue ? $"and up.IdUser = {userId}" : "";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @$"
                    select p.Id, p.Name, p.Permission, case when max(pf2.IdPermission) is null then 0 else 1 end as IsFamily
                    from Permission p
                    left join UserPermission up on p.Id = up.IdPermission
                    inner join PermissionFamily pf on p.Id = pf.IdPermission
                    left join PermissionFamily pf2 on p.Id = pf2.IdPermissionParent
                    where pf.IdPermissionParent = {composite.Id} {userQuery} {addedPermissionsQuery}
                    group by p.Id, p.Name, p.Permission";
                cmd.Connection = newConnection;
                reader = cmd.ExecuteReader();

                var components = new List<Component>();
                while (reader.Read())
                {
                    Component component;
                    var isFamily = int.Parse(reader["IsFamily"].ToString()) == 1;

                    if (isFamily)
                        component = new Composite();
                    else
                        component = new Leaf();

                    component.Name = reader["Name"].ToString();
                    component.Id = int.Parse(reader["Id"].ToString());
                    component.Permission = reader["Permission"].ToString();

                    addedPermissions.Add(component.Id);

                    if (isFamily)
                        AddCompositeChildren((Composite)component, addedPermissions, userId);

                    components.Add(component);
                }
            
                components.ForEach(x => composite.AddChildren(x));
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                newConnection?.Close();
                reader?.Close();
            }
        }

        public IList<Component> GetSinglePermissions(bool isFamily)
        {
            SqlDataReader reader = null;
            List<Component> permissions = new List<Component>();
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    $@"
                    select distinct p.Id, p.Name as PermissionName, p.Permission as Permission
                    from Permission p
                    left join PermissionFamily pf on p.Id = pf.IdPermissionParent
                    where pf.IdPermission {(isFamily ? "is not null" : "is null")}"
                );
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Component component = null;

                    if (isFamily)
                        component = new Composite();
                    else
                        component = new Leaf();

                    component.Name = reader["PermissionName"].ToString();
                    component.Id = int.Parse(reader["Id"].ToString());
                    component.Permission = reader["Permission"].ToString();
                    permissions.Add(component);
                }

                return permissions;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        private bool PermissionExists(IList<Component> list, int id)
        {
            if(list.Any(x => x.Id == id)) 
                return true;

            foreach (var item in list)
            {
                if (item is Composite)
                {
                    bool exists = PermissionExists(item.Children, id);
                    if(exists) return true;
                }

                if (item is Leaf && item.Id == id)
                    return true;

            }

            return false;
        }

        public void SaveUserPermissions(int userId, IEnumerable<Component> permissions)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                       $@"
                            DELETE [UserPermission] WHERE IdUser = {userId}
                        ");
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"INSERT INTO UserPermission (IdUser, IdPermission) VALUES ";
                foreach(var item in permissions)
                {
                    cmd.CommandText += $"({userId}, {item.Id}),";
                }
                cmd.CommandText = cmd.CommandText.TrimEnd(',');
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void SavePermission(Component permission)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                if (permission.Id > 0)
                {
                    cmd.CommandText = @$"
                        UPDATE Permission SET Name = '{permission.Name}', Permission = '{permission.Permission}'
                        WHERE Id = {permission.Id}
                        SELECT SCOPE_IDENTITY();";

                    cmd.ExecuteScalar();

                }
                else
                {
                    cmd.CommandText = @$"
                        INSERT INTO Permission (Name, Permission) VALUES ('{permission.Name}', '{permission.Permission}')
                        SELECT SCOPE_IDENTITY();";


                    decimal id = (decimal)cmd.ExecuteScalar();
                    permission.Id = (int)id;
                }

                cmd.CommandText =
                       $@"
                            DELETE [PermissionFamily] WHERE IdPermissionParent = {permission.Id}
                        ";
                cmd.ExecuteNonQuery();

                if(permission is Composite)
                {
                    cmd.CommandText = @"INSERT INTO PermissionFamily (IdPermissionParent, IdPermission) VALUES ";
                    foreach (var item in permission.Children)
                    {
                        cmd.CommandText += $"({permission.Id}, {item.Id}),";
                    }
                    cmd.CommandText = cmd.CommandText.TrimEnd(',');
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
