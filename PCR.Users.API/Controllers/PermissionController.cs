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
    public class PermissionController : ApiController
    {
        private PermissionService _service;        
        public PermissionController()
        {
            _service = new PermissionService();
        }

       /// <summary>
       /// To get all permission details.
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [JwtAuthentication]
        [ActionName("GetPermissions")]
        public IHttpActionResult GetPermissions()
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var lstPermissions = _service.GetPermissions(accessToken);
                if (lstPermissions != null && lstPermissions.Count > 0)
                    return Content(HttpStatusCode.OK, lstPermissions);
                else
                    return Content(HttpStatusCode.OK, "Content not found");
            }
            catch(Exception ex)
            {
                Logger.WriteException("GetPermissions : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To get the permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthentication]
        [ActionName("GetPermissionByID")]
        public IHttpActionResult GetPermissionByID(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                var permission = _service.GetPermissionByID(id, accessToken);
                if (permission != null)
                    return Content(HttpStatusCode.OK, permission);
                else
                    return Content(HttpStatusCode.OK, "Content not found id = "+id);
            }
            catch (Exception ex)
            {
                Logger.WriteException("GetPermissionByID : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// To update the permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPut]
        [JwtAuthentication]
        [ActionName("UpdatePermission")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePermission(int id, Permission permission)
        {
            try
            {
                if (permission != null)
                {
                    string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                    bool isPermissionUpdated = _service.UpdatePermission(id, permission, accessToken);
                    if (isPermissionUpdated)
                        return Content(HttpStatusCode.OK, "Permission has been updated successfully.");
                    else
                        return Content(HttpStatusCode.OK, "Content not found by Id =" + id);
                }
                else
                    throw new Exception("Please provide the update permission details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("UpdatePermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To add the permission details.
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthentication]
        [ActionName("AddPermission")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddPermission(Permission permission)
        {   
            try
            {
                if (permission != null)
                {
                    if (ModelState.IsValid)
                    {
                        string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                        bool isPermissionSaved = _service.AddPermission(permission, accessToken);
                        if (isPermissionSaved)
                            return Content(HttpStatusCode.OK, "Permission has been created successfully.");
                        else
                            return Content(HttpStatusCode.OK, "PermissionName is already exist.");
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
                    throw new Exception("Please provide the permission details.");
            }
            catch (Exception ex)
            {
                Logger.WriteException("AddPermission : ", ex);
                return Content(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// To delete the permission details by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeletePermission")]
        [ResponseType(typeof(Permission))]
        [JwtAuthentication]
        public IHttpActionResult DeletePermission(int id)
        {
            try
            {
                string accessToken = Request.Headers.Authorization == null ? null : Request.Headers.Authorization.Parameter;
                string msg = _service.DeletePermission(id, accessToken);
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
