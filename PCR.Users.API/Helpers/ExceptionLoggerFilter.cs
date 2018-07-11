using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace PCR.Users.API.Helpers
{

    /// <summary>
    /// Log Exception details globally.
    /// </summary>
    public sealed class ExceptionLoggerFilter : ExceptionFilterAttribute
    {

        private ILogger<ExceptionLoggerFilter> _logger = new Log4NetLogger<ExceptionLoggerFilter>();
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                _logger.Error(String.Format("Controller:{0}|Action:{1}|Action Parameters:{2}",
                    filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                    filterContext.ActionContext.ActionDescriptor.ActionName,
                    JsonConvert.SerializeObject(filterContext.ActionContext.ActionArguments)),
                    filterContext.Exception);

                filterContext.Response = filterContext.Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Error Occurred. Please contact System Administrator.");
            }

        }

    }


}