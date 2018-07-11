using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCR.Users.API.Controllers;
using PCR.Users.Data;
using PCR.Users.Model.ViewModels;
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
    public class TestReports
    {
        string _accessToken = string.Empty;
        public TestReports()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(Helper.GetOnboardingDbConnection);

            var controller = new UserController();
            IHttpActionResult item = controller.GetAccessToken("onboardsuperuser@gmail.com", "superuser", "onboarding$.txt");
            var negResult = item as NegotiatedContentResult<string>;
            _accessToken = negResult.Content;
            Assert.IsNotNull(negResult);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
            Assert.AreNotEqual("Unable to get database connection.", negResult.Content);
            Assert.AreNotEqual("Content not found", negResult.Content);
        }
        
        [TestMethod]
        public void TestGetUsers_ShouldGetOnboardingCompletedEmployees()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetOnboardingCompletedEmployees(null, null);
            var negResult = response as NegotiatedContentResult<IList<Model.ViewModels.UserWorkAddress>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestGetUser_ShouldGetOnboardingInProgressEmployees()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetOnboardingInProgressEmployees(null, null);
            var negResult = response as NegotiatedContentResult<IList<Model.ViewModels.UserWorkAddress>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }


        [TestMethod]
        public void TestGetReports_ShouldGetPre_OnboardingEmployees()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetPre_OnboardingEmployees(null, null);
            var negResult = response as NegotiatedContentResult<IList<Model.ViewModels.UserWorkAddress>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);
        }

        [TestMethod]
        public void TestGetRoles_ShouldGetPendingOnboardingEmployees()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetPendingOnboardingEmployees(null, null);
            var negResult = response as NegotiatedContentResult<IList<Model.ViewModels.UserWorkAddress>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);

        }

        [TestMethod]
        public void TestGetRoles_ShouldGetPendingOnboardUsersCount()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetPendingOnboardUsersCount(true);
            var negResult = response as NegotiatedContentResult<List<Model.ViewModels.GetPendingOnboardUsers>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);

        }

        [TestMethod]
        public void TestGetRoles_ShouldGetOnboardDashboardDetails()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetOnboardDashboardDetails("tarun",3);
            var negResult = response as NegotiatedContentResult<DashboardDetails>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);

        }

        [TestMethod]
        public void TestGetRoles_ShouldGetMothWiseOnboardDetails()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetMothWiseOnboardDetails(1,"tarun", 3);
            var negResult = response as NegotiatedContentResult<List<UserMonthWiseCount>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);

        }


        [TestMethod]
        public void TestGetRoles_ShouldGetGetUsers()
        {
            var controller = new UserController();
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            var response = controller.GetUsers(1,"tarun");
            var negResult = response as NegotiatedContentResult<IList<Model.ViewModels.UserWorkAddress>>;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, negResult.StatusCode);

        }



    }

    }
