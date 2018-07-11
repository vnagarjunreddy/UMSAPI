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
    public class UserRoleService
    {
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        GetSessionDetails _sessionManager = new GetSessionDetails();

        public UserRoleService()
        {
        }

        /// <summary>
        /// To get all the user roles.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<UserRole> GetUserRoles(string accessToken)
        {
            IList<UserRole> lstUserRoles = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        lstUserRoles = repository.GetUserRoles();
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
            return lstUserRoles;
        }

        /// <summary>
        /// To get the user role details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public UserRole GetUserRoleByID(int id, string accessToken)
        {
            UserRole userRole = null;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        userRole = repository.GetUserRoleIDDetails(id);
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
            return userRole;            
        }

        /// <summary>
        /// To update the user role details by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="userrole"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateUserRole(int id, UserRole userrole, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        var userRoleDetails = repository.GetUserRoleIDDetails(id);
                        if (userRoleDetails != null)
                        {
                            int existUsrRoleCount = repository.FindUserRole(id, (userrole.UserID == null ? userRoleDetails.UserID : userrole.UserID), (userrole.RoleID == null ? userRoleDetails.RoleID : userrole.RoleID));
                            if (existUsrRoleCount == 0)
                            {
                                if (userrole.RoleID != 0)
                                    userRoleDetails.RoleID = userrole.RoleID;
                                if (userrole.UserID != 0)
                                    userRoleDetails.UserID = userrole.UserID;
                            }
                            else
                            {
                                throw new Exception("UserRole is already exist.");
                            }
                            userRoleDetails.UpdatedBy = userrole.UpdatedBy;
                            userRoleDetails.UpdatedDate = DateTime.Now;
                            repository.ModifiedUserRole(userRoleDetails);
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
        /// To add the user role details.
        /// </summary>
        /// <param name="userrole"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddUserRole(UserRole userrole, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        bool isCheckRoleAndUser = repository.CheckRoleAndUser(userrole.RoleID, userrole.UserID);
                        if (isCheckRoleAndUser)
                        {
                            bool isExistUserRoleCount = repository.ExistUserRole(userrole.UserID, userrole.RoleID);
                            if (isExistUserRoleCount)
                            {  
                                userrole.CreatedDate = DateTime.Now;
                                userrole.UpdatedDate = DateTime.Now;
                                repository.AddUserRole(userrole);
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
        /// To delete the user role details by id.
        /// </summary>
        /// <param name="userrole"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeleteUserRole(int id, string accessToken)
        {
            string msg = string.Empty;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        var userRole = repository.GetUserRoleIDDetails(id);
                        if (userRole != null)
                        {
                            repository.DeleteUserRole(userRole);
                            msg = "UserRole has been deleted successfully.";
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

        /// <summary>
        /// To get the user role id based on user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public int GetUserRoleID(int userId, string accessToken)
        {
            int userRoleId = 0;
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRoleRepository(session.DatabaseId()))
                    {
                        userRoleId = repository.GetUserRoleID(userId);
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
            return userRoleId;
        }
    }
}
