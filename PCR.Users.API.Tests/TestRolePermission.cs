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
    public class TestRolePermission
    {
        string _accessToken = string.Empty;
        public TestRolePermission()
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


        #region Add Role Permission

        [TestMethod]
        public void TestAddRolepermission_ShouldAddRolePermissionWithCorrectParameters()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;
            
            var item = AddRolePermission1();
            controller.Configuration = new HttpConfiguration();
            controller.Validate(item);
            var negResult = controller.AddRolePermission(item) as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("RolePermission has been created successfully.", negResult.Content);
            Assert.AreNotEqual("RolePermission is already exist.", negResult.Content);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestAddRolepermission_ShouldAddRolePermissionWithoutOptionalFields()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var item = AddRolePermission2();
            controller.Configuration = new HttpConfiguration();
            controller.Validate(item);
            var negResult = controller.AddRolePermission(item) as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual("RolePermission has been created successfully.", negResult.Content);
            Assert.AreNotEqual("RolePermission is already exist.", negResult.Content);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestAddRolepermission_ShouldNotAddRolePermissionWithoutMandatoryFields()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var item = AddRolePermission3();
            controller.Configuration = new HttpConfiguration();
            controller.Validate(item);
            var negResult = controller.AddRolePermission(item) as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        }

        [TestMethod]
        public void TestAddRolepermission_ShouldNotAddRolePermissionWithParametersAboveRange()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var item = AddRolePermission4();
            controller.Configuration = new HttpConfiguration();
            controller.Validate(item);
            var negResult = controller.AddRolePermission(item) as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        }

        #endregion

        #region Update Role Permission

        [TestMethod]
        public void TestUpdateRolePermission_ShouldUpdateRolePermissionWithCorrectParameters()
        {
            var controller = new RolePermissionController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var item = UpdateRolePermission1();
            IHttpActionResult actionResult = controller.UpdateRolePermission(6, item);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreEqual("RolePermission has been updated successfully.", negResult.Content);
            Assert.AreNotEqual("Content not found by Id =" + 6, negResult.Content);
        }

        [TestMethod]
        public void TestUpdateRolePermission_ShouldNotUpdateRolePermissionWithWrongId()
        {
            var controller = new RolePermissionController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var item = UpdateRolePermission2();
            IHttpActionResult actionResult = controller.UpdateRolePermission(9999999, item);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestUpdateRolePermission_ShouldNotUpdateRolePermissionWithParametersAboveTheRange()
        {
            var controller = new RolePermissionController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var item = UpdateRolePermission3();
            IHttpActionResult actionResult = controller.UpdateRolePermission(10, item);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        }

        [TestMethod]
        public void TestUpdateRolePermission_ShouldUpdateRolePermissionWithIdButWithoutParameters()
        {
            var controller = new RolePermissionController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var item = UpdateRolePermission4();
            IHttpActionResult actionResult = controller.UpdateRolePermission(6, item);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, negResult.StatusCode);
        }

        #endregion

        #region Get Role Permission

        [TestMethod]
        public void TestGetRolePermission_ShouldGetAllRolePermissions()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetRolePermissions();
            var negResult = actionResult as NegotiatedContentResult<IList<RolePermission>>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreNotEqual("Unable to get database connection.", negResult.Content);
            Assert.AreNotEqual("Content not found", negResult.Content);
        }

        [TestMethod]
        public void TestGetRolePermission_ShouldGetRolePermissionsWithCorrectId()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetRolePermissionByID(4);
            var negResult = actionResult as NegotiatedContentResult<RolePermission>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestGetRolePermission_ShouldNotGetRolePermissionsWithWrongId()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetRolePermissionByID(999999);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreEqual("Content not found id = " + 999999, negResult.Content);
        }

        #endregion

        #region Delete Role Permission

        [TestMethod]
        public void TestDeletePermission_ShouldDeleteRolePermissionWithCorrectId()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteRolePermission(2011);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreEqual("RolePermission has been deleted successfully.", negResult.Content);
        }

        [TestMethod]
        public void TestDeletePermission_ShouldNotDeleteRolePermissionWithWrongId()
        {
            var controller = new RolePermissionController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteRolePermission(99999999);
            var negResult = actionResult as NegotiatedContentResult<string>;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreEqual("Content not found by Id =" + 99999999, negResult.Content);
        }

        #endregion


        #region Inputs

        RolePermission AddRolePermission1()
        {
            return new RolePermission()
            {
                RoleID = 2020,
                PermissionID = 2020,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission AddRolePermission2()
        {
            return new RolePermission()
            {
                RoleID = 2020,
                PermissionID = 2019,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission AddRolePermission3()
        {
            return new RolePermission()
            {
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission AddRolePermission4()
        {
            return new RolePermission()
            {
                RoleID = 1,
                PermissionID = 999999999,
                CreatedBy = "test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test ",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test ",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission UpdateRolePermission1()
        {
            return new RolePermission()
            {
                RoleID = 2019,
                PermissionID = 2020,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission UpdateRolePermission2()
        {
            return new RolePermission()
            {
                RoleID = 2019,
                PermissionID = 2019,
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission UpdateRolePermission3()
        {
            return new RolePermission()
            {
                RoleID = 2,
                PermissionID = 9999999,
                CreatedBy = "test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test ",
                CreatedDate = DateTime.Now,
                UpdatedBy = "test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test test ",
                UpdatedDate = DateTime.Now,
                DatabaseId = "onboarding$.txt"
            };
        }

        RolePermission UpdateRolePermission4()
        {
            return new RolePermission()
            {
                DatabaseId = "onboarding$.txt"
            };
        }

        #endregion


    }
}
