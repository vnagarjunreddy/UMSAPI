using Newtonsoft.Json;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PCR.Users.API.Helpers
{
    /// <summary>
    /// Global Filter to log the Tracing information
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public sealed class LoggingFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger<LoggingFilterAttribute> logger = new Log4NetLogger<LoggingFilterAttribute>();

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            string controllerNamespace = string.Format("Controller:{0}|Action:{1}", actionContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionContext.ActionContext.ActionDescriptor.ActionName);

            if (actionContext.Response != null && actionContext.Exception == null)
            {
                var reponseObject = new
                {
                    Response = new
                    {
                        StatusCode = actionContext.Response.StatusCode,
                        StatusDescription = actionContext.Response.Content,
                        Exception = actionContext.Exception
                    }
                };
                logger.Info(string.Format("Response Details: {0}.{1}{2}", controllerNamespace, Environment.NewLine, JsonConvert.SerializeObject(reponseObject)));

            }
            else
            {
                logger.Info(string.Format("Response Details: {0} - Exception Occurred.", controllerNamespace));
            }
            base.OnActionExecuted(actionContext);
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string controllerNamespace = string.Format("Controller:{0}|Action:{1}",
                            actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                            actionContext.ActionDescriptor.ActionName);

            var requestObject = new
            {
                Request = new
                {
                    Parameters = JsonConvert.SerializeObject(actionContext.ActionArguments),
                    Url = actionContext.Request.RequestUri,
                    Headers = JsonConvert.SerializeObject(actionContext.Request.Headers)
                }
            };

            logger.Info(string.Format("Request Details: {0}.{1}{2}", controllerNamespace, Environment.NewLine, JsonConvert.SerializeObject(requestObject)));

            base.OnActionExecuting(actionContext);
        }

    }
}