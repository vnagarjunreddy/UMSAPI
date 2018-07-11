using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.API.Tests
{
    public class Helper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, results, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }

        public static Assembly GetOnboardingDbConnection(object sender, ResolveEventArgs args)
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
