//using Newtonsoft.Json;
using PCR.Users.Services;
using PCR.Users.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Services.Helpers
{
    public static class ConnectionTools
    {      
        /// <summary>
        /// To get the connection of the database (using Non_PCROnBoard).
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetConnection()
        {
            DbConnectionStringBuilder csb;
            var entityCnxStringBuilder = new EntityConnectionStringBuilder
            {
                ProviderConnectionString = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager
                  .ConnectionStrings["PCROnBoardUMS"].ConnectionString).ConnectionString
            };
            entityCnxStringBuilder.Provider = "System.Data.SqlClient";
            var sqlCnxStringBuilder = new SqlConnectionStringBuilder(entityCnxStringBuilder.ProviderConnectionString);

            csb = new DbConnectionStringBuilder { ConnectionString = sqlCnxStringBuilder.ConnectionString };
            string providerName = "System.Data.SqlClient";
            var factory = DbProviderFactories.GetFactory(providerName);
            var dbConnection = factory.CreateConnection();
            dbConnection.ConnectionString = csb.ConnectionString;
            return dbConnection;
        }

        /// <summary>
        /// To get the connection of the database.
        /// </summary>
        /// <param name="databaseId"></param>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        public static IDbConnection GetOnboardingDbConnection(string databaseId=null, string pcrId= "")
        {
            try
            {
                PCRConnection onboardingPCR = new PCRConnection(ConfigurationManager.AppSettings["PCRAppPath"]);
                CGI4VB.OnboardingConnectionInfo dbc;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]) == true)
                {
                    return GetConnection();
                }

                if (String.IsNullOrEmpty(pcrId))
                {
                    dbc = onboardingPCR.GetOnboardingDBConnections(databaseId);                  
                }
                else
                {
                    dbc = onboardingPCR.GetOnboardingDBConnections(databaseId, pcrId);                   
                }
                if (dbc != null)
                {
                    dbc.MasterDBConnection.Close();
                    return dbc.ClientDBConnection;
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch
            {
                throw new Exception("Unable to get database connection.");
            }
        }
    }      
}
