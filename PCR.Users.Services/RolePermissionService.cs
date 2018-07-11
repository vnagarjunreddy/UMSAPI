using PCR.Users.Data;
using PCR.Users.Models;
using PCR.Users.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Services
{
    public class RolePermissionService
    {
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        GetSessionDetails _sessionManager = new GetSessionDetails();
        public RolePermissionService()
        {
        }

        /// <summary>
        /// To get all the role permission details.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<RolePermission> GetRolePermissions(string accessToken)
        {
            IList<RolePermission> lstRolePermissions = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RolePermissionRepository(session.DatabaseId()))
                    {
                       lstRolePermissions = repository.GetRolePermissions();                       
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
            return lstRolePermissions;
        }

        /// <summary>
        /// To get the role permission details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public RolePermission GetRolePermissionByID(int id, string accessToken)
        {
            RolePermission rolePermission = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RolePermissionRepository(session.DatabaseId()))
                    {
                         rolePermission = repository.GetRolePermissionIDDetails(id);                        
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
            return rolePermission;
        }

        /// <summary>
        /// To update the role permission details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="rolepermission"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateRolePermission(int id, RolePermission rolepermission, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RolePermissionRepository(session.DatabaseId()))
                    {
                        bool isExistRoleAndPermission = repository.CheckRoleAndPermission(rolepermission.RoleID, rolepermission.PermissionID);
                        if (isExistRoleAndPermission)
                        {
                            var rolePermissionDetails = repository.GetRolePermissionIDDetails(id);
                            if (rolePermissionDetails != null)
                            {
                                int rs = repository.FindRolePermission(id, (rolepermission.RoleID == 0 ? rolePermissionDetails.RoleID : rolepermission.RoleID), (rolepermission.PermissionID == 0 ? rolePermissionDetails.PermissionID : rolepermission.PermissionID));
                                if (rs > 0)
                                    throw new Exception("RolePermission is already exist.");
                                else
                                {
                                    rolePermissionDetails.RoleID = rolepermission.RoleID;
                                    rolePermissionDetails.PermissionID = rolepermission.PermissionID;
                                }

                                rolePermissionDetails.UpdatedBy = rolepermission.UpdatedBy;
                                rolePermissionDetails.UpdatedDate = DateTime.Now;
                                rolePermissionDetails.RolePermissionID = id;
                                rolePermissionDetails.CreatedBy = rolePermissionDetails.CreatedBy;
                                rolePermissionDetails.CreatedDate = rolePermissionDetails.CreatedDate;
                                repository.ModifiedRolePermission(rolePermissionDetails);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
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
            return false;
        }

        /// <summary>
        /// To add the role permission details.
        /// </summary>
        /// <param name="rolepermission"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddRolePermission(RolePermission rolepermission, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RolePermissionRepository(session.DatabaseId()))
                    {
                        bool IsExistRoleAndPermission = repository.CheckRoleAndPermission(rolepermission.RoleID, rolepermission.PermissionID);
                        if (IsExistRoleAndPermission)
                        {
                            bool IsexistRolePermissionCount = repository.ExistRolePermission(rolepermission.RoleID, rolepermission.PermissionID);
                            if (IsexistRolePermissionCount)
                            {
                                rolepermission.CreatedDate = DateTime.Now;
                                rolepermission.UpdatedDate = DateTime.Now;
                                repository.AddRolePermission(rolepermission);
                                return true;
                            }
                            else
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
            return false;
        }

        /// <summary>
        /// To delete the role permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeleteRolePermission(int id, string accessToken)
        {
            string msg = string.Empty;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RolePermissionRepository(session.DatabaseId()))
                    {
                        var rolepermission = repository.GetRolePermissionIDDetails(id);
                        if (rolepermission != null)
                        {
                            repository.DeleteRolePermission(rolepermission);
                            msg = "RolePermission has been deleted successfully.";
                        }
                        else
                            msg = "Content not found by Id =" + id;
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
