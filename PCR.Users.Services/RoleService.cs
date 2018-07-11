using PCR.Users.Data;
using PCR.Users.Models;
using PCR.Users.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Services
{
    public class RoleService
    {
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        GetSessionDetails _sessionManager = new GetSessionDetails();
        public RoleService()
        {
        }

        /// <summary>
        /// To get all the roles.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<Role> GetRoles(string accessToken)
        {
            IList<Role> lstRoles = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RoleRepository(session.DatabaseId()))
                    {
                         lstRoles = repository.GetRoles();
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
            return lstRoles;
        }

        /// <summary>
        /// To get the role details by role id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Role GetRoleByID(int id, string accessToken)
        {
            Role roleDetails = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RoleRepository(session.DatabaseId()))
                    {
                         roleDetails = repository.GetRoleIDDetails(id);
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
            return roleDetails;
        }

        /// <summary>
        /// To update the role details.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="role"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateRole(int id, Role role, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RoleRepository(session.DatabaseId()))
                    {
                        var roleDetails = repository.GetRoleIDDetails(id);
                        if (roleDetails != null)
                        {
                            if (role.RoleName != null)
                            {
                                if (role.RoleName.Length > 50)
                                    throw new Exception("RoleName should not exceed more than 50 characters");

                                int rs = repository.FindRoleName(id, role.RoleName);
                                if (rs > 0)
                                    throw new Exception("RoleName is already exist.");
                                else
                                    roleDetails.RoleName = role.RoleName;
                            }
                            if (role.Description != null)
                            {
                                if (role.Description.Length > 500)
                                    throw new Exception("Description should not exceed more than 500 characters");
                                else
                                    roleDetails.Description = role.Description;
                            }

                            if (role.IsActive != null)
                                roleDetails.IsActive = roleDetails.IsActive;
                            roleDetails.UpdatedDate = DateTime.Now;
                            roleDetails.RoleID = id;
                            repository.ModifiedRole(roleDetails);
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
        /// To add the role details.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddRole(Role role, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RoleRepository(session.DatabaseId()))
                    {
                        int existroleCount = repository.ExistRoleName(role.RoleName);
                        if (existroleCount == 0)
                        {
                            role.IsActive = true;
                            role.CreatedDate = DateTime.Now;
                            role.UpdatedDate = DateTime.Now;
                            repository.AddRole(role);
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
        /// To delete the role details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeleteRole(int id, string accessToken)
        {
            string msg = string.Empty;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new RoleRepository(session.DatabaseId()))
                    {
                        var role = repository.GetRoleIDDetails(id);
                        if (role != null)
                        {
                            int assignPrmsnCount = repository.AssignRoleCount(id);
                            if (assignPrmsnCount == 0)
                            {
                                repository.DeleteRole(role);
                                msg = "Role has been deleted successfully.";
                            }
                            else
                                msg = "This Role is already assigned canot be deleted.";
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
