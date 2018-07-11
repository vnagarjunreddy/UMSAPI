using PCR.Users.Model.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace PCR.Users.Data.Helpers
{
    public static class ConnectionTools
    {
        /// <summary>
        /// To get the connection of the database (using Non_PCROnBoard).
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetMasterConnection()
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

        public static DbConnection GetClientConnection(DBCredentials dbCredentials)
        {

            var dbConnectionString = "Data Source=" + dbCredentials.DBServer + "; Initial Catalog = " + dbCredentials.DBName + "; Persist Security Info = True; User ID = " + dbCredentials.DBUserId + "; Password = " + dbCredentials.DBPassword + "; MultipleActiveResultSets = True; Application Name = EntityFramework";

            // DbConnectionStringBuilder csb;
            //var entityCnxStringBuilder = new EntityConnectionStringBuilder
            //{
            //    ProviderConnectionString = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager
            //      .ConnectionStrings["PCROnBoard"].ConnectionString).ConnectionString
            //};
            //entityCnxStringBuilder.Provider = "System.Data.SqlClient";
            //var sqlCnxStringBuilder = new SqlConnectionStringBuilder(entityCnxStringBuilder.ProviderConnectionString);

            //csb = new DbConnectionStringBuilder { ConnectionString = sqlCnxStringBuilder.ConnectionString };
            string providerName = "System.Data.SqlClient";
            var factory = DbProviderFactories.GetFactory(providerName);
            var dbConnection = factory.CreateConnection();
            dbConnection.ConnectionString = dbConnectionString;
            dbConnection.Open();
            return dbConnection;
        }

        /// <summary>
        /// To get the connection of the database.
        /// </summary>
        /// <param name="databaseId"></param>
        /// <param name="pcrId"></param>
        /// <returns></returns>
        public static CGI4VB.OnboardingConnectionInfo GetOnboardingDbConnection(string databaseId=null, string pcrId= "")
        {
            try
            {
                PCRConnection onboardingPCR = new PCRConnection(ConfigurationManager.AppSettings["PCRAppPath"]);
                CGI4VB.OnboardingConnectionInfo dbc;
               
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
                    return dbc;
                }
                else
                {
                    throw new Exception("Unable to get database connection.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to get database connection.");
            }
        }

        public static DbConnection GetMasterDbConnection()
        {
            try
            {
                PCRConnection onboardingPCR = new PCRConnection(ConfigurationManager.AppSettings["PCRAppPath"]);
                DbConnection dbc;
                dbc = onboardingPCR.GetOnboardingMasterDBConnection();

                if (dbc == null)
                    throw new Exception("Unable to get database connection.");

                return dbc;
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to get database connection.");
            }
        }


    }      
}
