
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Data
{
    public class UserRoleRepository : BaseRepository
    {
        public UserRoleRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {
        }
        public UserRoleRepository(string databseId) : base(databseId)
        {
        }

        public UserRoleRepository() : base()
        {
        }


        /// <summary>
        /// To get the all user role details.
        /// </summary>
        /// <returns></returns>
        public IList<UserRole> GetUserRoles()
        {
            try
            {
                var listUserRoles = _clientDbContext.UserRoles.ToList();
                return listUserRoles;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the user role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserRole GetUserRoleIDDetails(int id)
        {
            try
            {
                var userRoleDetails = _clientDbContext.UserRoles.Where(a => a.UserRoleID == id).FirstOrDefault();
                return userRoleDetails;
            }
            catch
            {
                throw;
            }
        }

        public UserRole GetUserRoleIDDetailsByUserId(int userId)
        {
            try
            {
                _masterDbContext.Configuration.ProxyCreationEnabled = false;
                var userRoleDetails = _masterDbContext.UserRoles.Where(a => a.UserID == userId).FirstOrDefault();
                if (userRoleDetails == null && _clientDbContext !=null)
                {
                    _clientDbContext.Configuration.ProxyCreationEnabled = false;
                    userRoleDetails = _clientDbContext.UserRoles.Where(a => a.UserID == userId).FirstOrDefault();
                }
                return userRoleDetails;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// To find the user role details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int FindUserRole(int? id, int? userId, int? roleId)
        {
            try
            {
                int userRoleExistCount = _clientDbContext.UserRoles.Where(a => a.UserRoleID != (id == null ? a.UserRoleID : id) && a.UserID == userId && a.RoleID == roleId).Count();
                return userRoleExistCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To exist the user role details.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool ExistUserRole(int? userId, int? roleId)
        {
            try
            {
                int userRoleExistCount = _clientDbContext.UserRoles.Where(a => a.UserID == userId && a.RoleID == roleId).Count();
                if(userRoleExistCount == 0)
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
        /// To update the user role details.
        /// </summary>
        /// <param name="userrole"></param>
        public void ModifiedUserRole(UserRole userrole)
        {
            try
            {
                var data = _masterDbContext.UserRoles.Where(a => a.UserRoleID == userrole.UserRoleID && a.UserID == userrole.UserID).FirstOrDefault();
                if (_clientDbContext != null && data == null)
                {
                    _clientDbContext.Entry(userrole).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();                    
                }
                else
                {
                    _masterDbContext.Entry(userrole).State = System.Data.Entity.EntityState.Modified;
                    _masterDbContext.SaveChanges();
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
        public void AddUserRole(UserRole userrole)
        {
            try
            {
                _clientDbContext.UserRoles.Add(userrole);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the user role details.
        /// </summary>
        /// <param name="userrole"></param>
        public void DeleteUserRole(UserRole userrole)
        {
            try
            {
                _clientDbContext.UserRoles.Remove(userrole);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking userId and roleId details.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckRoleAndUser(int? roleId, int? userId)
        {
            try
            {
                int roleCount = _clientDbContext.Roles.Where(a => a.RoleID == roleId).Count();
                int userCount = _clientDbContext.Users.Where(a => a.UserID == userId).Count();
                if (roleCount == 0 && userCount == 0)
                    throw new Exception("Please enter valid UserID and RoleID");
                else if (roleCount == 0)
                    throw new Exception("Please enter valid RoleID");
                else if (userCount == 0)
                    throw new Exception("Please enter valid UserID");
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To het the user roleId based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserRoleID(int userId)
        {
            try
            {
                int getUserRoleId = Convert.ToInt32(_clientDbContext.UserRoles.Where(a => a.UserID == userId).FirstOrDefault().RoleID);
                return getUserRoleId;
            }
            catch
            {
                throw;
            }
        }

    }
}
