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
    public class RolePermissionController : ApiController
    {
        private RolePermissionService _service;
        public RolePermissionController()
        {
            _service = new RolePermissionService();
        }

        /// <summary>
        /// To get the all role permission details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRolePermissions")]
        [JwtAuthentication]
        public IHttpActionResult GetRolePermissions()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstRolePermission = _service.GetRolePermissions(accessToken);
                if (lstRolePermission != null && lstRolePermission.Count > 0)
                    return Content(HttpStatusCode.OK, lstRolePermission);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetRolePermissions : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the role permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRolePermissionByID")]
        [JwtAuthentication]
        public IHttpActionResult GetRolePermissionByID(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var rolePermission = _service.GetRolePermissionByID(id, accessToken);
                if (rolePermission != null)
                    return Content(HttpStatusCode.OK, rolePermission);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = " + id);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetRolePermissionByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To update the role permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rolepermission"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("UpdateRolePermission")]
        [ResponseType(typeof(void))]
        [JwtAuthentication]
        public IHttpActionResult UpdateRolePermission(int id, RolePermission rolepermission)
        {
            try
            {
                if (rolepermission != null)
                {
                    string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                    bool isRolePermissionUpdated = _service.UpdateRolePermission(id, rolepermission, accessToken);
                    if (isRolePermissionUpdated)
                        return Content(HttpStatusCode.OK, "RolePermission has been updated successfully.");
                    else
                        return Content(HttpStatusCode.OK, "Content not found by Id =" + id);
                }
                else
                    throw new Exception("Please provide the update role permission details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdateRolePermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To add the role permission details.
        /// </summary>
        /// <param name="rolepermission"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddRolePermission")]
        [ResponseType(typeof(void))]
        [JwtAuthentication]
        public IHttpActionResult AddRolePermission(RolePermission rolepermission)
        {
            try
            {
                if (rolepermission != null)
                {
                    if (ModelState.IsValid)
                    {
                        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                        bool isRolePermissionSaved = _service.AddRolePermission(rolepermission, accessToken);
                        if (isRolePermissionSaved)
                            return Content(HttpStatusCode.OK, "RolePermission has been created successfully.");
                        else
                            return Content(HttpStatusCode.OK, "RolePermission is already exist.");
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
                    throw new Exception("Please provide the role permission details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddRolePermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To delete the role permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteRolePermission")]
        [ResponseType(typeof(RolePermission))]
        [JwtAuthentication]
        public IHttpActionResult DeleteRolePermission(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.DeleteRolePermission(id, accessToken);
                return Content(HttpStatusCode.OK, msg);
            }
            catch (Exception ex)
            {
                Logger.WriteException("DeleteRolePermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }


    }
}
