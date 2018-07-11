using PCR.Users.Data.Helpers;
using PCR.Users.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Data
{
   public class BaseRepository : IDisposable
    {
        protected UMSContext _masterDbContext;
        protected UMSContext _clientDbContext;
        private CGI4VB.OnboardingConnectionInfo _connectionInfo;

        /// <summary>
        /// Constructor for authenticated PCR Access
        /// </summary>
        /// <param name="databaseId">databaseId to connect to</param>
        /// <param name="pcrId">existing user PCr-ID</param>
        public BaseRepository(string databaseId, string pcrId)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]) == true)
            {
                _masterDbContext = new UMSContext(ConnectionTools.GetMasterConnection());

                GetConnection(databaseId);
            }
            else
            {
                _connectionInfo = ConnectionTools.GetOnboardingDbConnection(databaseId, pcrId);
                _masterDbContext = new UMSContext(_connectionInfo.MasterDBConnection);
                _clientDbContext = new UMSContext(_connectionInfo.ClientDBConnection);
            }
        }

        /// <summary>
        /// Constructor for public access, creates a connection without an existing PCR-ID
        /// </summary>
        /// <param name="databaseId">DatabaseId to connect to</param>
        public BaseRepository(string databaseId)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]) == true)
            {
                _masterDbContext = new UMSContext(ConnectionTools.GetMasterConnection());

                GetConnection(databaseId);
            }
            else
            {
                _connectionInfo = ConnectionTools.GetOnboardingDbConnection(databaseId);
                _masterDbContext = new UMSContext(_connectionInfo.MasterDBConnection);
                _clientDbContext = new UMSContext(_connectionInfo.ClientDBConnection);
            }
        }

        public BaseRepository()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]) == true)
            {
                _masterDbContext = new UMSContext(ConnectionTools.GetMasterConnection());
            }
            else
            {
                _masterDbContext = new UMSContext(ConnectionTools.GetMasterDbConnection());
            }
        }


        public UMSContext GetConnection(string databaseId)
        {

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            //HttpResponseMessage response = client.GetAsync(ConfigurationManager.AppSettings["UMS_URL"] + "/api/User/GetUserDetailsByID?userId=" + userId).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var responseResult = response.Content.ReadAsStringAsync().Result;
            //    if (responseResult != "\"Content not found id = " + userId + "\"" && responseResult != "Content not found id = " + userId)
            //    {
            //        return true;
            //    }
            //}
            //throw new Exception("Invalid userId");

            var db = (from c in _masterDbContext.Clients
                      where c.DBName == databaseId
                      select new DBCredentials
                      {
                          ClientID = c.ClientID,
                          DBName = c.DBName,
                          DBPassword = c.DBPassword,
                          DBServer = c.DBServer,
                          DBUserId = c.DBUserId
                      }).FirstOrDefault();

            if (db != null)
            {
                _clientDbContext = new UMSContext(ConnectionTools.GetClientConnection(db));
            }
            return _clientDbContext;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_clientDbContext != null)
                        _clientDbContext.Dispose();

                    if (_masterDbContext != null)
                        _masterDbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

     

    }
}
