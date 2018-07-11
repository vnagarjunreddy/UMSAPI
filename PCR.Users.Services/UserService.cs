
using Newtonsoft.Json;
using PCR.Users.Data;
using PCR.Users.Model.ViewModels;
using PCR.Users.Models;
using PCR.Users.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PCR.Users.Services
{
    public class UserService
    {
      
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        GetSessionDetails _sessionManager = new GetSessionDetails();
        
        public UserService()
        {
           
        }

        /// <summary>
        /// To get the all user details.
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<Model.ViewModels.UserWorkAddress> GetUsers(int roleID, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetUsers(roleID, session.DatabaseId());
                        return lstUsers;
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
        /// To get the all user details.
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<Model.ViewModels.UserWorkAddress> GetManageUsers(int roleID, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetManageUsers(roleID, session.DatabaseId());
                        return lstUsers;
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
        /// To get the user details by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public UserWorkAddress GetUserByID(int userId, string accessToken)
        {
            UserWorkAddress userWorkAddrs = new UserWorkAddress();
            User userDetails = null;
            WorkAddress workAddressDetails = null;
            UserRepository userRepository = null;
            WorkAddressRepository workAddressRepository = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    userRepository = new UserRepository(session.DatabaseId());
                    workAddressRepository = new WorkAddressRepository(session.DatabaseId());

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetUserIDDetails(userId);

                    using (var wrkAdrsRepository = workAddressRepository)
                        workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByID(userId);

                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;
                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                        userWorkAddrs.ProfileImage = userDetails.ProfileImage;
                    }
                }
                else
                {
                    userRepository = new UserRepository();
                    workAddressRepository = new WorkAddressRepository();

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetUserIDDetails(userId);

                    using (var wrkAdrsRepository = workAddressRepository)
                        workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByID(userId);

                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;

                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                        userWorkAddrs.ProfileImage = userDetails.ProfileImage;
                    }
                }
                return userWorkAddrs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //public UserWorkAddress GetUserByMailId(string MailId, string accessToken)
        //{
        //    UserWorkAddress userWorkAddrs = new UserWorkAddress();
        //    User userDetails = null;
        //    WorkAddress workAddressDetails = null;
        //    UserRepository userRepository = null;
        //    WorkAddressRepository workAddressRepository = null;
        //    try
        //    {
        //        var session = _sessionManager.GetSessionValues(accessToken);
        //        if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
        //        {
        //            userRepository = new UserRepository(session.DatabaseId());
        //            workAddressRepository = new WorkAddressRepository(session.DatabaseId());

        //            using (var userRepo = userRepository)
        //                userDetails = userRepo.GetMailIdDetails(MailId);

        //            using (var wrkAdrsRepository = workAddressRepository)
        //                workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailId(userDetails.UserID);

        //            if (userDetails == null && workAddressDetails == null)
        //                return null;
        //            else
        //            {
        //                userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
        //                userWorkAddrs.Address2 = workAddressDetails.Address2;
        //                userWorkAddrs.CityId = workAddressDetails.CityId;
        //                userWorkAddrs.CreatedBy = userDetails.CreatedBy;
        //                userWorkAddrs.CreatedDate = userDetails.CreatedDate;
        //                userWorkAddrs.DOB = userDetails.DOB;
        //                userWorkAddrs.EmailAddress = userDetails.EmailAddress;
        //                userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
        //                userWorkAddrs.FirstName = userDetails.FirstName;
        //                userWorkAddrs.Gender = userDetails.Gender;
        //                userWorkAddrs.IsDelete = userDetails.IsDelete;
        //                userWorkAddrs.LastName = userDetails.LastName;
        //                userWorkAddrs.LocationID = workAddressDetails.LocationID;
        //                userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
        //                userWorkAddrs.MiddleName = userDetails.MiddleName;
        //                userWorkAddrs.Mobile = userDetails.Mobile;
        //                userWorkAddrs.OtherName = userDetails.OtherName;
        //                userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
        //                userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
        //                userWorkAddrs.RoleID = workAddressDetails.RoleID;
        //                userWorkAddrs.StateId = workAddressDetails.StateId;
        //                userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
        //                userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
        //                userWorkAddrs.UserID = userDetails.UserID;
        //                userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
        //                userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
        //                userWorkAddrs.Zip = workAddressDetails.Zip;
        //                userWorkAddrs.Address1 = workAddressDetails.Address1;
        //                userWorkAddrs.UserName = userDetails.UserName;
        //                userWorkAddrs.Password = userDetails.Password;
        //                userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
        //                userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
        //                userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;
        //                userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
        //                userWorkAddrs.PCRUserName = userDetails.PCRUserName;
        //                userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
        //                userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
        //                userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
        //            }
        //        }
        //        else
        //        {
        //            userRepository = new UserRepository();
        //            workAddressRepository = new WorkAddressRepository();

        //            using (var userRepo = userRepository)
        //                userDetails = userRepo.GetMailIdDetails(MailId);

        //            using (var wrkAdrsRepository = workAddressRepository)
        //                workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailId(userDetails.UserID);

        //            if (userDetails == null && workAddressDetails == null)
        //                return null;
        //            else
        //            {
        //                userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
        //                userWorkAddrs.Address2 = workAddressDetails.Address2;
        //                userWorkAddrs.CityId = workAddressDetails.CityId;
        //                userWorkAddrs.CreatedBy = userDetails.CreatedBy;
        //                userWorkAddrs.CreatedDate = userDetails.CreatedDate;
        //                userWorkAddrs.DOB = userDetails.DOB;
        //                userWorkAddrs.EmailAddress = userDetails.EmailAddress;
        //                userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
        //                userWorkAddrs.FirstName = userDetails.FirstName;
        //                userWorkAddrs.Gender = userDetails.Gender;
        //                userWorkAddrs.IsDelete = userDetails.IsDelete;
        //                userWorkAddrs.LastName = userDetails.LastName;
        //                userWorkAddrs.LocationID = workAddressDetails.LocationID;
        //                userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
        //                userWorkAddrs.MiddleName = userDetails.MiddleName;
        //                userWorkAddrs.Mobile = userDetails.Mobile;
        //                userWorkAddrs.OtherName = userDetails.OtherName;
        //                userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
        //                userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
        //                userWorkAddrs.RoleID = workAddressDetails.RoleID;
        //                userWorkAddrs.StateId = workAddressDetails.StateId;
        //                userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
        //                userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
        //                userWorkAddrs.UserID = userDetails.UserID;
        //                userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
        //                userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
        //                userWorkAddrs.Zip = workAddressDetails.Zip;
        //                userWorkAddrs.Address1 = workAddressDetails.Address1;
        //                userWorkAddrs.UserName = userDetails.UserName;
        //                userWorkAddrs.Password = userDetails.Password;
        //                userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
        //                userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
        //                userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;

        //                userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
        //                userWorkAddrs.PCRUserName = userDetails.PCRUserName;
        //                userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
        //                userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
        //                userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
        //            }
        //        }
        //        return userWorkAddrs;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        /// <summary>
        /// To add the user details.
        /// </summary>
        /// <param name="userWorkAddress"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddUserDetails(UserWorkAddress userWorkAddress, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var roleRepository = new RoleRepository(session.DatabaseId()))
                    {
                        int roleId = Convert.ToInt32(userWorkAddress.RoleID);
                        var role = roleRepository.GetRoleIDDetails(roleId);   //To checking the roleId details from DB if it's exist or not.
                        if (role == null || roleId == 1)
                            throw new Exception("Please select valid role.");
                        else
                        {
                            using (var userRepository = new UserRepository(session.DatabaseId()))
                            {
                                int existUserNameCount = userRepository.ExistUserName(userWorkAddress.UserName);
                                if (existUserNameCount > 0)
                                    throw new Exception("User name is already exist.");
                                else
                                {
                                    User user = new User();
                                    user.UserName = userWorkAddress.UserName;
                                    user.Password = Encrypt(userWorkAddress.Password);     // To encrypt the password using MD5 algorithm.
                                    user.AccessFailedCount = 0;
                                    user.EmailAddress = userWorkAddress.UserName;
                                    user.UserName = userWorkAddress.UserName;
                                    user.CreatedBy = userWorkAddress.CreatedBy;
                                    user.EmailConfirmed = true;
                                    user.FirstName = userWorkAddress.FirstName;
                                    user.IsDelete = false;
                                    user.LastName = userWorkAddress.LastName;
                                    user.LockOutEnabled = false;
                                    user.MiddleName = userWorkAddress.MiddleName;
                                    user.PhoneNumber = userWorkAddress.PhoneNumber;
                                    user.PhoneNumberConfirmed = true;
                                    user.DOB = userWorkAddress.DOB;
                                    user.OtherName = userWorkAddress.OtherName;
                                    user.Gender = userWorkAddress.Gender;
                                    user.Mobile = userWorkAddress.Mobile;                                   
                                    user.CreatedDate = DateTime.Now;
                                    user.UpdatedBy = null;
                                    user.UpdatedDate = DateTime.Now;
                                    user.ReportingAuthorityEmail = userWorkAddress.ReportingAuthorityEmail;
                                    user.ReportingAuthorityName = userWorkAddress.ReportingAuthorityName;
                                    user.ReportingAuthorityPhone = userWorkAddress.ReportingAuthorityPhone;
                                    user.DatabaseId = userWorkAddress.DatabaseId;
                                    user.PcrId = userWorkAddress.PcrId;

                                    //To assigning the pcr user to onboarding
                                    user.IsPcrUser = userWorkAddress.IsPcrUser;
                                    user.PcrDatabaseId = userWorkAddress.PcrDatabaseId;
                                    user.PcrRecordId = userWorkAddress.PcrRecordId;
                                    user.PCRUserName = userWorkAddress.PCRUserName;
                                   // user. = userWorkAddress.JobTitleID;

                                    int userId = userRepository.AddUser(user);     // To add the user details in DB.

                                    if (userWorkAddress.LinkedPcrUsers != null)
                                    {
                                        List<LinkedPCRUsers> listLinkedPcrUsers = new List<LinkedPCRUsers>();
                                        foreach (var item in userWorkAddress.LinkedPcrUsers)
                                        {
                                            LinkedPCRUsers pcrUser = new LinkedPCRUsers();
                                            pcrUser.UserName = item.UserName;
                                            pcrUser.PcrRecordId = item.PcrRecordId;
                                            pcrUser.PcrDatabaseId = item.PcrDatabaseId;
                                            pcrUser.IsPcrUser = true;
                                            pcrUser.CreatedBy = userWorkAddress.CreatedBy;
                                            pcrUser.CreatedDate = DateTime.Now;
                                            pcrUser.UserID = userId;
                                            pcrUser.UpdatedBy = userWorkAddress.UpdatedBy;
                                            pcrUser.UpdatedDate = DateTime.Now;
                                            listLinkedPcrUsers.Add(pcrUser);
                                        }
                                        if (listLinkedPcrUsers != null)
                                            userRepository.AddPcrLinkedUser(listLinkedPcrUsers, userId);     // To add the linked pcr user details in DB.
                                    }

                                    if (userWorkAddress.ListOnboardEmpSteps != null)
                                    {
                                        List<OnBoardingEmployeeSteps> listOnboardSteps = new List<OnBoardingEmployeeSteps>();
                                        
                                        foreach (var item in userWorkAddress.ListOnboardEmpSteps)
                                        {
                                            OnBoardingEmployeeSteps empSteps = new OnBoardingEmployeeSteps();
                                            empSteps.OnboardStageId = item.OnboardStageId;
                                            empSteps.UserID = userId;
                                            empSteps.UpdatedDate = DateTime.Now;
                                            empSteps.CreatedDate = DateTime.Now;
                                            empSteps.CreatedBy = item.CreatedBy;
                                            empSteps.UpdatedBy = item.UpdatedBy;

                                            listOnboardSteps.Add(empSteps);
                                        }
                                        if (listOnboardSteps != null)
                                            userRepository.AddOnBoardingEmployeeSteps(listOnboardSteps, userId);     // To add the linked pcr user details in DB.
                                    }

                                    if (userWorkAddress.ListI9AcceptableDocumentPackages != null)
                                    {
                                        List<I9AcceptableDocumentPackages> listPackages = new List<I9AcceptableDocumentPackages>();
                                     
                                        foreach (var item in userWorkAddress.ListI9AcceptableDocumentPackages)
                                        {
                                            I9AcceptableDocumentPackages docPackages = new I9AcceptableDocumentPackages();
                                            docPackages.AcceptableDocumentPackageID = item.AcceptableDocumentPackageID;
                                            docPackages.DocumentPackageID = item.DocumentPackageID;
                                            docPackages.UserId = userId;
                                            docPackages.ModifiedDate = DateTime.Now;
                                            docPackages.CreatedDate = DateTime.Now;
                                            docPackages.CreatedBy = item.CreatedBy;
                                            docPackages.ModifiedBy = item.ModifiedBy;

                                            listPackages.Add(docPackages);
                                        }
                                        if (listPackages != null)
                                            userRepository.AddEmployeeSelectedDocumentPackages(listPackages, userId);     // To add the linked pcr user details in DB.
                                    }

                                    using (var workAddressRepository = new WorkAddressRepository(session.DatabaseId()))
                                    {
                                        WorkAddress workAddressDetails = new WorkAddress();
                                        workAddressDetails.UserID = userId;
                                        workAddressDetails.Address1 = userWorkAddress.Address1;
                                        workAddressDetails.Address2 = userWorkAddress.Address2;
                                        workAddressDetails.CityId = userWorkAddress.CityId;
                                        workAddressDetails.StateId = userWorkAddress.StateId;
                                        workAddressDetails.WorkPhone = userWorkAddress.WorkPhone;
                                        workAddressDetails.Zip = userWorkAddress.Zip;
                                        workAddressDetails.RoleID = roleId;
                                        workAddressDetails.LocationID = userWorkAddress.LocationID;
                                        workAddressRepository.AddWorkAddressDetails(workAddressDetails);     //To add the user work address details.
                                    }

                                    using (var userRoleRepository = new UserRoleRepository(session.DatabaseId()))
                                    {
                                        UserRole userRole = new UserRole()
                                        {
                                            RoleID = userWorkAddress.RoleID,
                                            UserID = userId,
                                            CreatedBy = userWorkAddress.CreatedBy,
                                            CreatedDate = DateTime.Now,
                                            UpdatedBy = userWorkAddress.UpdatedBy,
                                            UpdatedDate = DateTime.Now,
                                            DatabaseId = userWorkAddress.DatabaseId,
                                            PcrId = userWorkAddress.PcrId
                                        };
                                        userRoleRepository.AddUserRole(userRole);      // To add the user role details. 
                                        return true;
                                    }
                                }
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
        }

        /// <summary>
        /// To update the user details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userWorkAddress"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateUser(int userId, Model.ViewModels.UserViewModel userWorkAddress, string accessToken)
        {
            UserRepository userRepository = null;
            WorkAddressRepository workAddressRepository = null;
            UserRoleRepository userRoleRepository = null;
            bool isUpdateUser = false;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR )
                {
                    userRepository = new UserRepository(session.DatabaseId());
                    workAddressRepository = new WorkAddressRepository(session.DatabaseId());

                    using (var userRepo = userRepository)
                    {   
                        var userDetails = userRepository.GetUserIDDetails(userId);     // To get the user details.

                        using (var workAddressRepo = workAddressRepository)
                        {
                            var workAddress = workAddressRepository.GetWorkAddressDetailsByID(userId);      // To get the user work address details.

                            if (userDetails == null && workAddress == null)
                                return false;
                            else
                            {
                                userDetails.EmailConfirmed = true;
                                userDetails.UpdatedBy = userWorkAddress.UpdatedBy;
                                userDetails.FirstName = userWorkAddress.FirstName;
                                userDetails.IsDelete = userDetails.IsDelete;
                                userDetails.LastName = userWorkAddress.LastName;
                                userDetails.LockOutEnabled = userDetails.LockOutEnabled;
                                userDetails.MiddleName = userWorkAddress.MiddleName;
                                userDetails.PhoneNumber = userWorkAddress.PhoneNumber;
                                userDetails.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                                userDetails.DOB = userWorkAddress.DOB;
                                userDetails.OtherName = userWorkAddress.OtherName;
                                userDetails.Gender = userWorkAddress.Gender;
                                userDetails.Mobile = userWorkAddress.Mobile;
                                userDetails.ReportingAuthorityEmail = userWorkAddress.ReportingAuthorityEmail;
                                userDetails.ReportingAuthorityName = userWorkAddress.ReportingAuthorityName;
                                userDetails.ReportingAuthorityPhone = userWorkAddress.ReportingAuthorityPhone;
                                userDetails.CreatedDate = userDetails.CreatedDate;
                                userDetails.UpdatedDate = DateTime.Now;
                                userDetails.UserID = userId;
                                userDetails.UserName = userDetails.UserName;
                                userDetails.EmailAddress = userDetails.EmailAddress;
                                userDetails.Password = userDetails.Password;
                                userDetails.DatabaseId = userWorkAddress.DatabaseId;
                                userDetails.PcrId = userWorkAddress.PcrId;
                               // userDetails.JobTitleID = userWorkAddress.JobTitleID;

                                userRepository.ModifiedUser(userDetails);             //To update the user details.

                                if (userWorkAddress.LinkedPcrUsers != null)
                                {
                                    List<LinkedPCRUsers> listLinkedPcrUsers = new List<LinkedPCRUsers>();
                                    foreach (var item in userWorkAddress.LinkedPcrUsers)
                                    {
                                        LinkedPCRUsers pcrUser = new LinkedPCRUsers();
                                        pcrUser.UserName = item.UserName;
                                        pcrUser.PcrRecordId = item.PcrRecordId;
                                        pcrUser.PcrDatabaseId = item.PcrDatabaseId;
                                        pcrUser.IsPcrUser = true;
                                        pcrUser.CreatedBy = userWorkAddress.CreatedBy;
                                        pcrUser.CreatedDate = DateTime.Now;
                                        pcrUser.UserID = userId;
                                        pcrUser.UpdatedBy = userWorkAddress.UpdatedBy;
                                        pcrUser.UpdatedDate = DateTime.Now;
                                        listLinkedPcrUsers.Add(pcrUser);
                                    }
                                    if (listLinkedPcrUsers != null)
                                        userRepository.AddPcrLinkedUser(listLinkedPcrUsers, userId);     // To add the linked pcr user details in DB.
                                }

                                workAddress.WorkAddressID = workAddress.WorkAddressID;
                                workAddress.UserID = userId;
                                workAddress.Address1 = userWorkAddress.Address1;
                                workAddress.Address2 = userWorkAddress.Address2;
                                workAddress.CityId = userWorkAddress.CityId;
                                workAddress.StateId = userWorkAddress.StateId;
                                workAddress.WorkPhone = userWorkAddress.WorkPhone;
                                workAddress.Zip = userWorkAddress.Zip;
                                workAddress.RoleID = (int)userWorkAddress.RoleID;
                                workAddress.LocationID = userWorkAddress.LocationID;
                                workAddressRepository.UpdateWorkAddressDetails(workAddress);      // To update the user work address details.
                                
                                using (userRoleRepository = new UserRoleRepository(session.DatabaseId()))
                                {
                                    var userRole = userRoleRepository.GetUserRoleIDDetailsByUserId(userId);      // To get the user role details.

                                    userRole.UpdatedBy= userWorkAddress.UpdatedBy;
                                    userRole.UpdatedDate = DateTime.Now;
                                    userRole.RoleID = userWorkAddress.RoleID;
                                    
                                    userRoleRepository.ModifiedUserRole(userRole);      // To update the user role details. 
                                    isUpdateUser = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    userRepository = new UserRepository();
                    workAddressRepository = new WorkAddressRepository();
                    
                    using (var userRepo = userRepository)
                    {
                        var userDetails = userRepository.GetUserIDDetails(userId);     // To get the user details.

                        using (var workAddressRepo = workAddressRepository)
                        {
                            var workAddress = workAddressRepository.GetWorkAddressDetailsByID(userId);      // To get the user work address details.

                            if (userDetails == null && workAddress == null)
                                return false;
                            else
                            {
                                userDetails.EmailConfirmed = true;
                                userDetails.UpdatedBy = userWorkAddress.UpdatedBy;
                                userDetails.FirstName = userWorkAddress.FirstName;
                                userDetails.IsDelete = userDetails.IsDelete;
                                userDetails.LastName = userWorkAddress.LastName;
                                userDetails.LockOutEnabled = userDetails.LockOutEnabled;
                                userDetails.MiddleName = userWorkAddress.MiddleName;
                                userDetails.PhoneNumber = userWorkAddress.PhoneNumber;
                                userDetails.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                                userDetails.DOB = userWorkAddress.DOB;
                                userDetails.OtherName = userWorkAddress.OtherName;
                                userDetails.Gender = userWorkAddress.Gender;
                                userDetails.Mobile = userWorkAddress.Mobile;
                                userDetails.ReportingAuthorityEmail = userWorkAddress.ReportingAuthorityEmail;
                                userDetails.ReportingAuthorityName = userWorkAddress.ReportingAuthorityName;
                                userDetails.ReportingAuthorityPhone = userWorkAddress.ReportingAuthorityPhone;
                                userDetails.CreatedDate = userDetails.CreatedDate;
                                userDetails.UpdatedDate = DateTime.Now;
                                userDetails.UserID = userId;
                                userDetails.UserName = userDetails.UserName;
                                userDetails.EmailAddress = userDetails.EmailAddress;
                                userDetails.Password = userDetails.Password;
                                userDetails.DatabaseId = userWorkAddress.DatabaseId;
                                userDetails.PcrId = userWorkAddress.PcrId;
                                userRepository.ModifiedUser(userDetails);             //To update the user details.

                                workAddress.WorkAddressID = workAddress.WorkAddressID;
                                workAddress.UserID = userId;
                                workAddress.Address1 = userWorkAddress.Address1;
                                workAddress.Address2 = userWorkAddress.Address2;
                                workAddress.CityId = userWorkAddress.CityId;
                                workAddress.StateId = userWorkAddress.StateId;
                                workAddress.WorkPhone = userWorkAddress.WorkPhone;
                                workAddress.Zip = userWorkAddress.Zip;
                                workAddress.RoleID = (int)userWorkAddress.RoleID;
                                workAddress.LocationID = userWorkAddress.LocationID;
                                workAddressRepository.UpdateWorkAddressDetails(workAddress);      // To update the user work address details.

                                using (userRoleRepository = new UserRoleRepository())
                                {
                                    var userRole = userRoleRepository.GetUserRoleIDDetailsByUserId(userId);      // To get the user role details.

                                    userRole.UpdatedBy = userWorkAddress.UpdatedBy;
                                    userRole.UpdatedDate = DateTime.Now;
                                    userRole.RoleID = userWorkAddress.RoleID;

                                    userRoleRepository.ModifiedUserRole(userRole);      // To update the user role details. 
                                    isUpdateUser = true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return isUpdateUser;
        }

        /// <summary>
        /// To delete the user details.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeleteUser(int userId, string accessToken)
        {
            string msg = string.Empty;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        User user = userRepository.GetUserIDDetails(userId);
                        if (user != null)
                        {
                            if (user.IsDelete == true)
                                return "User already deleted.";

                            user.IsDelete = true;
                            userRepository.ModifiedUser(user);
                            msg = "User has been deleted successfully.";
                        }
                        else
                            throw new Exception("Content not found by Id =" + userId);
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
        /// To exist the user name in DB or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ExistUserNameWithoutUserId(string userName, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.ExistUserNameWithouUserId(userName);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        return userRepository.ExistUserNameWithouUserId(userName);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To exist the name in DB or not based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ExistUserNameWithUserID(int userId, string userName, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.ExistUserNameWithUserID(userId, userName);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        return userRepository.ExistUserNameWithUserID(userId, userName);
                    }
                }
            }
            catch
            {
                throw;
            }            
        }

        /// <summary>
        /// To get the user details based on Username and userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public UserWorkAddress GetUserDetailsByUserNameWithUserID(int userId, string userName, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.GetUserDetailsByUserNameWithUserID(userId, userName);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        return userRepository.GetUserDetailsByUserNameWithUserID(userId, userName);
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// To change the user password .
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="oldPassWord"></param>
        /// <param name="newPassword"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ChangePassword(int Id, string oldPassWord, string newPassword, string accessToken)
        {  
            oldPassWord = Encrypt(oldPassWord);     // To encrypt the password using MD5 algorithm.
            newPassword = Encrypt(newPassword);     // To encrypt the password using MD5 algorithm.
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        User user = userRepository.GetUserIDDetails(Id);
                        if (user != null)
                        {
                            if (user.Password == oldPassWord)
                            {
                                user.Password = newPassword;
                                userRepository.UpdatedUserPassWords(user);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            throw new Exception("Content not found by Id =" + Id);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        User user = userRepository.GetUserIDDetails(Id);
                        if (user != null)
                        {
                            if (user.Password == oldPassWord)
                            {
                                user.Password = newPassword;
                                userRepository.UpdatedUserPassWords(user);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            throw new Exception("Content not found by Id =" + Id);
                    }
                }
            }
            catch
            {
                throw;
            }           
        }

        /// <summary>
        /// To reset the user password.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="passWord"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ResetPassword(int Id, string passWord, string databaseId, string pcrId)
        {
            passWord = Encrypt(passWord);   // To encrypt the password using MD5 algorithm.
            try
            {
                if (!string.IsNullOrEmpty(databaseId) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(databaseId))
                    {
                        User user = userRepository.GetUserIDDetails(Id);
                        if (user != null)
                        {
                            user.Password = passWord;
                            user.IsFirstLogin = false;
                            userRepository.UpdatedUserPassWords(user);
                            return true;
                        }
                        else
                            return false;
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        User user = userRepository.GetUserIDDetails(Id);
                        if (user != null)
                        {
                            user.Password = passWord;
                            userRepository.UpdatedUserPassWords(user);
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To validate the user name in DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="databaseId"></param>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        public User ValidUserName(string userName, string databaseId, string pcrId)
        { 
            User userDetails = null;
            try
            {
                if (!string.IsNullOrEmpty(databaseId) || !string.IsNullOrEmpty(pcrId) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(databaseId,pcrId))
                    {
                        userDetails = userRepository.ValidUserName(userName);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        userDetails = userRepository.ValidUserName(userName);
                    }
                }
            }
            catch
            {
                throw;
            }
            return userDetails;
        }

        /// <summary>
        /// To validate the user name in DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public User ValidUserName(string userName, string accessToken)
        {
            User userDetails = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        userDetails = userRepository.ValidUserName(userName);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        userDetails = userRepository.ValidUserName(userName);
                    }
                }
            }
            catch
            {
                throw;
            }
            return userDetails;
        }

        /// <summary>
        /// To get the all user names.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<UserWorkAddress> GetAllUserNames(string accessToken)
        {
            IList<UserWorkAddress> lstUsers = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        lstUsers = userRepository.GetAllUserNames();
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
            return lstUsers;
        }

        /// <summary>
        /// To get the all user names.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IList<UserWorkAddress> GetUserNames(string accessToken)
        {
            IList<UserWorkAddress> lstUsers = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        lstUsers = userRepository.GetUserNames();
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
            return lstUsers;
        }

        /// <summary>
        /// To lock out the user details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string LockOutUser(int id, string accessToken)
        {
            string msg = string.Empty;
            try
            {   
                var session = _sessionManager.GetSessionValues(accessToken);
                
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        var userIdDetails = userRepository.GetUserIDDetails(id);
                        if (userIdDetails != null)
                        {
                            userIdDetails.UpdatedDate = DateTime.Now;
                            if (userIdDetails.LockOutEnabled == false)
                            {
                                userIdDetails.LockOutEnabled = true;
                                userRepository.ModifiedUser(userIdDetails);
                                msg = "User has been lockout successfully.";
                            }
                            else
                            {
                                userIdDetails.LockOutEnabled = false;
                                userRepository.ModifiedUser(userIdDetails);
                                msg = "User has been lockout enable successfully.";
                            }
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

        /// <summary>
        /// To get the user login details.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        public UserViewModel GetUserLoginDetails(string userName, string password, string databaseId, string pcrId, ref string userException)
        {            
            UserViewModel userdetails = null;
            password = Encrypt(password);   // To encrypt the password using MD5 algorithm.

            try
            {
                User user = null;
                //if (_isNonPCR)
                //{
                //    using (var repository =new UserRepository())
                //    {
                //        user = repository.ValidUser(userName, password);
                //        if (user != null)
                //        {
                //            userdetails = repository.GetUserLoginDetails(userName, password);
                //            userException = "This user is deactivated.";
                //            return userdetails;
                //        }
                //    }
                //}
                //else
                //{
                    using (var repository = new UserRepository())
                    {
                        user = repository.ValidUser(userName, password);
                        if (user != null)
                        {
                            userdetails = repository.GetUserLoginDetails(userName, password);
                            userException = "This user is deactivated.";
                            return userdetails;
                        }
                    }
                    if ((!string.IsNullOrEmpty(databaseId)) || (!string.IsNullOrEmpty(pcrId)) && user == null)
                    {
                        using (var repository = new UserRepository(databaseId,pcrId))
                        {
                            user = repository.ValidUser( userName, password);
                            if (user != null)
                            {
                                userdetails = repository.GetUserLoginDetails(userName, password);
                                userException = "This user is deactivated.";
                                return userdetails;
                            }
                        }
                    }
               // }

            }
            catch(Exception ex)
            {
                throw;
            }
            return null;
        }

        /// <summary>
        /// To check the password of the user in DB based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool DoesMatchUserPassword(int userId, string password, string accessToken)
        {
            password = Encrypt(password);   // To encrypt the password using MD5 algorithm.
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        return repository.DoesMatchUserPassword(userId, password);
                    }
                }
                else
                {
                    using (var repository = new UserRepository())
                    {
                        return repository.DoesMatchUserPassword(userId, password);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the single sign-on from PCR user.
        /// </summary>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        public UserWorkAddress GetPCRLoginDetails(string pcrId)
        {
            try
            {
                UserWorkAddress userWorkAddress = new UserWorkAddress();
                if (!string.IsNullOrEmpty(pcrId))
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    WebClient client1 = new WebClient();
                    client1.Headers.Add("Authorization", "Bearer " + pcrId);
                    var response = client1.DownloadString(ConfigurationManager.AppSettings["ClientAPI"] + "/uiapi/users/me/validate");
                                      
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var pcrUser = jss.Deserialize<Dictionary<string, string>>(response);
                    if (!String.IsNullOrEmpty(pcrUser["UserName"]) && !String.IsNullOrEmpty(pcrUser["RecordId"]))
                    {
                        if (Convert.ToBoolean(pcrUser["ActiveUser"]) == true)
                        {
                            if (!string.IsNullOrEmpty(pcrUser["DatabaseId"]) || !string.IsNullOrEmpty(pcrId))
                            {
                                using (var userRepository = new UserRepository(pcrUser["DatabaseId"], pcrId))
                                {
                                    userWorkAddress.UserName = pcrUser["UserName"];
                                    userWorkAddress.EmailAddress = pcrUser["UserName"];
                                    userWorkAddress.PhoneNumber = pcrUser["Phone"];
                                    userWorkAddress.DatabaseId = pcrUser["DatabaseId"];
                                    userWorkAddress.PcrId = pcrId;

                                    //Getting user details from GetUserDetails method                     
                                    var userDetails = userRepository.GetUserDetails(pcrUser["UserName"]);
                                    if (userDetails != null)
                                    {
                                        userWorkAddress.AccessFailedCount = userDetails.AccessFailedCount;
                                        userWorkAddress.Address1 = userDetails.Address1;
                                        userWorkAddress.Address2 = userDetails.Address2;
                                        userWorkAddress.CityId = userDetails.CityId;
                                        userWorkAddress.CreatedBy = userDetails.CreatedBy;
                                        userWorkAddress.CreatedDate = userDetails.CreatedDate;
                                        userWorkAddress.DOB = userDetails.DOB;
                                        userWorkAddress.EmailAddress = userDetails.EmailAddress;
                                        userWorkAddress.EmailConfirmed = userDetails.EmailConfirmed;
                                        userWorkAddress.FirstName = userDetails.FirstName;
                                        userWorkAddress.Gender = userDetails.Gender;
                                        userWorkAddress.IsDelete = userDetails.IsDelete;
                                        userWorkAddress.LastName = userDetails.LastName;
                                        userWorkAddress.LocationID = userDetails.LocationID;
                                        userWorkAddress.LockOutEnabled = userDetails.LockOutEnabled;
                                        userWorkAddress.MiddleName = userDetails.MiddleName;
                                        userWorkAddress.Mobile = userDetails.Mobile;
                                        userWorkAddress.OtherName = userDetails.OtherName;
                                        userWorkAddress.Password = userDetails.Password;
                                        userWorkAddress.PhoneNumber = userDetails.PhoneNumber;
                                        userWorkAddress.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                                        userWorkAddress.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                                        userWorkAddress.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                                        userWorkAddress.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;
                                        userWorkAddress.RoleID = userDetails.RoleID;
                                        userWorkAddress.StateId = userDetails.StateId;
                                        userWorkAddress.UpdatedBy = userDetails.UpdatedBy;
                                        userWorkAddress.UpdatedDate = userDetails.UpdatedDate;
                                        userWorkAddress.UserID = userDetails.UserID;
                                        userWorkAddress.UserName = userDetails.UserName;
                                        userWorkAddress.WorkAddressID = userDetails.WorkAddressID;
                                        userWorkAddress.WorkPhone = userDetails.WorkPhone;
                                        userWorkAddress.Zip = userDetails.Zip;
                                    }
                                    else
                                    {
                                        userWorkAddress.RoleID = 1;
                                        userWorkAddress.LastName = pcrUser["FullName"];
                                    }
                                }
                            }
                        }
                    }
                }
                return userWorkAddress;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To encrypt the user password Using MD5 algorithm.
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string Encrypt(string pwd)
        {
            string passphrase = ConfigurationManager.AppSettings["passphrase"].ToString();
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider 
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(pwd);
            //to convert the string to utf encoding binary 

            try
            {
                //To transform the utf binary code to md5 encrypt    
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }
            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);
        }


      

        /// <summary>
        /// To get the user details from PCR login.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
     

        /// <summary>
        /// To check the recordId in DB.
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        protected User UserExistsInOnboardingSystem(long recordId, string databaseId)
        {
            try
            {
                using (var userRepository = new UserRepository(databaseId))
                {
                    return userRepository.ValidateUserId(recordId);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// To check the PCRRecord id in DB based on userId.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ExistPcrRecordIdWithUserID(long pcrRecordId,int userId,string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.ExistPcrRecordIdWithUserID(pcrRecordId,userId);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        return userRepository.ExistPcrRecordIdWithUserID(pcrRecordId,userId);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To check the PCrRecordId in DB.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ExistPcrRecordId(long pcrRecordId,  string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.ExistPcrRecordIdWithOutUserID(pcrRecordId);
                    }
                }
                else
                {
                    using (var userRepository = new UserRepository())
                    {
                        return userRepository.ExistPcrRecordIdWithOutUserID(pcrRecordId);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To add the list of users at a time.
        /// </summary>
        /// <param name="userWorkAddress"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool AddListOfUsers(UserWorkAddress userWorkAddress, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var roleRepository = new RoleRepository(session.DatabaseId()))
                    {
                        int roleId = Convert.ToInt32(userWorkAddress.RoleID);
                        var role = roleRepository.GetRoleIDDetails(roleId);   //To checking the roleId details from DB if it's exist or not.
                        if (role != null || roleId != 1)
                        {
                            using (var userRepository = new UserRepository(session.DatabaseId()))
                            {
                                int existUserNameCount = userRepository.ExistUserName(userWorkAddress.UserName);
                                if (existUserNameCount == 0)
                                {
                                    User user = new User();
                                    user.UserName = userWorkAddress.UserName;
                                    user.Password = Encrypt(userWorkAddress.Password);     // To encrypt the password using MD5 algorithm.
                                    user.AccessFailedCount = 0;
                                    user.EmailAddress = userWorkAddress.UserName;
                                    user.UserName = userWorkAddress.UserName;
                                    user.CreatedBy = userWorkAddress.CreatedBy;
                                    user.EmailConfirmed = true;
                                    user.FirstName = userWorkAddress.FirstName;
                                    user.IsDelete = false;
                                    user.LastName = userWorkAddress.LastName;
                                    user.LockOutEnabled = false;
                                    user.MiddleName = userWorkAddress.MiddleName;
                                    user.PhoneNumber = userWorkAddress.PhoneNumber;
                                    user.PhoneNumberConfirmed = true;
                                    user.DOB = userWorkAddress.DOB;
                                    user.OtherName = userWorkAddress.OtherName;
                                    user.Gender = userWorkAddress.Gender;
                                    user.Mobile = userWorkAddress.Mobile;
                                    user.CreatedBy = user.CreatedBy;
                                    user.CreatedDate = DateTime.Now;
                                    user.UpdatedBy = null;
                                    user.UpdatedDate = DateTime.Now;
                                    user.ReportingAuthorityEmail = userWorkAddress.ReportingAuthorityEmail;
                                    user.ReportingAuthorityName = userWorkAddress.ReportingAuthorityName;
                                    user.ReportingAuthorityPhone = userWorkAddress.ReportingAuthorityPhone;
                                    user.DatabaseId = userWorkAddress.DatabaseId;
                                    user.PcrId = userWorkAddress.PcrId;

                                    //To assigning the pcr user to onboarding
                                    user.IsPcrUser = userWorkAddress.IsPcrUser;
                                    user.PcrDatabaseId = userWorkAddress.PcrDatabaseId;
                                    user.PcrRecordId = userWorkAddress.PcrRecordId;

                                    int userId = userRepository.AddUser(user);     // To add the user details in DB.

                                    using (var workAddressRepository = new WorkAddressRepository(session.DatabaseId()))
                                    {
                                        WorkAddress workAddressDetails = new WorkAddress();
                                        workAddressDetails.UserID = userId;
                                        workAddressDetails.Address1 = userWorkAddress.Address1;
                                        workAddressDetails.Address2 = userWorkAddress.Address2;
                                        workAddressDetails.CityId = userWorkAddress.CityId;
                                        workAddressDetails.StateId = userWorkAddress.StateId;
                                        workAddressDetails.WorkPhone = userWorkAddress.WorkPhone;
                                        workAddressDetails.Zip = userWorkAddress.Zip;
                                        workAddressDetails.RoleID = roleId;
                                        workAddressDetails.LocationID = userWorkAddress.LocationID;
                                        workAddressRepository.AddWorkAddressDetails(workAddressDetails);     //To add the user work address details.
                                    }

                                    using (var userRoleRepository = new UserRoleRepository(session.DatabaseId()))
                                    {
                                        UserRole userRole = new UserRole()
                                        {
                                            RoleID = userWorkAddress.RoleID,
                                            UserID = userId,
                                            CreatedBy = userWorkAddress.CreatedBy,
                                            CreatedDate = DateTime.Now,
                                            UpdatedBy = userWorkAddress.UpdatedBy,
                                            UpdatedDate = DateTime.Now,
                                            DatabaseId = userWorkAddress.DatabaseId,
                                            PcrId = userWorkAddress.PcrId
                                        };
                                        userRoleRepository.AddUserRole(userRole);      // To add the user role details. 
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    return false;
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
        /// To update the user onboarding status. which is help for Onboarding process.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateUserOnboardStatus(int userId, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.UpdateUserOnboardStatus(userId);
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To update the user onboarding status. which is help for Onboarding process.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool UpdateManagerApprovalStatus(int userId, string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.UpdateManagerApprovalStatus(userId);
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public DashboardDetails GetOnboardDashboardDetails(string userName, int roleId,string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR == true)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.GetOnboardDashboardDetails(userName,roleId);
                    }
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public List<UserMonthWiseCount> GetMothWiseOnboardDetails(int type,string userName, int roleId,string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);  
                              
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR == true)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                       return  userRepository.GetMothWiseOnboardDetails(type, userName,roleId);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return null;
        }

        public IList<Model.ViewModels.UserWorkAddress> GetUsersForDashboard(int roleID,string userName, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetUsersForDashboard(roleID,userName, session.DatabaseId());
                        return lstUsers;
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

        public IList<Model.ViewModels.UserWorkAddress> GetOnboardingCompletedEmployees(DateTime? fromDate, DateTime? toDate, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetOnboardingCompletedEmployees(fromDate,toDate,session.DatabaseId());
                        return lstUsers;
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

        public IList<Model.ViewModels.UserWorkAddress> GetOnboardingInProgressEmployees(DateTime? fromDate, DateTime? toDate, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetOnboardingInProgressEmployees(fromDate, toDate, session.DatabaseId());
                        return lstUsers;
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

        public IList<Model.ViewModels.UserWorkAddress> GetPre_OnboardingEmployees(DateTime? fromDate, DateTime? toDate, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetPre_OnboardingEmployees(fromDate, toDate, session.DatabaseId());
                        return lstUsers;
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

        public IList<Model.ViewModels.UserWorkAddress> GetPendingOnboardingEmployees(DateTime? fromDate, DateTime? toDate, string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetPendingOnboardingEmployees(fromDate, toDate, session.DatabaseId());
                        return lstUsers;
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
       
        public List<Model.ViewModels.GetPendingOnboardUsers> GetPendingOnboardingUsersCount(bool isSuperAdmin, string accessToken)
        {
            try
            {
                int roleID = 1;
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    if (!isSuperAdmin)
                    {
                        using (var userRoleRepo = new UserRoleRepository(session.DatabaseId()))
                        {
                            roleID = userRoleRepo.GetUserRoleID(Convert.ToInt32(session.UserRecordId()));
                        }
                    }
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsersCount = repository.GetPendingOnboardingUsersCount(roleID, session.DatabaseId());                        
                        return lstUsersCount;
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



        public IList<User> GetSuperAdminDetails(string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetSuperAminDetails();
                        return lstUsers;
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

        public IList<User> GetSuperAdminDetailsServices(string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetSuperAminDetailsServices();
                        return lstUsers;
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


        public IList<UserWorkAddress> GetClientAdminDetails(string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetClientAminDetails();
                        return lstUsers;
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

        public IList<UserWorkAddress> GetClientAdminDetailsServices(string accessToken)
        {
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetClientAminDetailsServices();
                        return lstUsers;
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
        public UserWorkAddress GetUserByMailId(string MailId, string accessToken)
        {
            UserWorkAddress userWorkAddrs = new UserWorkAddress();
            User userDetails = null;
            WorkAddress workAddressDetails = null;
            UserRepository userRepository = null;
            WorkAddressRepository workAddressRepository = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    userRepository = new UserRepository(session.DatabaseId());
                    workAddressRepository = new WorkAddressRepository(session.DatabaseId());

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetMailIdDetails(MailId);
                    if (userDetails != null)
                    {
                        using (var wrkAdrsRepository = workAddressRepository)
                            workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailId(userDetails.UserID);
                    }
                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;
                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                    }
                }
                else
                {
                    userRepository = new UserRepository();
                    workAddressRepository = new WorkAddressRepository();

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetMailIdDetails(MailId);

                    using (var wrkAdrsRepository = workAddressRepository)
                        workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailId(userDetails.UserID);

                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;

                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                    }
                }
                return userWorkAddrs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string AddImage(User uploadDocuments, string accessToken)
        {
            string msg = string.Empty;

            
            try
            {
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        repository.UpdateImage(uploadDocuments);
                    }
                    msg = "Image uploaded successfully";
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

        public UserWorkAddress GetUserByMailIdServices(string MailId, string accessToken)
        {
            UserWorkAddress userWorkAddrs = new UserWorkAddress();
            User userDetails = null;
            WorkAddress workAddressDetails = null;
            UserRepository userRepository = null;
            WorkAddressRepository workAddressRepository = null;
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);
                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    userRepository = new UserRepository(session.DatabaseId());
                    workAddressRepository = new WorkAddressRepository(session.DatabaseId());

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetMailIdDetails(MailId);

                    using (var wrkAdrsRepository = workAddressRepository)
                        workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailIdServices(userDetails.UserID);

                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;
                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                    }
                }
                else
                {
                    userRepository = new UserRepository();
                    workAddressRepository = new WorkAddressRepository();

                    using (var userRepo = userRepository)
                        userDetails = userRepo.GetMailIdDetails(MailId);

                    using (var wrkAdrsRepository = workAddressRepository)
                        workAddressDetails = wrkAdrsRepository.GetWorkAddressDetailsByMailIdServices(userDetails.UserID);

                    if (userDetails == null && workAddressDetails == null)
                        return null;
                    else
                    {
                        userWorkAddrs.AccessFailedCount = userDetails.AccessFailedCount;
                        userWorkAddrs.Address2 = workAddressDetails.Address2;
                        userWorkAddrs.CityId = workAddressDetails.CityId;
                        userWorkAddrs.CreatedBy = userDetails.CreatedBy;
                        userWorkAddrs.CreatedDate = userDetails.CreatedDate;
                        userWorkAddrs.DOB = userDetails.DOB;
                        userWorkAddrs.EmailAddress = userDetails.EmailAddress;
                        userWorkAddrs.EmailConfirmed = userDetails.EmailConfirmed;
                        userWorkAddrs.FirstName = userDetails.FirstName;
                        userWorkAddrs.Gender = userDetails.Gender;
                        userWorkAddrs.IsDelete = userDetails.IsDelete;
                        userWorkAddrs.LastName = userDetails.LastName;
                        userWorkAddrs.LocationID = workAddressDetails.LocationID;
                        userWorkAddrs.LockOutEnabled = userDetails.LockOutEnabled;
                        userWorkAddrs.MiddleName = userDetails.MiddleName;
                        userWorkAddrs.Mobile = userDetails.Mobile;
                        userWorkAddrs.OtherName = userDetails.OtherName;
                        userWorkAddrs.PhoneNumber = userDetails.PhoneNumber;
                        userWorkAddrs.PhoneNumberConfirmed = userDetails.PhoneNumberConfirmed;
                        userWorkAddrs.RoleID = workAddressDetails.RoleID;
                        userWorkAddrs.StateId = workAddressDetails.StateId;
                        userWorkAddrs.UpdatedBy = userDetails.UpdatedBy;
                        userWorkAddrs.UpdatedDate = userDetails.UpdatedDate;
                        userWorkAddrs.UserID = userDetails.UserID;
                        userWorkAddrs.WorkAddressID = workAddressDetails.WorkAddressID;
                        userWorkAddrs.WorkPhone = workAddressDetails.WorkPhone;
                        userWorkAddrs.Zip = workAddressDetails.Zip;
                        userWorkAddrs.Address1 = workAddressDetails.Address1;
                        userWorkAddrs.UserName = userDetails.UserName;
                        userWorkAddrs.Password = userDetails.Password;
                        userWorkAddrs.ReportingAuthorityEmail = userDetails.ReportingAuthorityEmail;
                        userWorkAddrs.ReportingAuthorityName = userDetails.ReportingAuthorityName;
                        userWorkAddrs.ReportingAuthorityPhone = userDetails.ReportingAuthorityPhone;

                        userWorkAddrs.IsPcrUser = userDetails.IsPcrUser;
                        userWorkAddrs.PCRUserName = userDetails.PCRUserName;
                        userWorkAddrs.PcrRecordId = userDetails.PcrRecordId;
                        userWorkAddrs.PcrDatabaseId = userDetails.PcrDatabaseId;
                        userWorkAddrs.LinkedPcrUsers = userDetails.LinkedPcrUsers;
                    }
                }
                return userWorkAddrs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NotificationTemplete GetTemplteForSendingCredentials(int typeId,string accessToken)
        {
            try
            {
                var session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR == true)
                {
                    using (var userRepository = new UserRepository(session.DatabaseId()))
                    {
                        return userRepository.GetTemplteForSendingCredentials(typeId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public IList<Model.ViewModels.EverifyEmloyees> GetEverifyEmployees(string userName, string accessToken)
        {
            try
            {
                List<EverifyEmloyees> lstEmployees = new List<EverifyEmloyees>();
                dynamic session = null;
                if (!string.IsNullOrEmpty(accessToken))
                    session = _sessionManager.GetSessionValues(accessToken);

                if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR)
                {
                    using (var repository = new UserRepository(session.DatabaseId()))
                    {
                        var lstUsers = repository.GetEverifyEmployees(userName);
                                              
                        if (lstUsers != null)
                        {
                            foreach (var item in lstUsers)
                            {
                                var sSNCode = item.SSN;

                                var decrypt = Utility.DecryptString(sSNCode);
                                if (decrypt != null && decrypt != "")
                                {
                                    string hiddenString = decrypt.Substring(decrypt.Length - 4).PadLeft(decrypt.Length, '*');

                                    item.SSN = hiddenString;                                    
                                }
                                //else
                                //{
                                //    item.SSN = hiddenString;
                                //}
                                lstEmployees.Add(item);
                            }                            
                        }
                    }
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
                return lstEmployees;
            }
            catch
            {
                throw;
            }
        }

        public List<UserWorkAddress> GetAllEmployeesUploadDocPackages(int docPackageId, string accessToken)
        {
            var session = _sessionManager.GetSessionValues(accessToken);
            List<UserWorkAddress> i9UploadDocument = null;
            if (!string.IsNullOrEmpty(session.DatabaseId()) || _isNonPCR == true)
            {
                using (var userRepository = new UserRepository(session.DatabaseId()))
                {
                    i9UploadDocument = userRepository.GetAllEmployeesUploadDocPackages(docPackageId);
                }
            }
            return i9UploadDocument;
        }

        public SessionDetails ValidateUserDetails(string UrlString)
        {
            try
            {
                string DecryptedString = Utility.DecryptString(UrlString);
                NameValueCollection queryparameters = HttpUtility.ParseQueryString(DecryptedString);

                string DatabaseId = queryparameters["databaseId"].ToString();

                string UserName = queryparameters["UserName"].ToString();

                string SendDate = queryparameters["SendDate"].ToString();


                if (!string.IsNullOrEmpty(SendDate))
                {
                    try
                    {
                        DateTime ExpiredDate = Convert.ToDateTime(SendDate);

                        if (ExpiredDate.AddDays(7) > DateTime.Now)
                        {
                            using (var repository = new UserRepository(DatabaseId))
                            {
                                SessionDetails Details = repository.GetUserInfo(UserName);

                                if(Details != null)
                                        Details.databaseId = DatabaseId;

                                return Details;
                            }
                        }
                        else
                        {
                            throw new Exception("URL has Expired");
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new Exception("Invalid URL");
                }

               
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid URL or Expired");
            }
        }

    }
}
