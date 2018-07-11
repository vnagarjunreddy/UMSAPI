using PCR.Users.API.Helpers;
using PCR.Users.Models;
using PCR.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PCR.Users.API.Controllers
{
    public class UserRoleController : ApiController
    {
        private UserRoleService _service;
        public UserRoleController()
        {
            _service = new UserRoleService();
        }

        /// <summary>
        /// To get the all user role details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserRoles")]
        public IHttpActionResult GetUserRoles()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstUsrRole = _service.GetUserRoles(accessToken);
                if (lstUsrRole.Count > 0)
                    return Content(HttpStatusCode.OK, lstUsrRole);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserRoles : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the user role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserRoleByID")]
        public IHttpActionResult GetUserRoleByID(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var usrRole = _service.GetUserRoleByID(id, accessToken);
                if (usrRole != null)
                    return Content(HttpStatusCode.OK, usrRole);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + id);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserRoleByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To update the user role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userrole"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("UpdateUserRole")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateUserRole(int id, UserRole userrole)
        {
            try
            {
                if (userrole != null)
                {
                    string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                    bool isUserRoleUpdated = _service.UpdateUserRole(id, userrole, accessToken);
                    if (isUserRoleUpdated)
                        return Content(HttpStatusCode.OK, "UserRole has been updated successfully.");
                    else
                        return Content(HttpStatusCode.OK, "Content not found by Id =" + id);
                }
                else
                    throw new Exception("Please provide the update user role details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateUserRole : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To add the user role details.
        /// </summary>
        /// <param name="userrole"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddUserRole")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddUserRole(UserRole userrole)
        {
            try
            {
                if (userrole != null)
                {
                    if (ModelState.IsValid)
                    {
                        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                        bool isUserRoleSaved = _service.AddUserRole(userrole, accessToken);
                        if (isUserRoleSaved)
                            return Content(HttpStatusCode.OK, "UserRole has been created successfully.");
                        else
                            return Content(HttpStatusCode.OK, "UserRole is already exist.");
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
                    throw new Exception("Please provide the user role details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddUserRole : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To delete the user role details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteUserRole")]
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult DeleteUserRole(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.DeleteUserRole(id, accessToken);
                return Content(HttpStatusCode.OK, msg);
            }            
            catch (Exception ex)
            {
                Logger.WriteException("DeleteUserRole : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To get the userroleId based on userId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserRoleID")]
        public IHttpActionResult GetUserRoleID(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                int userRoleId = _service.GetUserRoleID(id, accessToken);
                if (userRoleId > 0)
                    return Content(HttpStatusCode.OK, userRoleId);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + id);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetUserRoleID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
