using PCR.Users.API.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PCR.Users.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

            //GlobalConfiguration.Configuration.Filters.Add(new LoggingFilterAttribute());
            //GlobalConfiguration.Configuration.Filters.Add(new ExceptionLoggerFilter());

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(GetOnboardingDbConnection);

        }

        static Assembly GetOnboardingDbConnection(object sender, ResolveEventArgs args)
        {
            string folderPath = ConfigurationManager.AppSettings["PCRAppPath"];
            string assemblyPathDll = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
            string assemblyPathExe = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".exe");
            string assemblyPathDllBin = Path.Combine(folderPath + "\\bin", new AssemblyName(args.Name).Name + ".dll");
            Assembly assembly;
            if (File.Exists(assemblyPathDll))
            {
                assembly = Assembly.LoadFrom(assemblyPathDll);
            }
            else if (File.Exists(assemblyPathExe))
            {
                assembly = Assembly.LoadFrom(assemblyPathExe);
            }
            else if (File.Exists(assemblyPathDllBin))
            {
                assembly = Assembly.LoadFrom(assemblyPathDllBin);
            }
            else
            {
                return null;
            }
            return assembly;
        }
    }
}
