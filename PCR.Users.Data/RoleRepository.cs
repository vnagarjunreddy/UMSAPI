
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Data
{
    public class RoleRepository : BaseRepository
    {
        public RoleRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {
        }
        public RoleRepository(string databseId) : base(databseId)
        {
        }

        /// <summary>
        /// To get the all role details.
        /// </summary>
        /// <returns></returns>
        public IList<Role> GetRoles()
        {
            try
            {
                var lstRoles = _clientDbContext.Roles.ToList();
                return lstRoles;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleIDDetails(int id)
        {
            try
            {
                var roleDetails = _clientDbContext.Roles.Where(a => a.RoleID == id).FirstOrDefault();
                return roleDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To find the role name is exist or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int FindRoleName(int? id, string roleName)
        {
            try
            {
                int roleExistCount = _clientDbContext.Roles.Where(a => a.RoleID != (id == null ? a.RoleID : id) && a.RoleName == roleName).Count();
                return roleExistCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To exist the role name is exist or not.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public int ExistRoleName(string roleName)
        {
            try
            {
                int roleExistCount = _clientDbContext.Roles.Where(a => a.RoleName == roleName).Count();
                return roleExistCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To update the role details by id.
        /// </summary>
        /// <param name="role"></param>
        public void ModifiedRole(Role role)
        {
            try
            {
                _clientDbContext.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _clientDbContext.SaveChanges();                  
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the role details.
        /// </summary>
        /// <param name="role"></param>
        public void AddRole(Role role)
        {
            try
            {
                _clientDbContext.Roles.Add(role);
                _clientDbContext.SaveChanges();                    
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the role details by id.
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {
            try
            {
                _clientDbContext.Roles.Remove(role);
                _clientDbContext.SaveChanges();                 
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the role assign count.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int AssignRoleCount(int roleId)
        {
            try
            {
                int userRoleCount = _clientDbContext.UserRoles.Where(a => a.RoleID == roleId).Count();
                int rolePermissionCount = _clientDbContext.RolePermissions.Where(a => a.RoleID == roleId).Count();
                if (userRoleCount == 0 & rolePermissionCount == 0)
                    return 0;
                else
                    return 1;
            }
            catch
            {
                throw;
            }
        }

    }
}
