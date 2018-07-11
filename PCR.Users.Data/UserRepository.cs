
using PCR.Users.Model.ViewModels;
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace PCR.Users.Data
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {
        }
        public UserRepository(string databseId) : base(databseId)
        {
        }
        public UserRepository() : base()
        {
        }

        /// <summary>
        /// To get the all user details based on roleId.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<UserWorkAddress> GetUsers(int roleId, string databaseId)
        {
            try
            {
                var lstUsers = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false)
                                join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                from p in _clientDbContext.Roles.Where(a => a.RoleID == n.RoleID && a.RoleID > roleId)
                                join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                select new UserWorkAddress
                                {
                                    UserID = m.UserID,
                                    UserName = m.UserName,
                                    Password = m.Password,
                                    EmailConfirmed = m.EmailConfirmed,
                                    AccessFailedCount = m.AccessFailedCount,
                                    LockOutEnabled = m.LockOutEnabled,
                                    PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                    EmailAddress = m.EmailAddress,
                                    FirstName = m.FirstName,
                                    MiddleName = m.MiddleName,
                                    LastName = m.LastName,
                                    RoleID = p.RoleID,
                                    WorkAddressID = w.WorkAddressID,
                                    LocationID = w.LocationID,
                                    WorkPhone = w.WorkPhone,
                                    IsDelete = m.IsDelete,
                                    CreatedBy = m.CreatedBy,
                                    CreatedDate = m.CreatedDate,
                                    UpdatedBy = m.UpdatedBy,
                                    UpdatedDate = m.UpdatedDate,
                                    Gender = m.Gender,
                                    Mobile = m.Mobile,
                                    OtherName = m.OtherName,
                                    DOB = m.DOB,
                                    ReportingAuthorityName = m.ReportingAuthorityName,
                                    ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                    ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                    Address1 = w.Address1,
                                    Address2 = w.Address2,
                                    StateId = w.StateId,
                                    CityId = w.CityId,
                                    Zip = w.Zip,
                                    DatabaseId = databaseId,
                                    IsOnboardCompleted = m.IsOnboardCompleted,
                                    IsManagerApproved = m.IsManagerApproved
                                }).ToList().OrderByDescending(a => a.UserID);

                return lstUsers.ToList();

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the all user details based on roleId.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<UserWorkAddress> GetManageUsers(int roleId, string databaseId)
        {
            try
            {
                var lstUsers = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false)
                                join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                from p in _clientDbContext.Roles.Where(a => a.RoleID == n.RoleID && roleId < a.RoleID && a.RoleID < 4)
                                join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                select new UserWorkAddress
                                {
                                    UserID = m.UserID,
                                    UserName = m.UserName,
                                    Password = m.Password,
                                    EmailConfirmed = m.EmailConfirmed,
                                    AccessFailedCount = m.AccessFailedCount,
                                    LockOutEnabled = m.LockOutEnabled,
                                    PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                    EmailAddress = m.EmailAddress,
                                    FirstName = m.FirstName,
                                    MiddleName = m.MiddleName,
                                    LastName = m.LastName,
                                    RoleID = p.RoleID,
                                    WorkAddressID = w.WorkAddressID,
                                    LocationID = w.LocationID,
                                    WorkPhone = w.WorkPhone,
                                    IsDelete = m.IsDelete,
                                    CreatedBy = m.CreatedBy,
                                    CreatedDate = m.CreatedDate,
                                    UpdatedBy = m.UpdatedBy,
                                    UpdatedDate = m.UpdatedDate,
                                    Gender = m.Gender,
                                    Mobile = m.Mobile,
                                    OtherName = m.OtherName,
                                    DOB = m.DOB,
                                    ReportingAuthorityName = m.ReportingAuthorityName,
                                    ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                    ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                    Address1 = w.Address1,
                                    Address2 = w.Address2,
                                    StateId = w.StateId,
                                    CityId = w.CityId,
                                    Zip = w.Zip,
                                    DatabaseId = databaseId,
                                    IsOnboardCompleted = m.IsOnboardCompleted,
                                    IsManagerApproved = m.IsManagerApproved
                                }).ToList().OrderByDescending(a => a.UserID);

                return lstUsers.ToList();

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the user details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserIDDetails(int id)
        {
            try
            {
                //  var dbContext = _clientDbContext == null ? _masterDbContext : _clientDbContext;
                var userDetails = _masterDbContext.Users.Find(id);
                if (userDetails == null && _clientDbContext != null)
                    userDetails = _clientDbContext.Users.Where(a => a.UserID == id).Include(a => a.LinkedPcrUsers).FirstOrDefault();

                return userDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking the user name existing count.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int ExistUserName(string userName)
        {
            try
            {
                int existUserNamesCount = _clientDbContext.Users.Where(a => a.UserName == userName).Count();
                return existUserNamesCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking the user name based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistUserNameWithUserID(int userId, string userName)
        {
            try
            {
                int existUserNamesCount = _masterDbContext.Users.Where(a => a.UserID != userId && a.UserName == userName).Count();
                if (existUserNamesCount == 0 && _clientDbContext != null)
                {
                    existUserNamesCount = _clientDbContext.Users.Where(a => a.UserID != userId && a.UserName == userName).Count();
                }

                if (existUserNamesCount > 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// To get the user details based on user name and userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserWorkAddress GetUserDetailsByUserNameWithUserID(int userId, string userName)
        {
            try
            {
                var getUserLoginDetails = (from m in _masterDbContext.Users.Where(a => a.UserName == userName && a.UserID == userId && a.IsDelete == false && a.LockOutEnabled == false)
                                           join n in _masterDbContext.UserRoles on m.UserID equals n.UserID
                                           join p in _masterDbContext.Roles on n.RoleID equals p.RoleID
                                           join w in _masterDbContext.WorkAddress on m.UserID equals w.UserID
                                           select new UserWorkAddress
                                           {
                                               AccessFailedCount = m.AccessFailedCount,
                                               Address1 = w.Address1,
                                               Address2 = w.Address2,
                                               CityId = w.CityId,
                                               CreatedBy = m.CreatedBy,
                                               CreatedDate = m.CreatedDate,
                                               DOB = m.DOB,
                                               EmailAddress = m.EmailAddress,
                                               EmailConfirmed = m.EmailConfirmed,
                                               FirstName = m.FirstName,
                                               Gender = m.Gender,
                                               IsDelete = m.IsDelete,
                                               LastName = m.LastName,
                                               LocationID = w.LocationID,
                                               LockOutEnabled = m.LockOutEnabled,
                                               MiddleName = m.MiddleName,
                                               Mobile = m.Mobile,
                                               OtherName = m.OtherName,
                                               // Password = m.Password,
                                               PhoneNumber = m.PhoneNumber,
                                               PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                               ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                               ReportingAuthorityName = m.ReportingAuthorityName,
                                               ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                               RoleID = p.RoleID,
                                               StateId = w.StateId,
                                               UpdatedBy = m.UpdatedBy,
                                               UpdatedDate = m.UpdatedDate,
                                               UserID = m.UserID,
                                               UserName = m.UserName,
                                               WorkAddressID = w.WorkAddressID,
                                               WorkPhone = w.WorkPhone,
                                               Zip = w.Zip,
                                               IsPcrUser = m.IsPcrUser,
                                               ProfileImage = m.ProfileImage

                                           }).FirstOrDefault();
                if (getUserLoginDetails == null)
                {
                    getUserLoginDetails = (from m in _clientDbContext.Users.Where(a => a.UserName == userName && a.UserID == userId && a.IsDelete == false && a.LockOutEnabled == false)
                                           join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                           join p in _clientDbContext.Roles on n.RoleID equals p.RoleID
                                           join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                           select new UserWorkAddress
                                           {
                                               AccessFailedCount = m.AccessFailedCount,
                                               Address1 = w.Address1,
                                               Address2 = w.Address2,
                                               CityId = w.CityId,
                                               CreatedBy = m.CreatedBy,
                                               CreatedDate = m.CreatedDate,
                                               DOB = m.DOB,
                                               EmailAddress = m.EmailAddress,
                                               EmailConfirmed = m.EmailConfirmed,
                                               FirstName = m.FirstName,
                                               Gender = m.Gender,
                                               IsDelete = m.IsDelete,
                                               LastName = m.LastName,
                                               LocationID = w.LocationID,
                                               LockOutEnabled = m.LockOutEnabled,
                                               MiddleName = m.MiddleName,
                                               Mobile = m.Mobile,
                                               OtherName = m.OtherName,
                                               // Password = m.Password,
                                               PhoneNumber = m.PhoneNumber,
                                               PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                               ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                               ReportingAuthorityName = m.ReportingAuthorityName,
                                               ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                               RoleID = p.RoleID,
                                               StateId = w.StateId,
                                               UpdatedBy = m.UpdatedBy,
                                               UpdatedDate = m.UpdatedDate,
                                               UserID = m.UserID,
                                               UserName = m.UserName,
                                               WorkAddressID = w.WorkAddressID,
                                               WorkPhone = w.WorkPhone,
                                               Zip = w.Zip,
                                               IsPcrUser = m.IsPcrUser,
                                               ProfileImage = m.ProfileImage
                                           }).FirstOrDefault();
                }
                return getUserLoginDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To checking the user name except userId.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistUserNameWithouUserId(string userName)
        {
            try
            {
                int existUserNamesCount = _masterDbContext.Users.Where(a => a.UserName == userName && a.IsDelete == false).Count();
                if (existUserNamesCount == 0 && _clientDbContext != null)
                {
                    existUserNamesCount = _clientDbContext.Users.Where(a => a.UserName == userName && a.IsDelete == false).Count();
                }

                if (existUserNamesCount > 0)
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
        /// To update the user details.
        /// </summary>
        /// <param name="user"></param>
        public void ModifiedUser(User user)
        {
            try
            {
                //var dbContext = _clientDbContext == null ? _masterDbContext : _clientDbContext;
                var userDetails = _masterDbContext.Users.Where(a => a.UserID == user.UserID && a.UserName == user.UserName && a.Password == user.Password).FirstOrDefault();
                if (userDetails != null)
                {
                    _masterDbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _masterDbContext.SaveChanges();
                }
                else
                {
                    _clientDbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdatedUserPassWords(User user)
        {
            try
            {
                var userDetails = _masterDbContext.Users.Where(a => a.UserID == user.UserID && a.UserName == user.UserName).FirstOrDefault();
                if (userDetails != null)
                {
                    _masterDbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _masterDbContext.SaveChanges();
                }
                else
                {
                    _clientDbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the user details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUser(User user)
        {
            try
            {
                _clientDbContext.Users.Add(user);
                _clientDbContext.SaveChanges();
                return user.UserID;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To validate the user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User ValidUserName(string userName)
        {
            try
            {
                User userdetails = _masterDbContext.Users.Where(a => a.UserName == userName && a.IsDelete == false).FirstOrDefault();
                if (userdetails == null && _clientDbContext != null)
                    userdetails = _clientDbContext.Users.Where(a => a.UserName == userName && a.IsDelete == false).FirstOrDefault();

                return userdetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the list of user names.
        /// </summary>
        /// <returns></returns>
        public IList<UserWorkAddress> GetAllUserNames()
        {
            try
            {
                var lstUserNames = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false)
                                    join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                    from p in _clientDbContext.Roles.Where(a => a.RoleID == n.RoleID && a.RoleID != 1)
                                    join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                    select new UserWorkAddress
                                    {
                                        UserID = m.UserID,
                                        EmailAddress = m.EmailAddress,
                                        FirstName = m.FirstName,
                                        MiddleName = m.MiddleName,
                                        LastName = m.LastName,
                                        RoleID = p.RoleID,
                                        WorkAddressID = w.WorkAddressID,
                                        WorkPhone = w.WorkPhone,
                                        PhoneNumber = m.PhoneNumber
                                    }).ToList();
                return lstUserNames.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the list of user names.
        /// </summary>
        /// <returns></returns>
        public IList<UserWorkAddress> GetUserNames()
        {
            try
            {
                var lstUserNames = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false)
                                    join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                    from p in _clientDbContext.Roles.Where(a => a.RoleID == n.RoleID && a.RoleID != 1 && a.RoleID < 4)
                                    join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                    select new UserWorkAddress
                                    {
                                        UserID = m.UserID,
                                        EmailAddress = m.EmailAddress,
                                        FirstName = m.FirstName,
                                        MiddleName = m.MiddleName,
                                        LastName = m.LastName,
                                        RoleID = p.RoleID,
                                        WorkAddressID = w.WorkAddressID,
                                        WorkPhone = w.WorkPhone,
                                        PhoneNumber = m.PhoneNumber
                                    }).ToList();
                return lstUserNames.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the user login details.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserViewModel GetUserLoginDetails(string userName, string password)
        {
            try
            {
                //if (_clientDbContext != null && _clientDbContext.Database.Exists())
                //{
                    var userDetails = GetUserLoginDetails(_masterDbContext, userName, password);
                    if (userDetails == null && _clientDbContext != null)
                        userDetails = GetUserLoginDetails(_clientDbContext, userName, password);

                    return userDetails;
                //}
                //else
                //{
                //     throw new Exception("Database not Exists for this client");
                    
                //}


            }
            catch
            {
                throw;
            }
        }

        public UserViewModel GetUserLoginDetails(UMSContext dbContext, string userName, string password)
        {
            try
            {
                var getUserLoginDetails = (from m in dbContext.Users.Where(a => a.UserName == userName && a.Password == password && a.IsDelete == false && a.LockOutEnabled == false)
                                           join n in dbContext.UserRoles on m.UserID equals n.UserID
                                           join p in dbContext.Roles on n.RoleID equals p.RoleID
                                           join w in dbContext.WorkAddress on m.UserID equals w.UserID
                                           select new UserViewModel
                                           {
                                               AccessFailedCount = m.AccessFailedCount,
                                               Address1 = w.Address1,
                                               Address2 = w.Address2,
                                               CityId = w.CityId,
                                               CreatedBy = m.CreatedBy,
                                               CreatedDate = m.CreatedDate,
                                               DOB = m.DOB,
                                               EmailAddress = m.EmailAddress,
                                               EmailConfirmed = m.EmailConfirmed,
                                               FirstName = m.FirstName,
                                               Gender = m.Gender,
                                               IsDelete = m.IsDelete,
                                               LastName = m.LastName,
                                               LocationID = w.LocationID,
                                               LockOutEnabled = m.LockOutEnabled,
                                               MiddleName = m.MiddleName,
                                               Mobile = m.Mobile,
                                               OtherName = m.OtherName,
                                               // Password = m.Password,
                                               PhoneNumber = m.PhoneNumber,
                                               PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                               ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                               ReportingAuthorityName = m.ReportingAuthorityName,
                                               ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                               RoleID = p.RoleID,
                                               StateId = w.StateId,
                                               UpdatedBy = m.UpdatedBy,
                                               UpdatedDate = m.UpdatedDate,
                                               UserID = m.UserID,
                                               UserName = m.UserName,
                                               WorkAddressID = w.WorkAddressID,
                                               WorkPhone = w.WorkPhone,
                                               Zip = w.Zip,
                                               IsPcrUser = false
                                           }).FirstOrDefault();
                return getUserLoginDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To check the password of the user in DB based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>      
        /// <returns></returns>
        public bool DoesMatchUserPassword(int userId, string password)
        {
            try
            {
                int passwordMatchCount = _masterDbContext.Users.Where(a => a.UserID == userId && a.Password == password).Count();
                if (passwordMatchCount == 1)
                    return true;
                else if (_clientDbContext != null)
                    passwordMatchCount = _clientDbContext.Users.Where(a => a.UserID == userId && a.Password == password).Count();

                if (passwordMatchCount == 1)
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
        /// To get the login user detials of the PCR.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserWorkAddress GetUserDetails(string userName)
        {
            try
            {
                var userDetails = GetUserLoginDetails(_masterDbContext, userName);
                if (userDetails == null && _clientDbContext != null)
                {
                    userDetails = GetUserLoginDetails(_clientDbContext, userName);
                }
                return userDetails;
            }
            catch
            {
                throw;
            }
        }

        public UserWorkAddress GetUserLoginDetails(UMSContext dbContext, string userName)
        {
            try
            {
                var getUserLoginDetails = (from m in dbContext.Users.Where(a => a.UserName == userName && a.IsDelete == false && a.LockOutEnabled == false)
                                           join n in dbContext.UserRoles on m.UserID equals n.UserID
                                           join p in dbContext.Roles on n.RoleID equals p.RoleID
                                           join w in dbContext.WorkAddress on m.UserID equals w.UserID
                                           select new UserWorkAddress
                                           {
                                               AccessFailedCount = m.AccessFailedCount,
                                               Address1 = w.Address1,
                                               Address2 = w.Address2,
                                               CityId = w.CityId,
                                               CreatedBy = m.CreatedBy,
                                               CreatedDate = m.CreatedDate,
                                               DOB = m.DOB,
                                               EmailAddress = m.EmailAddress,
                                               EmailConfirmed = m.EmailConfirmed,
                                               FirstName = m.FirstName,
                                               Gender = m.Gender,
                                               IsDelete = m.IsDelete,
                                               LastName = m.LastName,
                                               LocationID = w.LocationID,
                                               LockOutEnabled = m.LockOutEnabled,
                                               MiddleName = m.MiddleName,
                                               Mobile = m.Mobile,
                                               OtherName = m.OtherName,
                                               PhoneNumber = m.PhoneNumber,
                                               PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                               ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                               ReportingAuthorityName = m.ReportingAuthorityName,
                                               ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                               RoleID = p.RoleID,
                                               StateId = w.StateId,
                                               UpdatedBy = m.UpdatedBy,
                                               UpdatedDate = m.UpdatedDate,
                                               UserID = m.UserID,
                                               UserName = m.UserName,
                                               WorkAddressID = w.WorkAddressID,
                                               WorkPhone = w.WorkPhone,
                                               Zip = w.Zip,
                                               IsPcrUser = m.IsPcrUser
                                           }).FirstOrDefault();
                return getUserLoginDetails;
            }
            catch
            {
                throw;
            }
        }

        public SessionDetails GetUserInfo(string UserName)
        {
            try
            {
                if (_clientDbContext != null)
                {
                    var Userinfo = _clientDbContext.Users.Where(u=>u.UserName == UserName).Select(s => new SessionDetails { UserId = s.UserID, UserName = s.UserName,IsFirstLogin = s.IsFirstLogin }).FirstOrDefault();

                    return Userinfo;
                }
            }
            catch
            {
                throw;
            }

            return null;
        }

        /// <summary>
        /// To validate the user credinitials.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User ValidUser(string userName, string password)
        {
            try
            {
                _masterDbContext.Configuration.ProxyCreationEnabled = false;
                var userDetails = _masterDbContext.Users.Where(a => a.UserName == userName && a.Password == password && a.IsDelete == false).FirstOrDefault();
                if (userDetails == null && _clientDbContext != null)
                {
                    _clientDbContext.Configuration.ProxyCreationEnabled = false;
                    userDetails = _clientDbContext.Users.Where(a => a.UserName == userName && a.Password == password && a.IsDelete == false).FirstOrDefault();
                }
                return userDetails;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// To check recordId exist in DB.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public User ValidateUserId(long recordId)
        {
            try
            {
                var userDetails = _masterDbContext.Users.Where(a => a.PcrRecordId == recordId && a.IsDelete == false).FirstOrDefault();
                if (userDetails == null)
                {
                    var linkedPcrUser = _masterDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == recordId).FirstOrDefault();
                    if (linkedPcrUser != null)
                        userDetails = _masterDbContext.Users.Where(a => a.UserID == linkedPcrUser.UserID && a.IsDelete == false).FirstOrDefault();
                }

                if (userDetails == null && _clientDbContext != null)
                {
                    userDetails = _clientDbContext.Users.Where(a => a.PcrRecordId == recordId && a.IsDelete == false).FirstOrDefault();
                    if (userDetails == null)
                    {
                        var linkedPcrUser = _clientDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == recordId).FirstOrDefault();
                        if (linkedPcrUser != null)
                            userDetails = _clientDbContext.Users.Where(a => a.UserID == linkedPcrUser.UserID && a.IsDelete == false).FirstOrDefault();
                    }

                }
                return userDetails;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// To check the PCR record Id in DB baesd on userId.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ExistPcrRecordIdWithUserID(long pcrRecordId, int userId)
        {
            try
            {
                int existUserNamesCount = _masterDbContext.Users.Where(a => a.PcrRecordId == pcrRecordId && a.UserID != userId).Count();
                if (existUserNamesCount > 0)
                    return false;
                else
                    existUserNamesCount = _masterDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == pcrRecordId && a.UserID != userId).Count();

                if (existUserNamesCount == 0 && _clientDbContext != null)
                {
                    existUserNamesCount = _clientDbContext.Users.Where(a => a.PcrRecordId == pcrRecordId && a.UserID != userId).Count();
                    if (existUserNamesCount > 0)
                        return false;
                    else
                        existUserNamesCount = _clientDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == pcrRecordId && a.UserID != userId).Count();

                    if (existUserNamesCount > 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To check the pcr record Id in DB.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <returns></returns>
        public bool ExistPcrRecordIdWithOutUserID(long pcrRecordId)
        {
            try
            {
                int existUserNamesCount = _masterDbContext.Users.Where(a => a.PcrRecordId == pcrRecordId).Count();
                if (existUserNamesCount > 0)
                    return false;
                else
                    existUserNamesCount = _masterDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == pcrRecordId).Count();

                if (existUserNamesCount == 0 && _clientDbContext != null)
                {
                    existUserNamesCount = _clientDbContext.Users.Where(a => a.PcrRecordId == pcrRecordId).Count();
                    if (existUserNamesCount > 0)
                        return false;
                    else
                        existUserNamesCount = _clientDbContext.LinkedPCRUsers.Where(a => a.PcrRecordId == pcrRecordId).Count();

                    if (existUserNamesCount > 0)
                        return false;
                    else
                        return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the multiple users at a time in DB.
        /// </summary>
        /// <param name="linkedPcrUser"></param>
        /// <param name="userId"></param>
        public void AddPcrLinkedUser(List<LinkedPCRUsers> linkedPcrUser, int userId)
        {
            try
            {
                var linkedUsers = _clientDbContext.LinkedPCRUsers.Where(a => a.UserID == userId).ToList();
                if (linkedUsers != null)
                {
                    foreach (var item in linkedUsers)
                    {
                        _clientDbContext.LinkedPCRUsers.Remove(item);
                        _clientDbContext.SaveChanges();
                    }
                }
                foreach (var item in linkedPcrUser)
                {
                    _clientDbContext.LinkedPCRUsers.Add(item);
                    _clientDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddOnBoardingEmployeeSteps(List<OnBoardingEmployeeSteps> listOnBoardingEmployeeSteps, int userId)
        {
            try
            {
                var listonboardEmpSteps = _clientDbContext.OnBoardingEmployeeSteps.Where(a => a.UserID == userId).ToList();
                if (listonboardEmpSteps != null)
                {
                    foreach (var item in listonboardEmpSteps)
                    {
                        _clientDbContext.OnBoardingEmployeeSteps.Remove(item);
                        _clientDbContext.SaveChanges();
                    }
                }
                foreach (var item in listOnBoardingEmployeeSteps)
                {
                    _clientDbContext.OnBoardingEmployeeSteps.Add(item);
                    _clientDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void AddEmployeeSelectedDocumentPackages(List<I9AcceptableDocumentPackages> listOnBoardingEmployeeSteps, int userId)
        {
            try
            {
                var listonboardEmpSteps = _clientDbContext.I9AcceptableDocumentPackages.Where(a => a.UserId == userId).ToList();
                if (listonboardEmpSteps != null)
                {
                    foreach (var item in listonboardEmpSteps)
                    {
                        _clientDbContext.I9AcceptableDocumentPackages.Remove(item);
                        _clientDbContext.SaveChanges();
                    }
                }
                foreach (var item in listOnBoardingEmployeeSteps)
                {
                    int parentId = item.DocumentPackageID;
                    var lstPackages = _clientDbContext.DocumentPackages.Where(a => a.ParentId == item.DocumentPackageID && a.IsDeleted == false).ToList();
                    foreach (var item1 in lstPackages)
                    {
                        item.DocumentPackageID = item1.DocumentPackageID;
                        item.ParentID = parentId;
                        _clientDbContext.I9AcceptableDocumentPackages.Add(item);
                        _clientDbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// To update the user onboard status in DB based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateUserOnboardStatus(int userId)
        {
            try
            {
                var userDetails = _clientDbContext.Users.Where(a => a.UserID == userId && a.IsDelete == false && a.LockOutEnabled == false).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.IsOnboardCompleted = true;
                    _clientDbContext.Entry(userDetails).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateManagerApprovalStatus(int userId)
        {
            try
            {
                var userDetails = _clientDbContext.Users.Where(a => a.UserID == userId && a.IsDelete == false && a.LockOutEnabled == false).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.IsManagerApproved = true;
                    _clientDbContext.Entry(userDetails).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        //public User getimage(int userId)
        //{
        //    try
        //    {
        //        //  var dbContext = _clientDbContext == null ? _masterDbContext : _clientDbContext;
        //        var userDetails = _masterDbContext.Users.Find(userId);
        //        if (userDetails == null && _clientDbContext != null)
        //            userDetails = _clientDbContext.Users.Where(a => a.UserID == userId).FirstOrDefault();

        //        return userDetails;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public DashboardDetails GetOnboardDashboardDetails(string userName, int roleId)
        {
            try
            {
                var userRoles = _clientDbContext.UserRoles.Where(a => a.RoleID == 4).ToList();
                var users = _clientDbContext.Users.Where(a => a.IsDelete == false).ToList();
                if (roleId == 3)
                    users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.ReportingAuthorityEmail == userName).ToList();

                SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);
                startDate.Value = DBNull.Value;
                endDate.Value = DBNull.Value;
                var pendingOnboardingCount = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetPendingOnboardingEmployees @FromDate, @ToDate", startDate, endDate).ToList().Count();

                //var pendingOnboardingCount = (from p in users.Where(a => a.IsOnboardCompleted == false)
                //                              from q in userRoles.Where(a => a.UserID == p.UserID)
                //                              select p).Count();

                var pendingE_VerifyCount = (from p in users
                                            from q in userRoles.Where(a => a.UserID == p.UserID)
                                            select p).Count();

                var totalEmployees = (from p in users
                                      from q in userRoles.Where(a => a.UserID == p.UserID)
                                      select p).Count();

                var recentEmployees = (from p in users.Where(a => a.CreatedDate.Month == DateTime.Now.Month).ToList()
                                       from q in userRoles.Where(a => a.UserID == p.UserID)
                                       select p).Count();

                DashboardDetails dashboard = new DashboardDetails();
                dashboard.PendingOnboardingCount = pendingOnboardingCount;
                dashboard.PendingE_VerifyCount = pendingE_VerifyCount;
                dashboard.TotalEmployees = totalEmployees;
                dashboard.RecentEmployeesCount = recentEmployees;
                return dashboard;
            }
            catch
            {
                throw;
            }
        }

        //public DashboardDetails GetOnboardDashboardDetails()
        //{
        //    try
        //    {
        //        var pendingOnboardingCount = (from p in _clientDbContext.Users.Where(a => a.IsDelete == false && a.IsOnboardCompleted == false)
        //                                      from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                                      select p).Count();

        //        var pendingE_VerifyCount = (from p in _clientDbContext.Users.Where(a => a.IsDelete == false)
        //                                    from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                                    select p).Count();

        //        var totalEmployees = (from p in _clientDbContext.Users.Where(a => a.IsDelete == false)
        //                              from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                              select p).Count();
        //        DashboardDetails dashboard = new DashboardDetails();
        //        dashboard.PendingOnboardingCount = pendingOnboardingCount;
        //        dashboard.PendingE_VerifyCount = pendingE_VerifyCount;
        //        dashboard.TotalEmployee = totalEmployees;
        //        return dashboard;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public List<UserMonthWiseCount> GetMothWiseOnboardDetails(int type, string userName,int roleId)
        //{
        //    try
        //    {
        //        List<User> users = new List<User>();
        //        List<UserMonthWiseCount> lstUsersCount = new List<UserMonthWiseCount>();
        //        List<string> monthId = new List<string> { "1","2","3","4","5","6","7","8","9","10","11","12" };

        //        if (type == 2)             //Pending Onboarding Users Count
        //        {
        //            if (roleId == 3)
        //                users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.IsOnboardCompleted == false && a.ReportingAuthorityEmail == userName).ToList();
        //            else
        //                users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.IsOnboardCompleted == false ).ToList();

        //            lstUsersCount = (from p in users
        //                     from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                     group p by p.CreatedDate.Month into g
        //                     select new UserMonthWiseCount
        //                     {
        //                         MonthName = g.Key.ToString(),
        //                         UserCount = g.Count()
        //                     }).ToList();
        //        }
        //        else if(type == 3)         //Pending E-Verify Users Count
        //        {
        //            if (roleId == 3)
        //                users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.ReportingAuthorityEmail == userName).ToList();
        //            else
        //                users = _clientDbContext.Users.Where(a => a.IsDelete == false ).ToList();

        //            lstUsersCount = (from p in users
        //                     from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                     group p by p.CreatedDate.Month into g
        //                     select new UserMonthWiseCount
        //                     {
        //                         MonthName = g.Key.ToString(),
        //                         UserCount = g.Count()
        //                     }).ToList();
        //        }
        //        else if(type == 4)         //Total Employees
        //        {
        //            if (roleId == 3)
        //                users = _clientDbContext.Users.Where(a => a.ReportingAuthorityEmail == userName).ToList();
        //            else
        //                users = _clientDbContext.Users.ToList();

        //            lstUsersCount = (from p in users
        //                     from q in _clientDbContext.UserRoles.Where(a => a.RoleID == 4 && a.UserID == p.UserID)
        //                     group p by p.CreatedDate.Month into g
        //                     select new UserMonthWiseCount
        //                     {
        //                         MonthName = g.Key.ToString(),
        //                         UserCount = g.Count()
        //                     }).ToList();
        //        }

        //        var result= (from p in monthId
        //                    join q in lstUsersCount on p equals q.MonthName into a
        //                    from q1 in a.DefaultIfEmpty()
        //                    select new UserMonthWiseCount
        //                    {
        //                        MonthName=p.ToString(),
        //                        UserCount= q1!= null? q1.UserCount:0
        //                    }).ToList();

        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public List<UserMonthWiseCount> GetMothWiseOnboardDetails(int type, string userName, int roleId)
        {
            try
            {
                List<UserMonthWiseCount> lstUsersCount = new List<UserMonthWiseCount>();
                List<string> monthId = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

                var userRoles = _clientDbContext.UserRoles.Where(a => a.RoleID == 4).ToList();
                var users = _clientDbContext.Users.Where(a => a.IsDelete == false).ToList();
                if (roleId == 3)
                    users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.ReportingAuthorityEmail == userName).ToList();

                if (type == 2)             //Pending Onboarding Users Count
                {
                    SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                    SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);
                    startDate.Value = DBNull.Value;
                    endDate.Value = DBNull.Value;
                    var pendingOnboardingCount = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetPendingOnboardingEmployees @FromDate, @ToDate", startDate, endDate).ToList();

                    lstUsersCount = (from p in pendingOnboardingCount
                                     from q in userRoles.Where(a => a.UserID == p.UserID)
                                     group p by p.CreatedDate.Month into g
                                     select new UserMonthWiseCount { MonthName = g.Key.ToString(), UserCount = g.Count() }).ToList();
                }
                else if (type == 3)         //Pending E-Verify Users Count
                {
                    lstUsersCount = (from p in users
                                     from q in userRoles.Where(a => a.UserID == p.UserID)
                                     group p by p.CreatedDate.Month into g
                                     select new UserMonthWiseCount { MonthName = g.Key.ToString(), UserCount = g.Count() }).ToList();
                }
                else if (type == 4)         //Total Employees
                {
                    lstUsersCount = (from p in users
                                     from q in userRoles.Where(a => a.UserID == p.UserID)
                                     group p by p.CreatedDate.Month into g
                                     select new UserMonthWiseCount { MonthName = g.Key.ToString(), UserCount = g.Count() }).ToList();
                }

                var result = (from p in monthId
                              join q in lstUsersCount on p equals q.MonthName into a
                              from q1 in a.DefaultIfEmpty()
                              select new UserMonthWiseCount
                              {
                                  MonthName = p.ToString(),
                                  UserCount = q1 != null ? q1.UserCount : 0
                              }).ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public IList<UserWorkAddress> GetUsersForDashboard(int roleId, string userName, string databaseId)
        {
            try
            {
                var users = _clientDbContext.Users.Where(a => a.IsDelete == false).ToList();
                if (roleId == 3)
                {
                    users = _clientDbContext.Users.Where(a => a.IsDelete == false && a.ReportingAuthorityEmail == userName).ToList();
                }

                var lstUsers = (from m in users
                                join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
                                from p in _clientDbContext.Roles.Where(a => a.RoleID == n.RoleID && a.RoleID > roleId)
                                join w in _clientDbContext.WorkAddress on m.UserID equals w.UserID
                                select new UserWorkAddress
                                {
                                    UserID = m.UserID,
                                    UserName = m.UserName,
                                    Password = m.Password,
                                    EmailConfirmed = m.EmailConfirmed,
                                    AccessFailedCount = m.AccessFailedCount,
                                    LockOutEnabled = m.LockOutEnabled,
                                    PhoneNumberConfirmed = m.PhoneNumberConfirmed,
                                    EmailAddress = m.EmailAddress,
                                    FirstName = m.FirstName,
                                    MiddleName = m.MiddleName,
                                    LastName = m.LastName,
                                    RoleID = p.RoleID,
                                    WorkAddressID = w.WorkAddressID,
                                    LocationID = w.LocationID,
                                    WorkPhone = w.WorkPhone,
                                    IsDelete = m.IsDelete,
                                    CreatedBy = m.CreatedBy,
                                    CreatedDate = m.CreatedDate,
                                    UpdatedBy = m.UpdatedBy,
                                    UpdatedDate = m.UpdatedDate,
                                    Gender = m.Gender,
                                    Mobile = m.Mobile,
                                    OtherName = m.OtherName,
                                    DOB = m.DOB,
                                    ReportingAuthorityName = m.ReportingAuthorityName,
                                    ReportingAuthorityEmail = m.ReportingAuthorityEmail,
                                    ReportingAuthorityPhone = m.ReportingAuthorityPhone,
                                    Address1 = w.Address1,
                                    Address2 = w.Address2,
                                    StateId = w.StateId,
                                    CityId = w.CityId,
                                    Zip = w.Zip,
                                    DatabaseId = databaseId,
                                    IsOnboardCompleted = m.IsOnboardCompleted
                                }).ToList().OrderByDescending(a => a.UserID).Take(10);
                return lstUsers.ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<UserWorkAddress> GetOnboardingCompletedEmployees(DateTime? fromDate, DateTime? toDate, string databaseId)
        {
            try
            {
                SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);

                if (fromDate == null)
                    startDate.Value = DBNull.Value;//  DBType.Null;
                else
                    startDate.Value = fromDate;

                if (toDate == null)
                    endDate.Value = DBNull.Value;//  DBType.Null;
                else
                    endDate.Value = toDate;

                var lstUsers = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetOnboardingCompletedEmployees @FromDate, @ToDate", startDate, endDate).ToList();

                return lstUsers;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<UserWorkAddress> GetOnboardingInProgressEmployees(DateTime? fromDate, DateTime? toDate, string databaseId)
        {
            try
            {
                SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);

                if (fromDate == null)
                    startDate.Value = DBNull.Value;//  DBType.Null;
                else
                    startDate.Value = fromDate;

                if (toDate == null)
                    endDate.Value = DBNull.Value;//  DBType.Null;
                else
                    endDate.Value = toDate;

                var lstUsers = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetOnboardingInProgressEmployees @FromDate, @ToDate", startDate, endDate).ToList();

                return lstUsers;

            }
            catch
            {
                throw;
            }
        }

        public IList<UserWorkAddress> GetPre_OnboardingEmployees(DateTime? fromDate, DateTime? toDate, string databaseId)
        {
            try
            {
                SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);

                if (fromDate == null)
                    startDate.Value = DBNull.Value;//  DBType.Null;
                else
                    startDate.Value = fromDate;

                if (toDate == null)
                    endDate.Value = DBNull.Value;//  DBType.Null;
                else
                    endDate.Value = toDate;

                var lstUsers = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetPre_OnboardingEmployees @FromDate, @ToDate", startDate, endDate).ToList();

                return lstUsers;

            }
            catch
            {
                throw;
            }
        }

        public IList<UserWorkAddress> GetPendingOnboardingEmployees(DateTime? fromDate, DateTime? toDate, string databaseId)
        {
            try
            {
                SqlParameter startDate = new SqlParameter("@FromDate", SqlDbType.DateTime);
                SqlParameter endDate = new SqlParameter("@ToDate", SqlDbType.DateTime);

                if (fromDate == null)
                    startDate.Value = DBNull.Value;//  DBType.Null;
                else
                    startDate.Value = fromDate;

                if (toDate == null)
                    endDate.Value = DBNull.Value;//  DBType.Null;
                else
                    endDate.Value = toDate;

                var lstUsers = _clientDbContext.Database.SqlQuery<UserWorkAddress>("dbo.GetPendingOnboardingEmployees @FromDate, @ToDate", startDate, endDate).ToList();

                return lstUsers;

            }
            catch
            {
                throw;
            }
        }


        public List<GetPendingOnboardUsers> GetPendingOnboardingUsersCount(int roleId, string databaseId)
        {
            try
            {
                List<GetPendingOnboardUsers> lstUsersCount = new List<GetPendingOnboardUsers>();

                lstUsersCount = (from Employees in _clientDbContext.Users
                                 join Managers in _clientDbContext.Users on Employees.ReportingAuthorityEmail equals Managers.UserName
                                 join n in _clientDbContext.UserRoles on Managers.UserID equals n.UserID
                                 where (roleId == 1 ? ((n.RoleID == 2) || (n.RoleID == 3)) : ((n.RoleID == 3))) && Employees.ReportingAuthorityEmail != null
                                 && Employees.IsDelete != true && Employees.LockOutEnabled != true && Employees.IsOnboardCompleted != true
                                 group n by new { n.RoleID, Managers.ReportingAuthorityEmail } into asd
                                 select new GetPendingOnboardUsers
                                 {
                                     ////Count=a.Count(),
                                     //RoleID = n.RoleID,
                                     //FullName = Employees.UserName  +" "+ Managers.FirstName,
                                     //UserName = Employees.ReportingAuthorityEmail

                                     Count = asd.Count(),
                                     RoleID = asd.Key.RoleID,
                                     FullName = _clientDbContext.Users.Where(a => a.UserName == asd.Key.ReportingAuthorityEmail).FirstOrDefault().LastName + " " + _clientDbContext.Users.Where(a => a.UserName == asd.Key.ReportingAuthorityEmail).FirstOrDefault().FirstName,
                                     UserName = asd.Key.ReportingAuthorityEmail
                                 })
                              .ToList().OrderBy(a => a.RoleID).ToList();

                return lstUsersCount;
            }
            catch
            {
                throw;
            }
        }


        //public List<GetPendingOnboardUsers> GetPendingOnboardingUsersCount(int roleId, string databaseId)
        //{
        //    try
        //    {
        //        List<GetPendingOnboardUsers> lstUsersCount = new List<GetPendingOnboardUsers>();

        //        var usersCount = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false && a.LockOutEnabled == false && a.IsOnboardCompleted == false && a.ReportingAuthorityEmail != null)
        //                          join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
        //                          where roleId == 1 ? ((n.RoleID == 2) || (n.RoleID == 3)) : ((n.RoleID == 3))
        //                          select new { m.ReportingAuthorityEmail, n.RoleID }).GroupBy(a => a)
        //                        .Select(b => new GetPendingOnboardUsers
        //                        {
        //                            Count = b.Count(),
        //                            UserName = b.Key.ReportingAuthorityEmail,
        //                            RoleID = b.Key.RoleID,

        //                        })
        //                        .ToList().OrderBy(a => a.RoleID).ToList();

        //        var usersCount1 = (from m in _clientDbContext.Users.Where(a => a.IsDelete == false && a.LockOutEnabled == false && a.IsOnboardCompleted == false && a.ReportingAuthorityEmail != null)
        //                           join n in _clientDbContext.UserRoles on m.UserID equals n.UserID
        //                           where roleId == 1 ? ((n.RoleID == 2) || (n.RoleID == 3)) : ((n.RoleID == 3))
        //                           group n by new { n.RoleID, m.ReportingAuthorityEmail } into asd
        //                           select new GetPendingOnboardUsers
        //                           {
        //                               RoleID = asd.Key.RoleID,
        //                               FullName = _clientDbContext.Users.Where(a => a.UserName == asd.Key.ReportingAuthorityEmail).FirstOrDefault().FirstName + " " + _clientDbContext.Users.Where(a => a.UserName == asd.Key.ReportingAuthorityEmail).FirstOrDefault().LastName,
        //                               UserName = asd.Key.ReportingAuthorityEmail
        //                           })
        //                      .ToList();

        //        foreach (var item in usersCount)
        //        {
        //            var userDetails = _clientDbContext.Users.Where(a => a.UserName == item.UserName && a.IsDelete == false && a.LockOutEnabled == false).FirstOrDefault();
        //            if (userDetails != null)
        //            {
        //                lstUsersCount.Add(new GetPendingOnboardUsers { Count = item.Count, RoleID = item.RoleID, UserName = item.UserName, FullName = userDetails.LastName + " " + userDetails.FirstName });
        //            }
        //        }
        //        return lstUsersCount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return lstUsersCount;
        //}

        public IList<User> GetSuperAminDetails()
        {
            try
            {
                _masterDbContext.Configuration.ProxyCreationEnabled = false;
                var superAdminDetails = _masterDbContext.Users.ToList();
                return superAdminDetails;
            }
            catch
            {
                throw;
            }

        }

        public IList<User> GetSuperAminDetailsServices()
        {
            try
            {
                _masterDbContext.Configuration.ProxyCreationEnabled = false;
                var superAdminDetails = _masterDbContext.Users.ToList();
                return superAdminDetails;
            }
            catch
            {
                throw;
            }

        }

        public IList<UserWorkAddress> GetClientAminDetails()
        {
            try
            {

                var query = (from a in _clientDbContext.UserRoles
                             where a.RoleID == 2
                             join b in _clientDbContext.Users on a.UserID equals b.UserID
                             where b.IsDelete == false
                             select new UserWorkAddress
                             {
                                 RoleID = a.RoleID,
                                 FirstName = b.FirstName,
                                 LastName = b.LastName,
                                 UserName = b.UserName,
                                 UserID = b.UserID,

                             }).ToList();


                return query;
            }
            catch
            {
                throw;
            }

        }

        public IList<UserWorkAddress> GetClientAminDetailsServices()
        {
            try
            {

                var query = (from a in _clientDbContext.UserRoles
                             where a.RoleID == 2
                             join b in _clientDbContext.Users on a.UserID equals b.UserID
                             where b.IsDelete == false
                             select new UserWorkAddress
                             {
                                 RoleID = a.RoleID,
                                 FirstName = b.FirstName,
                                 LastName = b.LastName,
                                 UserName = b.UserName,
                                 UserID = b.UserID,

                             }).ToList();


                return query;
            }
            catch
            {
                throw;
            }

        }

        public User GetMailIdDetails(string mailId)
        {
            try
            {

                var userDetails = _clientDbContext.Users.Where(a => a.UserName == mailId).FirstOrDefault();
                if (userDetails != null)
                {
                    return userDetails;
                }
                else
                {
                    var userDetail = _masterDbContext.Users.Where(a => a.UserName == mailId).FirstOrDefault();
                    return userDetail;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void UpdateImage(User uploadDocuments)
        {
            try
            {
                var doc = _masterDbContext.Users.FirstOrDefault(a => a.UserName == uploadDocuments.UserName);

                if (doc != null)
                {
                    var doc1 = _masterDbContext.Users.Find(uploadDocuments.UserID);
                    if (uploadDocuments.ProfileImage != null)
                        doc1.ProfileImage = uploadDocuments.ProfileImage;

                    doc.UpdatedDate = DateTime.Now;

                    _masterDbContext.Entry(doc1).State = System.Data.Entity.EntityState.Modified;
                    _masterDbContext.SaveChanges();
                }
                else
                {
                    doc = _clientDbContext.Users.FirstOrDefault(a => a.UserName == uploadDocuments.UserName);
                    if (uploadDocuments.ProfileImage != null)
                        doc.ProfileImage = uploadDocuments.ProfileImage;

                    doc.UpdatedDate = DateTime.Now;

                    _clientDbContext.Entry(doc).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NotificationTemplete GetTemplteForSendingCredentials(int typeId)
        {
            try
            {
                SqlParameter type = new SqlParameter("@NotifyId", SqlDbType.Int);
                type.Value = typeId;

                var templete = _clientDbContext.Database.SqlQuery<NotificationTemplete>("exec GetTempleteByNotifyId {0}", typeId);

                return templete.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<EverifyEmloyees> GetEverifyEmployees(string userName)
        {
            try
            {
                SqlParameter userNameField = new SqlParameter("@UserName", SqlDbType.VarChar);

                if (userName == null)
                    userNameField.Value = DBNull.Value;//  DBType.Null;
                else
                    userNameField.Value = userName;

                var lstUsers = _clientDbContext.Database.SqlQuery<EverifyEmloyees>("dbo.GetEverifyEmployees @UserName", userNameField).ToList();


                // var lstUsers = _clientDbContext.Database.SqlQuery<EverifyEmloyees>("exec GetEverifyEmployees {0}", userNameField);


                return lstUsers.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UserWorkAddress> GetAllEmployeesUploadDocPackages(int docPackageId)
        {
            try
            {
                List<UserWorkAddress> lstUsers = new List<UserWorkAddress>();
                lstUsers = (from p in _clientDbContext.I9AcceptableDocumentPackages
                            join q in _clientDbContext.Users on p.UserId equals q.UserID
                            join r in _clientDbContext.UserRoles on q.UserID equals r.UserID
                            where p.ParentID == docPackageId
                            group new { q.UserID, q.FirstName, q.LastName, q.MiddleName, q.UserName, r.RoleID, p.CreatedDate }
                            by new { q.UserID, q.FirstName, q.LastName, q.MiddleName, q.UserName, r.RoleID, p.CreatedDate }
                            into grp
                            select new UserWorkAddress
                            {
                                UserID = grp.Key.UserID,
                                RoleID = grp.Key.RoleID,
                                UserName = grp.Key.UserName,
                                FirstName = grp.Key.FirstName,
                                LastName = grp.Key.LastName,
                                MiddleName = grp.Key.MiddleName,
                                CreatedDate = (DateTime)grp.Key.CreatedDate
                            }).ToList();

                return lstUsers;
            }
            catch
            {
                throw;
            }
        }


    }

    //public class class2: UserRepository
    //{   
    //    class2 obj = new class2();



    //}
}
