using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCR.Users.API.Controllers;
using PCR.Users.Data;
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PCR.Users.API.Tests
{
    [TestClass]
    public class TestRole
    {
        string _accessToken = string.Empty;
        public TestRole()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(Helper.GetOnboardingDbConnection);

            var controller = new UserController();
            IHttpActionResult item = controller.GetAccessToken("ganesh.betabulls@gmail.com", "123456", "onboarding$.txt");
            var negResult = item as NegotiatedContentResult<string>;
            _accessToken = negResult.Content;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreNotEqual("Unable to get database connection.", negResult.Content);
            Assert.AreNotEqual("Content not found", negResult.Content);
        }
        
        #region Add Role

        //[TestMethod]
        //public void TestAddRole_ShouldAddRoleWithCorrectParameters()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;
            
        //    var item = AddRole1();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Validate(item);
        //    IHttpActionResult result = controller.AddRole(item);
        //    var negResult = result as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual("Role has been created successfully.", negResult.Content);
        //    Assert.AreNotEqual("Role name is already exist.", negResult.Content);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestAddRole_ShouldNotAddRoleWithoutMandatoryFields()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;

        //    var item = AddRole2();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Validate(item);
        //    IHttpActionResult result = controller.AddRole(item);
        //    var negResult = result as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestAddRole_ShouldAddRoleWithoutOptionalFields()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;

        //    var item = AddRole3();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Validate(item);
        //    IHttpActionResult result = controller.AddRole(item);
        //    var negResult = result as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual("Role has been created successfully.", negResult.Content);
        //    Assert.AreNotEqual("Role name is already exist.", negResult.Content);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestAddRole_ShouldNotAddRoleWithParametersAboveRange()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;

        //    var item = AddRole4();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Validate(item);
        //    IHttpActionResult result = controller.AddRole(item);
        //    var negResult = result as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        //}

        //#endregion

        //#region Update Role

        //[TestMethod]
        //public void TestUpdateRole_ShouldUpdateRoleWithCorrectParameters()
        //{
        //    var controller = new RoleController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

        //    var testDocument = UpdateRole1();
        //    IHttpActionResult response = controller.UpdateRole(9, testDocument);
        //    var negResult = response as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //    Assert.AreEqual("Role has been updated successfully.", negResult.Content);
        //    Assert.AreNotEqual("Content not found by Id =" + 9, negResult.Content);
        //}

        //[TestMethod]
        //public void TestUpdateRole_ShouldNotUpdateRoleWithWrongId()
        //{
        //    var controller = new RoleController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

        //    var testDocument = UpdateRole2();
        //    IHttpActionResult response = controller.UpdateRole(999999, testDocument);
        //    var negResult = response as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(response);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestUpdateRole_ShouldNotUpdateRoleWithParametersBeyondTheRange()
        //{
        //    var controller = new RoleController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

        //    var testDocument = UpdateRole3();
        //    IHttpActionResult response = controller.UpdateRole(10, testDocument);
        //    var negResult = response as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(response);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestUpdateRole_ShouldNotUpdateRoleWithIdButWithoutParameters()
        //{
        //    var controller = new RoleController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

        //    var testDocument = UpdateRole4();
        //    IHttpActionResult response = controller.UpdateRole(2, testDocument);
        //    var negResult = response as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //    Assert.AreNotEqual("Content not found by Id =" + 2, negResult.Content);
        //}

        #endregion

        #region Get Role

        [TestMethod]
        public void TestGetRoles_ShouldGetAllRoles()
        {
            var controller = new RoleController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer X8dxdIrfZ5f8l8mvXmjQId940pXvzIuRhPc8AaG/dn6mloK1mOfBu9GHD3IpZA==");
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult item = controller.GetRoles();
            var negResult = item as NegotiatedContentResult<IList<Role>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreNotEqual("Unable to get database connection.", negResult.Content);
            Assert.AreNotEqual("Content not found", negResult.Content);
        }

        //[TestMethod]
        //public void TestGetRoles_ShouldGetRoleWithCorrenctId()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;
            
        //    var response = controller.GetRoleByID(1);
        //    var negResult = response as NegotiatedContentResult<Role>;
        //    Assert.IsNotNull(response);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //}

        //[TestMethod]
        //public void TestGetRoles_ShouldNotGetRoleWithWrongId()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;
            
        //    var response = controller.GetRoleByID(99999);
        //    var negResult = response as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(response);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //    Assert.AreEqual("Content not found id = " + 99999, negResult.Content);
        //}

        #endregion

        //#region Delete Role

        //[TestMethod]
        //public void TestDeleteRole_ShouldDeleteRoleWithCorrectId()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;

        //    IHttpActionResult actionResult = controller.DeleteRole(11);
        //    var negResult = actionResult as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(negResult);
        //    Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        //    Assert.AreEqual("Role has been deleted successfully.", negResult.Content);
        //}

        //[TestMethod]
        //public void TestDeleteRole_ShouldNotDeleteRoleWithWrongId()
        //{
        //    var controller = new RoleController();
        //    var controllerContext = new HttpControllerContext();
        //    var request = new HttpRequestMessage();
        //    request.Headers.Add("Authorization", "Bearer " + _accessToken);
        //    // Don't forget these lines, if you do then the request will be null.
        //    controllerContext.Request = request;
        //    controller.ControllerContext = controllerContext;

        //    IHttpActionResult actionResult = controller.DeleteRole(9999);
        //    var negResult = actionResult as NegotiatedContentResult<string>;
        //    Assert.IsNotNull(actionResult);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        //    Assert.AreEqual("Content not found by Id =" + 9999, negResult.Content);
        //}

        //#endregion



        #region Inputs

        Role AddRole1()
        {
            return new Role()
            {
                RoleID = 1,
                RoleName = "UnitTestRole10m12",
                Description = "this is super admin module",
                IsActive = true,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
        
            };
        }

        Role AddRole2()
        {
            return new Role()
            {
                RoleID = -100,
                // RoleName = "Super Admin",
                Description = "this is super admin module",
                IsActive = true,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        Role AddRole3()
        {
            return new Role()
            {
                RoleName = "UnitTestRole20",
                DatabaseId = "onboarding$.txt"
            };
        }

        Role AddRole4()
        {
            return new Role()
            {
                RoleName = "1Super Adminsmkngjfjslgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjgh",
                Description = "this is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjgh",
                IsActive = true,
                DatabaseId = "onboarding$.txt"
            };
        }

        Role UpdateRole1()
        {
            return new Role()
            {
                RoleID = 1,
                RoleName = "UpdateUnitTestRole10",
                Description = "this is super admin module",
                IsActive = true,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        Role UpdateRole2()
        {
            return new Role()
            {
                RoleID = 1,
                RoleName = "UpdateUnitTestRole20",
                Description = "this is super admin module",
                IsActive = true,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        Role UpdateRole3()
        {
            return new Role()
            {
                RoleName = "1Super Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjgh",
                Description = "this is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghthis is super admin moduleSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper AdminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjghSuper Adminsmkngjfjlgkhfjlghjahfdjhgjhfjghjhfgfjhgjlfhgjhfdjghjkhfjghjfhgjhjfhgjhfjghfgjhfjghjsfhgjfhjghfsjghfjhgjfhgjfhgjhfjghfjghjfhgjfhghfjghjfhgjhfjgh",
                IsActive = true,
                DatabaseId = "onboarding$.txt"
            };
        }

        Role UpdateRole4()
        {
            var Role = new Role
            {
                DatabaseId = "onboarding$.txt"
            };
            return Role;
        }
     
        #endregion
    }
}
