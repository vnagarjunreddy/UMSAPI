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
    public class RoleController : ApiController
    {
        private RoleService _service;
        public RoleController()
        {
            _service = new RoleService();
        }

        /// <summary>
        /// To get the all roles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoles")]
        [JwtAuthentication]
        public IHttpActionResult GetRoles()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstRoles = _service.GetRoles(accessToken);
                if (lstRoles != null && lstRoles.Count > 0)
                    return Content(HttpStatusCode.OK, lstRoles);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetRoles : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoleByID")]
        [JwtAuthentication]
        public IHttpActionResult GetRoleByID(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var role = _service.GetRoleByID(id, accessToken);
                if (role != null)
                    return Content(HttpStatusCode.OK, role);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + id);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetRoleByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To update the role deails by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("UpdateRole")]
        [ResponseType(typeof(void))]
        [JwtAuthentication]
        public IHttpActionResult UpdateRole(int id, Role role)
        {
            try
            {
                if (role != null)
                {
                    string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                    bool isRoleUpdated = _service.UpdateRole(id, role, accessToken);
                    if (isRoleUpdated)
                        return Content(HttpStatusCode.OK, "Role has been updated successfully.");
                    else
                        return Content(HttpStatusCode.OK, "Content not found by Id =" + id);
                }
                else
                    throw new Exception("Please provide the update role details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateRole : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To add the role details.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddRole")]
        [ResponseType(typeof(void))]
        [JwtAuthentication]
        public IHttpActionResult AddRole(Role role)
        {
            try
            {
                if (role != null)
                {
                    if (ModelState.IsValid)
                    {
                        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                        bool isRoleSeved = _service.AddRole(role, accessToken);
                        if (isRoleSeved)
                            return Content(HttpStatusCode.OK, "Role has been created successfully.");
                        else
                            return Content(HttpStatusCode.OK, "Role name is already exist.");
                    }
                    else
                    {
                        string modelError = null;
                        for (int i = 0; i < ModelState.Keys.Count; i++)
                            modelError += ModelState.Values.ToList()[i].Errors[0].ErrorMessage + ",";
                        return Content(HttpStatusCode.BadRequest, modelError);
                    }
                }
                else
                    throw new Exception("Please provide the role details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddRole : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To delete the role details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteRole")]
        [ResponseType(typeof(Role))]
        [JwtAuthentication]
        public IHttpActionResult DeleteRole(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.DeleteRole(id, accessToken);
                return Content(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("DeletePermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

    }
}
