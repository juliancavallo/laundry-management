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
                            AddCompositeChildren((Composite)component);

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

        public void GetPermissions(User user)
        {
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
                    WHERE u.Id = {user.Id}"
                );
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var isFamily = reader.GetInt32(reader.GetOrdinal("IsFamily")) == 1;
                    var permissionId = reader.GetInt32(reader.GetOrdinal("IdPermission"));

                    if (!PermissionExists(user.Permissions, permissionId))
                    {
                        Component component = null;
                        if (isFamily)
                        {
                            component = new Composite();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();
                            AddCompositeChildren((Composite)component, user.Id);

                            foreach (var item in component.Children)
                            {
                                user.Permissions.RemoveAll(x => x.Id == item.Id);
                            }
                        }
                        else
                        {
                            component = new Leaf();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();
                        }
                        
                        user.Permissions.Add(component);
                    }

                }
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

                while (reader.Read())
                {
                    var isFamily = reader.GetInt32(reader.GetOrdinal("IsFamily")) == 1;
                    var permissionId = reader.GetInt32(reader.GetOrdinal("IdPermission"));

                    if (!PermissionExists(result, permissionId))
                    {
                        Component component = null;
                        if (isFamily)
                        {
                            component = new Composite();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();
                            AddCompositeChildren((Composite)component, userId);

                            foreach (var item in component.Children)
                            {
                                result.RemoveAll(x => x.Id == item.Id);
                            }
                        }
                        else
                        {
                            component = new Leaf();
                            component.Name = reader.GetString(reader.GetOrdinal("PermissionName"));
                            component.Id = permissionId;
                            component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();
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

        private void AddCompositeChildren(Composite composite, int? userId = null)
        {
            SqlConnection newConnection = null;
            SqlDataReader reader = null;
            try 
            { 
                newConnection = new SqlConnection(connectionString);
                newConnection.Open();

                SqlCommand cmd =
                    userId.HasValue ?
                    new SqlCommand($@"
                        WITH [recursive] AS (
                                        SELECT pf2.IdPermissionParent, pf2.IdPermission
	                                    FROM PermissionFamily pf2
                                        WHERE pf2.IdPermissionParent = {composite.Id}
                                        UNION ALL 
                                        SELECT pf.IdPermissionParent, pf.IdPermission
	                                    FROM PermissionFamily pf 
                                        INNER JOIN [recursive] r on r.IdPermission= pf.IdPermissionParent)
                        SELECT r.IdPermissionParent,  p.Id, p.Name, p.Permission,case when max(up2.IdPermission) is not null then 1 else 0 end as IsFamily
                        FROM [recursive] r 
                        INNER JOIN Permission p on r.IdPermission = p.Id
                        INNER JOIN UserPermission up on p.Id = up.IdPermission
					    LEFT JOIN PermissionFamily pf on pf.IdPermissionParent = up.IdPermission
					    LEFT JOIN UserPermission up2 on pf.IdPermission = up2.IdPermission
					    WHERE up.IdUser = {userId}
					    group by r.IdPermissionParent, r.IdPermission, p.Id, p.Name, p.Permission
                    ") :
                    new SqlCommand($@"
                        WITH [recursive] AS (
                                        SELECT pf2.IdPermissionParent, pf2.IdPermission
	                                    FROM PermissionFamily pf2
                                        WHERE pf2.IdPermissionParent = {composite.Id}
                                        UNION ALL 
                                        SELECT pf.IdPermissionParent, pf.IdPermission
	                                    FROM PermissionFamily pf 
                                        INNER JOIN [recursive] r on r.IdPermission= pf.IdPermissionParent)
                        SELECT r.IdPermissionParent,  p.Id, p.Name, p.Permission,case when max(p2.Id) is not null then 1 else 0 end as IsFamily
                        FROM [recursive] r 
                        INNER JOIN Permission p on r.IdPermission = p.Id
                        LEFT JOIN PermissionFamily pf on pf.IdPermissionParent = p.Id
                        LEFT JOIN Permission p2 on pf.IdPermission = p2.Id
                        group by r.IdPermissionParent, r.IdPermission, p.Id, p.Name, p.Permission
                    ");

                cmd.Connection = newConnection;
                reader = cmd.ExecuteReader();

                var components = new List<Component>();
                while (reader.Read())
                {
                    Component component;
                    var idParent = reader.GetInt32(reader.GetOrdinal("IdPermissionParent"));

                    var isFamily = reader.GetInt32(reader.GetOrdinal("IsFamily")) == 1;

                    if (isFamily)
                        component = new Composite();
                    else
                        component = new Leaf();

                    component.Name = reader.GetString(reader.GetOrdinal("Name"));
                    component.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    component.Permission = reader.GetValue(reader.GetOrdinal("Permission"))?.ToString();

                    var parent = GetComponent(idParent, components);
                    if (parent == null)
                        components.Add(component);
                    else
                        parent.AddChildren(component);
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

        private Component GetComponent(int id, IList<Component> list)
        {
            Component component = list?.FirstOrDefault(x => x.Id == id);

            if(component == null)
            {
                foreach(var c in list)
                {
                    if(c is Composite)
                    {
                        var l = GetComponent(id, c.Children);

                        if(l != null)
                            return l.Id == id ? l : GetComponent(id, l.Children);
                    }
                    else
                        return null;
                }
            }

            return component;
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

        public void SavePermissions(int userId, IEnumerable<Component> permissions)
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
    }
}
