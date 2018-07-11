using PCR.Users.Data;
using PCR.Users.Models;
using PCR.Users.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace PCR.Users.Services
{
    public class PermissionService
    {
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        GetSessionDetails _sessionManager = new GetSessionDetails();
        public PermissionService()
        {
        }

        /// <summary>
        /// To get the all permission details.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<Permission> GetPermissions(string accessToken)
        {
            IList<Permission> lstpermissions = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                { 
                    using (var repository = new PermissionRepository(session.DatabaseId()))
                    {
                        lstpermissions = repository.GetPermissions();                        
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch
            {
                throw;
            }
            return lstpermissions;
        }

        /// <summary>
        /// To get the permission details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Permission GetPermissionByID(int id, string accessToken)
        {
            Permission permission = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new PermissionRepository(session.DatabaseId()))
                    {
                        permission= repository.GetPermissionIDDetails(id);
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch
            {
                throw;
            }
            return permission;
        }

        /// <summary>
        /// To update the permission details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="permission"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdatePermission(int id, Permission permission,string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new PermissionRepository(session.DatabaseId()))
                    {
                        var permissionDetails = repository.GetPermissionIDDetails(id);
                        if (permissionDetails != null)
                        {
                            if (permission.PermissionName != null)
                            {
                                if (permission.PermissionName.Length > 50)
                                    throw new Exception("PermissionName should not exceed more than 50 characters");

                                int existPermissionName = repository.FindPermissionName(id, permission.PermissionName);
                                if (existPermissionName > 0)
                                    throw new Exception("PermissionName is already exist.");
                                else
                                    permissionDetails.PermissionName = permission.PermissionName;
                            }
                            if (permission.Description != null)
                            {
                                if (permission.Description.Length > 500)
                                    throw new Exception("Description should not exceed more than 500 characters");
                                else
                                    permissionDetails.Description = permission.Description;
                            }
                            permissionDetails.UpdatedDate = DateTime.Now;
                            permissionDetails.PermissionID = id;
                            repository.ModifiedPermission(permissionDetails);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
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
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddPermission(Permission permission, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new PermissionRepository(session.DatabaseId()))
                    {
                        int existPermissionNameCount = repository.ExistPermissionName(permission.PermissionName);
                        if (existPermissionNameCount == 0)
                        {
                            Permission permsn = new Permission();
                            permsn.PermissionName = permission.PermissionName;
                            permsn.Description = permission.Description;
                            permsn.CreatedBy = permission.CreatedBy;
                            permsn.CreatedDate = DateTime.Now;
                            permsn.UpdatedBy = permission.UpdatedBy;
                            permsn.UpdatedDate = DateTime.Now;
                            permsn.DatabaseId = permission.DatabaseId;
                            permsn.PcrId = permission.PcrId;
                            repository.AddPermission(permsn);
                            return true;
                        }
                        else
                            return false;
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeletePermission(int id, string accessToken)
        {
            string msg = string.Empty;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new PermissionRepository(session.DatabaseId()))
                    {
                        var permission= repository.GetPermissionIDDetails(id);
                        if (permission != null)
                        {
                            int assignPrmsnCount = repository.AssignPermissionCount(id);
                            if (assignPrmsnCount == 0)
                            {
                                repository.DeletePermission(permission);
                                msg= "Permission has been deleted successfully.";
                            }
                            else
                                msg= "This Permission is already assigned canot be deleted.";
                        }
                        else
                            throw new Exception("Content not found by Id =" + id);
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch
            {
                throw;
            }
            return msg;
        }
    }
}
