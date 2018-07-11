using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Data
{
    public class PermissionRepository : BaseRepository
    {
        public PermissionRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {            
        }
        public PermissionRepository(string databseId) : base(databseId)
        {
        }

        /// <summary>
        /// To get all permissions.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public IList<Permission> GetPermissions()
        {
            try
            {
                return _clientDbContext.Permissions.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the permission details by id.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Permission GetPermissionIDDetails(int id)
        {
            try
            {
                return _clientDbContext.Permissions.Where(a => a.PermissionID == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// To find the permission name existing count.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PermissionName"></param>
        /// <returns></returns>
        public int FindPermissionName(int? id, string permissionName)
        {
            try
            {
                return _clientDbContext.Permissions.Where(a => a.PermissionID != (id == null ? a.PermissionID : id) && a.PermissionName == permissionName).Count();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To exist permission name count.
        /// </summary>
        /// <param name="PermissionName"></param>
        /// <returns></returns>
        public int ExistPermissionName(string permissionName)
        {
            try
            {
                return _clientDbContext.Permissions.Where(a => a.PermissionName == permissionName).Count();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To update the permission details.
        /// </summary>
        /// <param name="permission"></param>
        public void ModifiedPermission(Permission permission)
        {
            try
            {
                _clientDbContext.Entry(permission).State = System.Data.Entity.EntityState.Modified;
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the permission details.
        /// </summary>
        /// <param name="permission"></param>
        public void AddPermission(Permission permission)
        {
            try
            {
                _clientDbContext.Permissions.Add(permission);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the permission details.
        /// </summary>
        /// <param name="permission"></param>
        public void DeletePermission(Permission permission)
        {
            try
            {
                _clientDbContext.Permissions.Remove(permission);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To assigned permission count.
        /// </summary>
        /// <param name="PermissionID"></param>
        /// <returns></returns>
        public int AssignPermissionCount(int permissionId)
        {
            try
            {
                return _clientDbContext.RolePermissions.Where(a => a.PermissionID == permissionId).Count();
            }
            catch
            {
                throw;
            }
        }

    }
}
