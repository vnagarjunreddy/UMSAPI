using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PCR.Users.API.Helpers;
using PCR.Users.API.ViewModels;
using PCR.Users.Data;
using PCR.Users.Model.ViewModels;
using PCR.Users.Models;
using PCR.Users.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

namespace PCR.Users.API.Controllers
{
    
    public class UserController : ApiController
    {
        private UserService _service;
        private UserRoleService _serviceUserRole;
        private RoleService _serviceRole;
        
        public UserController()
        {
            _serviceRole = new RoleService();
            _serviceUserRole = new UserRoleService();     
            _service = new UserService();
        }

        /// <summary>
        /// To get the all user details.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUsers")]
        [JwtAuthentication]
        public IHttpActionResult GetUsers(int roleId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;                   //Getting from url header authorization

                var lstUsers = _service.GetUsers(roleId, accessToken);

                    if (lstUsers != null && lstUsers.Count > 0)
                        return Content(HttpStatusCode.OK, lstUsers);
                    else
                        return Content(HttpStatusCode.OK, "Content not found");
                
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUsers : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the all user details.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetManageUsers")]
        [JwtAuthentication]
        public IHttpActionResult GetUsersForUI(int roleId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;                   //Getting from url header authorization

                var lstUsers = _service.GetManageUsers(roleId, accessToken);

                if (lstUsers != null && lstUsers.Count > 0)
                    return Content(HttpStatusCode.OK, lstUsers);
                else
                    return Content(HttpStatusCode.OK, "Content not found");

            }
            catch (Exception ex)
            {
                Logger.WriteException("GetManageUsers : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the user details by id.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserDetailsByID")]
        [JwtAuthentication]       
        public IHttpActionResult GetUserDetailsByID(int userId)
        {
            try
            {
                
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var userDetails = _service.GetUserByID(userId, accessToken);
                if (userDetails != null)
                    return Content(HttpStatusCode.OK, userDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + userId);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserDetailsByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To add the user details.
        /// </summary>
        /// <param name="userWorkAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddUser")]
        //[JwtAuthentication]
        [JwtAuthentication]
        public IHttpActionResult AddUser(Model.ViewModels.UserWorkAddress userWorkAddress)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                if (userWorkAddress != null)
                {
                    if (ModelState.IsValid)
                    {
                        bool isUserSaved =  _service.AddUserDetails(userWorkAddress, accessToken);
                        if (isUserSaved)
                        {
                            SendEmailToNewUser(userWorkAddress, accessToken);  //To send email to user account, getting the user credinitials
                            return Content(HttpStatusCode.OK, "User has been added successfully.");
                        }
                        else
                            return Content(HttpStatusCode.BadRequest, "User name is already exist.");
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                        {
                            modelError += ModelState.Values.ToList()[i].Errors[0].ErrorMessage + ",";
                        }
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the user details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddUser : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To send the user credinitials to the given registered email.
        /// </summary>
        /// <param name="userWorkAddress"></param>
        public void SendEmailToNewUser(Model.ViewModels.UserWorkAddress userWorkAddress, string accessToken)
        {
            try
            {
                //Logger.WriteTrace("SendEmailToNewUser: Enter the function");
                var templte = _service.GetTemplteForSendingCredentials(1, accessToken);
                //Logger.WriteTrace("SendEmailToNewUser: Enter the function template = "+ templte);
                var appURl = ConfigurationManager.AppSettings["ApplicationURL"] + "/Login?";

                var encryptedString = PCR.Users.Services.Helpers.Utility.EncryptString("databaseId="+userWorkAddress.DatabaseId+"&UserName=" + userWorkAddress.UserName+"&SendDate=" + DateTime.Now);

                string Dec = PCR.Users.Services.Helpers.Utility.DecryptString(encryptedString);

                var subject = templte.EmailSubject;
                appURl = appURl+ "q=" + HttpUtility.UrlEncode( encryptedString);
               // Logger.WriteTrace("SendEmailToNewUser: before body creation");

                var body = templte.Template.Replace("@UserName", userWorkAddress.FirstName);
                body = body.Replace("@ApplicationURL", appURl);
                //body = body.Replace("@EmailAddress", userWorkAddress.UserName);
                //body = body.Replace("@Password", userWorkAddress.Password);
                body = body.Replace("@AuthorityName", userWorkAddress.ReportingAuthorityName);
                body = body.Replace("@AuthorityEmail", userWorkAddress.ReportingAuthorityEmail);
                if (!string.IsNullOrEmpty(userWorkAddress.ReportingAuthorityPhone))
                {
                    body = body.Replace("@AuthorityPhone", userWorkAddress.ReportingAuthorityPhone);
                }
                else
                {
                    body = body.Replace("@AuthorityPhone", null);
                    body = body.Replace("Phone:", null);
                }
               // Logger.WriteTrace("SendEmailToNewUser: before body creation");
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserEmail"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                //Logger.WriteTrace("SendEmailToNewUser: after body creation");
                //string reportingManager = string.Empty;
                //if (!string.IsNullOrEmpty(userWorkAddress.ReportingAuthorityName))
                //{
                //    reportingManager += "Name: " + userWorkAddress.ReportingAuthorityName;
                //}
                //if (!string.IsNullOrEmpty(userWorkAddress.ReportingAuthorityName))
                //{
                //    if (!string.IsNullOrEmpty(reportingManager)) { reportingManager += ", "; }
                //    reportingManager += "Email: " + userWorkAddress.ReportingAuthorityEmail;
                //}
                //if (!string.IsNullOrEmpty(userWorkAddress.ReportingAuthorityName))
                //{
                //    if (!string.IsNullOrEmpty(reportingManager)) { reportingManager += ", "; }
                //    reportingManager += "Phone: " + userWorkAddress.ReportingAuthorityPhone;
                //}

                //if (userWorkAddress.RoleID == 4)
                //{
                //    mailMessage.Body = "Dear " + userWorkAddress.FirstName + ", <br/><br/>Your user account with Onboarding application has been created. You can login and start your onboarding process by entering the required details. <br /><br />";
                //    mailMessage.Body += "Application URL :  " + ConfigurationManager.AppSettings["ApplicationURL"] + "/Login?databaseId=" + userWorkAddress.DatabaseId + "  <br />UserName : " + userWorkAddress.UserName + " <br />Password : " + userWorkAddress.Password + "<br /><br />Please contact your reporting manager  " + (reportingManager == "" ? "" : "" + reportingManager + "") + "  for any further support.<br /><br />Thanks & Regards,<br />PCR Onboarding Support Team.";
                //}
                //else if (userWorkAddress.RoleID == 3)
                //{
                //    mailMessage.Body = "Dear " + userWorkAddress.FirstName + ", <br/><br/>Your manager role user account with Onboarding application has been created. You can login and start using the onboarding application. <br /><br />";
                //    mailMessage.Body += "Application URL : " + ConfigurationManager.AppSettings["ApplicationURL"] + "/Login?databaseId=" + userWorkAddress.DatabaseId + "  <br />UserName : " + userWorkAddress.UserName + " <br />Password : " + userWorkAddress.Password + "<br /><br />Please contact support admin  " + (reportingManager == "" ? "" : "" + reportingManager + "") + "  for any further support.<br /><br />Thanks & Regards,<br />PCR Onboarding Support Team.";
                //}
                //else if (userWorkAddress.RoleID == 2)
                //{
                //    mailMessage.Body = "Dear " + userWorkAddress.FirstName + ", <br/><br/>Your client admin role user account with Onboarding application has been created. You can login and start using the onboarding application. <br /><br />";
                //    mailMessage.Body += "Application URL : " + ConfigurationManager.AppSettings["ApplicationURL"] + "/Login?databaseId=" + userWorkAddress.DatabaseId + "  <br />UserName : " + userWorkAddress.UserName + " <br />Password : " + userWorkAddress.Password + "<br /><br />Please contact support admin  " + (reportingManager == "" ? "" : "" + reportingManager + "") + "  for any further support.<br /><br />Thanks & Regards,<br />PCR Onboarding Support Team.";
                //}
                // mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(userWorkAddress.UserName));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                smtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
              //  Logger.WriteTrace("SendEmailToNewUser: before sending mail");
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserEmail"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
              //  Logger.WriteTrace("SendEmailToNewUser: after sending mail");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To update the user details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userWorkAddress"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "PUT")]
        [ActionName("UpdateUser")]
        [ResponseType(typeof(void))]
        [JwtAuthentication]
        public IHttpActionResult UpdateUser(int id, Model.ViewModels.UserViewModel userWorkAddress)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                if (userWorkAddress != null)
                {
                    if (ModelState.IsValid)
                    {
                        bool isUserUpdated = _service.UpdateUser(id, userWorkAddress,accessToken);
                        if (isUserUpdated)
                            return Content(HttpStatusCode.OK, "User has been updated successfully.");
                        else
                            return Content(HttpStatusCode.BadRequest, "Content not found by Id =" + id);
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                        {
                            modelError = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                        }
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the update user details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateUser : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To delete the user details by id(soft delete).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteUser")]
        [ResponseType(typeof(User))]
        [JwtAuthentication]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.DeleteUser(id, accessToken);
                return Content(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("DeleteUser : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To validate the user name in DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ToValidUserName")]
        [JwtAuthentication]
        public IHttpActionResult ToValidUserName(User user)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var validUserName = _service.ValidUserName(user.UserName,accessToken);
                if (validUserName != null)
                    return Content(HttpStatusCode.OK, true);
                else
                    return Content(HttpStatusCode.OK, false);
            }
            catch (Exception ex)
            {
                Logger.WriteException("ToValidUserName : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To find the user user name in the DB.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ToExistUserName")]
        [JwtAuthentication]
        public IHttpActionResult ToExistUserName(int userId, string userName)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isValidUserName = _service.ExistUserNameWithUserID(userId, userName, accessToken);
                return Content(HttpStatusCode.OK, isValidUserName);
            }
            catch (Exception ex)
            {
                Logger.WriteException("ToExistUserName : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To check user name is exist or not.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ToCheckUserNameExist")]                       // this method using for DMS.
        [JwtAuthentication]
        public IHttpActionResult ToCheckUserNameExist(string userName)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isValidUserName = _service.ExistUserNameWithoutUserId(userName, accessToken);
                return Content(HttpStatusCode.OK, isValidUserName);
            }
            catch (Exception ex)
            {
                Logger.WriteException("ToCheckUserNameExist : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To change the password of the user.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "PUT")]
        [ActionName("ChangePassword")]
        [JwtAuthentication]
        public IHttpActionResult ChangePassword(int id, ChangePassword changePassword)
        {
            try
            {
                if (changePassword != null)
                {
                    if (ModelState.IsValid)
                    {
                        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                        bool isPwdChanged = _service.ChangePassword(id, changePassword.OldPassword, changePassword.NewPassword, accessToken);
                        if (isPwdChanged)
                            return Content(HttpStatusCode.OK, "The password has been changed successfully.");
                        else
                            return Content(HttpStatusCode.OK, "The current password you have entered is incorrect.");
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                        {
                            modelError = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                        }
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the user change password details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("ChangePassword : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To reset the user password.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resetpassword"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "PUT")]
        [ActionName("ResetPassword")]
        public IHttpActionResult ResetPassword(int id, ResetPassword resetpassword)
        {
            try
            {
                if (resetpassword != null)
                {
                    if (ModelState.IsValid)
                    {
                        bool isPwdReset = _service.ResetPassword(id, resetpassword.Password, resetpassword.DatabaseId, resetpassword.pcrId);
                        if (isPwdReset)
                            return Content(HttpStatusCode.OK, "The Password has been reset successfully.");
                        else
                            return Content(HttpStatusCode.OK, "Content not found by Id =" + id);
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                        {
                            modelError = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                        }
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the user reset password details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("ResetPassword : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ValidateUserDetails(string UrlString)
        {
            try
            {
                var Details = _service.ValidateUserDetails(UrlString);

                if (Details != null)
                {
                    return Content(HttpStatusCode.OK, Details);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid URL");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To forget the user password.
        /// </summary>
        /// <param name="forgetpassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ForgetPassword")]
        public IHttpActionResult ForgetPassword(ForgetPassword forgetpassword)
        {
            try
            {
                if (forgetpassword != null)
                {
                    if (ModelState.IsValid)
                    {
                        var user = _service.ValidUserName(forgetpassword.UserName, forgetpassword.DatabaseId, forgetpassword.PcrId);
                        if (user != null)
                        {
                            user.DatabaseId = forgetpassword.DatabaseId;
                            user.PcrId = forgetpassword.PcrId;

                            //To sending email to the registered email address of user.
                            SendEmailToForgetPasswordUser(user);
                            return Content(HttpStatusCode.OK, "An email sent to '" + forgetpassword.UserName + "'. Please follow the specified instructions to recover the password.");
                        }
                        else
                            return Content(HttpStatusCode.OK, "Email you have entered is not found or invalid.");
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                        {
                            modelError = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                        }
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the user forget password details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("ForgetPassword : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To send the email to user for(forget password link).
        /// </summary>
        /// <param name="user"></param>
        public void SendEmailToForgetPasswordUser(User user)
        {
            try
            {
                // To encoding the userId and databaseId.
                string id = Encode(user.UserID.ToString() + "," + user.DatabaseId);
                string resetPswrdURL = ConfigurationManager.AppSettings["ApplicationURL"] + "/Login/ResetPassword?id=" + id;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserEmail"]);
                mailMessage.Subject = "PCR Account- Password Recovery";
                mailMessage.Body = "Dear " + user.FirstName + ",<br/><br/> You have requested to reset your password. Please click the below link to Reset your password.<br/> " + resetPswrdURL;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(user.UserName));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.UseDefaultCredentials = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserEmail"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                smtp.Credentials = NetworkCred;

                smtp.Send(mailMessage);
            }
            catch
            {
                throw;
            }
        }

        //To use the encoding.
        [NonAction]
        public string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        //To use the decoding.
        [NonAction]
        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }

        /// <summary>
        /// To get all the user names.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllUserNames")]
        [JwtAuthentication]
        public IHttpActionResult GetAllUserNames()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstUsrNames = _service.GetAllUserNames(accessToken);
                if (lstUsrNames.Count > 0)
                    return Content(HttpStatusCode.OK, lstUsrNames);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetAllUserNames : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetUserNames")]
        [JwtAuthentication]
        public IHttpActionResult GetUserNames()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstUsrNames = _service.GetUserNames(accessToken);
                if (lstUsrNames.Count > 0)
                    return Content(HttpStatusCode.OK, lstUsrNames);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserNames : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To lock out the user.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("LockOutUser")]
        [JwtAuthentication]
        public IHttpActionResult LockOutUser(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.LockOutUser(id, accessToken);
                return Content(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("LockOutUser : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To get the user login details.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserLoginDetails")]
        [AllowAnonymous]
        public IHttpActionResult GetUserLoginDetails(string userName, string password, string databaseId=null, string pcrId=null)
        {
            string errorMsg = "Either email address or password is incorrect.";
            try
            {
                var loginDetails = _service.GetUserLoginDetails(userName, password, databaseId, pcrId, ref errorMsg);
                if (loginDetails != null)
                {
                    string accessToken = JwtManager.GenerateToken(loginDetails, databaseId);
                    loginDetails.AccessToken = accessToken;
                    return Content(HttpStatusCode.OK, loginDetails);
                }
                else
                    return Content(HttpStatusCode.BadRequest, errorMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserLoginDetails : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To check the password of the user in DB based on userId.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("DoesMatchUserPassword")]
        [JwtAuthentication]
        public IHttpActionResult DoesMatchUserPassword(int userId, string password)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isUserPassword = _service.DoesMatchUserPassword(userId, password, accessToken);
                return Content(HttpStatusCode.OK, isUserPassword);
            }
            catch (Exception ex)
            {
                Logger.WriteException("DoesMatchUserPassword : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To get the User details by user name and userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ToGetUserDetailsByUserNameAndUserId")]
        [JwtAuthentication]
        public IHttpActionResult ToGetUserDetailsByUserNameAndUserId(int userId, string userName)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var userDetails = _service.GetUserDetailsByUserNameWithUserID(userId, userName, accessToken);
                if (userDetails != null)
                    return Content(HttpStatusCode.OK, userDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("ToGetUserDetailsByUserNameAndUserId : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }


        /// <summary>
        /// To get the single sign-on from PCR user.
        /// </summary>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPCRIdLoginDetails")]
        public IHttpActionResult GetPCRIdLoginDetails(string pcrId)
        {
            try
            {
                var pcrLoginDetails = _service.GetPCRLoginDetails(pcrId);
                //string accessToken = JwtManager.GenerateToken(pcrLoginDetails);
                //pcrLoginDetails.AccessToken = accessToken;
                if (pcrLoginDetails != null)
                    return Content(HttpStatusCode.OK, pcrLoginDetails);
                else
                    return Content(HttpStatusCode.OK, "Invalid credinitials.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetPCRIdLoginDetails : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To get the access token details.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAccessToken")]
        [AllowAnonymous]
        public IHttpActionResult GetAccessToken(string userName, string password, string databaseId)
        {
            string errorMsg = "Invalid credenitials.";
            try
            {
                var loginDetails = _service.GetUserLoginDetails(userName, password, databaseId, null, ref errorMsg);
                if (loginDetails != null)
                {
                    string accessToken = JwtManager.GenerateToken(loginDetails, databaseId);
                    return Content(HttpStatusCode.OK, accessToken);
                }
                else
                    return Content(HttpStatusCode.BadRequest, errorMsg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserLoginDetails : ", ex);
                return Content(HttpStatusCode.BadRequest, "Invalid databaseId or userId");
            }
        }

        [HttpGet]
        [ActionName("GetOAuthToken")]
        [JwtAuthentication]
        public IHttpActionResult GetOAuthToken()
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                CGI4VB.OnboardingSessionManager sessionManager = new CGI4VB.OnboardingSessionManager(accessToken);
                string oAuthToken = sessionManager.OauthToken();
               // string oAuthToken = token.Replace("+", "");
                return Content(HttpStatusCode.OK, oAuthToken);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOAuthToken : ", ex);
                return Content(HttpStatusCode.BadRequest, "Invalid databaseId or userId");
            }
        }
        

        /// <summary>
        /// Get the login details of the PCR user.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[ActionName("GetPCRUserLogin")]
        //[AllowAnonymous]
        //public IHttpActionResult GetPCRUserLogin(string code, string state)
        //{
        //    try
        //    {
        //        var loginDetails = _service.GetPCRLoginDetails(code, state);
        //        if (loginDetails != null)
        //        {
        //            return Content(HttpStatusCode.OK, loginDetails);
        //        }
        //        else
        //            return Content(HttpStatusCode.BadRequest, "Invalid credenitials.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteException("GetUserLoginDetails : ", ex);
        //        return Content(HttpStatusCode.BadRequest, "Invalid databaseId or userId");
        //    }
        //}

        /// <summary>
        /// To add the multiple users at a time
        /// </summary>
        /// <param name="userWorkAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddListOfUsers")]
        [JwtAuthentication]
        public IHttpActionResult AddListOfUsers(List<Model.ViewModels.UserWorkAddress> userWorkAddress)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                if (userWorkAddress != null)
                {
                    foreach (var userDate in userWorkAddress)
                    {
                        if (ModelState.IsValid)
                        {

                            bool isUserSaved = _service.AddListOfUsers(userDate, accessToken);
                            if (isUserSaved)
                            {
                                SendEmailToNewUser(userDate, accessToken);  //To send email to user account, getting the user credinitials                               
                            }
                            //else
                            //    return Content(HttpStatusCode.BadRequest, "User name is already exist.");
                        }
                        else
                        {
                            string modelError = null;
                            for (int i = 0; i < ModelState.Keys.Count; i++)
                            {
                                modelError += ModelState.Values.ToList()[i].Errors[0].ErrorMessage + ",";
                            }
                            return Content(HttpStatusCode.BadRequest, modelError);
                        }
                    }
                    return Content(HttpStatusCode.OK, "User has been added successfully.");
                }
                else
                    throw new Exception("Please provide the user details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddListOfUsers : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To check the PCR RecordId with based on userId is exist or not.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("IsExistPCRRecordIdWithUserID")]
        [JwtAuthentication]
        public IHttpActionResult IsExistPCRRecordIdWithUserID(long pcrRecordId, int userId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isValidUserName = _service.ExistPcrRecordIdWithUserID(pcrRecordId, userId, accessToken);
                return Content(HttpStatusCode.OK, isValidUserName);
            }
            catch (Exception ex)
            {
                Logger.WriteException("IsExistPCRRecordIdWithUserID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To check the PCR RecordId is exist or not.
        /// </summary>
        /// <param name="pcrRecordId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("IsExistPCRRecordIdWithOutUserID")]
        [JwtAuthentication]
        public IHttpActionResult IsExistPCRRecordIdWithOutUserID(long pcrRecordId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isValidUserName = _service.ExistPcrRecordId(pcrRecordId,  accessToken);
                return Content(HttpStatusCode.OK, isValidUserName);
            }
            catch (Exception ex)
            {
                Logger.WriteException("IsExistPCRRecordIdWithOutUserID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To update the user onboard status (it is help for onboarding process.)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "PUT")]
        [ActionName("UpdateUserOnboardStatus")]
        [JwtAuthentication]
        public IHttpActionResult UpdateUserOnboardStatus(int userId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isOnboardStatus = _service.UpdateUserOnboardStatus(userId, accessToken);
                return Content(HttpStatusCode.OK, isOnboardStatus);
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateUserOnboardStatus : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To update the user onboard status (it is help for onboarding process.)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AcceptVerbs("POST", "PUT")]
        [ActionName("UpdateManagerApprovalStatus")]
        [JwtAuthentication]
        public IHttpActionResult UpdateManagerApprovalStatus(int userId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                bool isOnboardStatus = _service.UpdateManagerApprovalStatus(userId, accessToken);
                return Content(HttpStatusCode.OK, isOnboardStatus);
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateManagerApprovalStatus : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }


        [HttpGet]
        [ActionName("GetOnboardDashboardDetails")]
        [JwtAuthentication]
        public IHttpActionResult GetOnboardDashboardDetails(string userName, int roleId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetOnboardDashboardDetails(userName,roleId, accessToken);
                return Content(HttpStatusCode.OK, dashboardDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardDashboardDetails :", ex);

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetMothWiseOnboardDetails")]
        [JwtAuthentication]
        public IHttpActionResult GetMothWiseOnboardDetails(int type, string userName, int roleId)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
               var monthWiseUserCount = _service.GetMothWiseOnboardDetails(type, userName,roleId,accessToken);
                return Content(HttpStatusCode.OK, monthWiseUserCount);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetMothWiseOnboardDetails :", ex);

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetUsersForDashboard")]
        [JwtAuthentication]
        public IHttpActionResult GetUsers(int roleId, string userName)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;                   //Getting from url header authorization

                var lstUsers = _service.GetUsersForDashboard(roleId, userName, accessToken);

                if (lstUsers != null && lstUsers.Count > 0)
                    return Content(HttpStatusCode.OK, lstUsers);
                else
                    return Content(HttpStatusCode.OK, "Content not found");

            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUsers : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetOnboardingCompletedEmployees")]
        [JwtAuthentication]
        public IHttpActionResult GetOnboardingCompletedEmployees(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetOnboardingCompletedEmployees(fromDate,toDate,accessToken);
                if (dashboardDetails.Count > 0)
                    return Content(HttpStatusCode.OK, dashboardDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardingCompletedEmployees :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetOnboardingInProgressEmployees")]
        [JwtAuthentication]
        public IHttpActionResult GetOnboardingInProgressEmployees(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetOnboardingInProgressEmployees(fromDate, toDate, accessToken);
                if (dashboardDetails.Count > 0)
                    return Content(HttpStatusCode.OK, dashboardDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardingInProgressEmployees :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetPre_OnboardingEmployees")]
        [JwtAuthentication]
        public IHttpActionResult GetPre_OnboardingEmployees(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetPre_OnboardingEmployees(fromDate, toDate, accessToken);
                if (dashboardDetails.Count > 0)
                    return Content(HttpStatusCode.OK, dashboardDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetPre_OnboardingEmployees :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetPendingOnboardingEmployees")]
        [JwtAuthentication]
        public IHttpActionResult GetPendingOnboardingEmployees(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetPendingOnboardingEmployees(fromDate, toDate, accessToken);
                if (dashboardDetails.Count > 0)
                    return Content(HttpStatusCode.OK, dashboardDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetPendingOnboardingEmployees :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        


        [HttpGet]
        [ActionName("GetPendingOnboardingCount")]
        [JwtAuthentication]
        public IHttpActionResult GetPendingOnboardUsersCount(bool isSuperAdmin)
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetPendingOnboardingUsersCount(isSuperAdmin, accessToken);
                if (dashboardDetails.Count > 0)
                {
                    return Content(HttpStatusCode.OK, dashboardDetails);
                }
                else
                    return Content(HttpStatusCode.OK, "Content not found.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardedUsers :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetSuperAdminUser")]
        [JwtAuthentication]
        public IHttpActionResult GetSuperAdminUser()
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetSuperAdminDetails(accessToken);
                return Content(HttpStatusCode.OK, dashboardDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardedUsers :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetSuperAdminUserServices")]
        [JwtAuthentication]
        public IHttpActionResult GetSuperAdminUserServices()
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetSuperAdminDetailsServices(accessToken);
                return Content(HttpStatusCode.OK, dashboardDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardedUsers :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetUserDetailsByEmailId")]
        [JwtAuthentication]
        public IHttpActionResult GetUserDetailsByMailId(string emailAddress)
        {
            try
            {

                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var userDetails = _service.GetUserByMailId(emailAddress, accessToken);
                if (userDetails != null)
                    return Content(HttpStatusCode.OK, userDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserDetailsByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        //[HttpGet]
        //[ActionName("GetImage")]
        //[JwtAuthentication]
        //public IHttpActionResult GetImage(int userId)
        //{
        //    try
        //    {

        //        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
        //        var userDetails = _service.GetImages(userId, accessToken);
        //        if (userDetails != null)
        //            return Content(HttpStatusCode.OK, userDetails);
        //        else
        //            return Content(HttpStatusCode.OK, "Content not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteException("GetImages : ", ex);
        //        return Content(HttpStatusCode.BadRequest, ex.Message);
        //    }
        //}

        [HttpPost]
        [JwtAuthentication]
        [ActionName("ChangeImage")]
        public IHttpActionResult ChangeImage(User model)
        {
            string message = string.Empty;
            
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                 message = _service.AddImage(model, accessToken);
                return Content(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                Logger.WriteException("ChangeImage : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }




        }


        [HttpGet]
        [ActionName("GetUserDetailsByEmailIdServices")]
        [JwtAuthentication]
        public IHttpActionResult GetUserDetailsByMailIdServices(string emailAddress)
        {
            try
            {

                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var userDetails = _service.GetUserByMailIdServices(emailAddress, accessToken);
                if (userDetails != null)
                    return Content(HttpStatusCode.OK, userDetails);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + emailAddress);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserDetailsByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetClientAdminUser")]
        [JwtAuthentication]
        public IHttpActionResult GetClientAdminUser()
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetClientAdminDetails(accessToken);
                return Content(HttpStatusCode.OK, dashboardDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardedUsers :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetClientAdminUserServices")]
        [JwtAuthentication]
        public IHttpActionResult GetClientAdminUserServices()
        {
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;
                var dashboardDetails = _service.GetClientAdminDetailsServices(accessToken);
                return Content(HttpStatusCode.OK, dashboardDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetOnboardedUsers :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetEverifyEmployees")]
        [JwtAuthentication]
        public IHttpActionResult GetEverifyEmployees(string userName)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;                   //Getting from url header authorization

                var lstUsers = _service.GetEverifyEmployees(userName, accessToken);

                if (lstUsers != null && lstUsers.Count > 0)
                {
                  
                    return Content(HttpStatusCode.OK, lstUsers);
                }
                else
                    return Content(HttpStatusCode.OK, "Content not found");

            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUsers : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [HttpGet]
        [JwtAuthentication]
        [ActionName("GetAllEmployeesUploadDocPackages")]
        public IHttpActionResult GetAllEmployeesUploadDocPackages(int docPackageId)
        {
            List<Model.ViewModels.UserWorkAddress> uploadDocumentById = null;
            try
            {
                string accessToken = Request.Headers.Authorization.Parameter;

                uploadDocumentById = _service.GetAllEmployeesUploadDocPackages(docPackageId, accessToken);
                if (uploadDocumentById.Count > 0)
                    return Content(HttpStatusCode.OK, uploadDocumentById);
                else
                    return Content(HttpStatusCode.OK, "Content not found");

            }
            catch (Exception ex)
            {
                Logger.WriteException("GetAllI9UploadDocumentbyUserId :", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
