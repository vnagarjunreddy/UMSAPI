
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Data
{
    public class RolePermissionRepository : BaseRepository
    {
        public RolePermissionRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {
        }
        public RolePermissionRepository(string databseId) : base(databseId)
        {
        }

        /// <summary>
        /// To get the all role permission details.
        /// </summary>
        /// <returns></returns>
        public IList<RolePermission> GetRolePermissions()
        {
            try
            {
                var lstRolePermissions = _clientDbContext.RolePermissions.ToList();
                return lstRolePermissions;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the role permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RolePermission GetRolePermissionIDDetails(int id)
        {
            try
            {
                var rolePermissionDetails = _clientDbContext.RolePermissions.Where(a => a.RolePermissionID == id).FirstOrDefault();
                return rolePermissionDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To find the role permission details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public int FindRolePermission(int? id, int? roleId, int? permissionId)
        {
            try
            {
                int rolePermissionCount = _clientDbContext.RolePermissions.Where(a => a.RolePermissionID != (id == null ? a.RolePermissionID : id) && a.RoleID == roleId && a.PermissionID == permissionId).Count();
                return rolePermissionCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking the role permission is exist or not.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public bool ExistRolePermission(int? roleId, int? permissionId)
        {
            try
            {
                int rolePermissionCount = _clientDbContext.RolePermissions.Where(a => a.RoleID == roleId && a.PermissionID == permissionId).Count();
                if (rolePermissionCount == 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To update the role permission details.
        /// </summary>
        /// <param name="rolepermission"></param>
        public void ModifiedRolePermission(RolePermission rolepermission)
        {
            try
            {
                _clientDbContext.Entry(rolepermission).State = System.Data.Entity.EntityState.Modified;
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the role permission details.
        /// </summary>
        /// <param name="rolepermission"></param>
        public void AddRolePermission(RolePermission rolepermission)
        {
            try
            {
                _clientDbContext.RolePermissions.Add(rolepermission);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the role permission details.
        /// </summary>
        /// <param name="rolePermission"></param>
        public void DeleteRolePermission(RolePermission rolePermission)
        {
            try
            {
                _clientDbContext.RolePermissions.Remove(rolePermission);
                _clientDbContext.SaveChanges();                   
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking Role and permission details.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public bool CheckRoleAndPermission(int? roleId, int? permissionId)
        {
            try
            {
                int roleCount = _clientDbContext.Roles.Where(a => a.RoleID == roleId).Count();
                int permissionCount = _clientDbContext.Permissions.Where(a => a.PermissionID == permissionId).Count();
                if (roleCount == 0 && permissionCount == 0)
                    throw new Exception("Invalid roleId and permissionId.");
                else if (roleCount == 0)
                    throw new Exception("Invalid roleId.");
                else if (permissionCount == 0)
                    throw new Exception("Invalid permissionId.");
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }


    }
}
