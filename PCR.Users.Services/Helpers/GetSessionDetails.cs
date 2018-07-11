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
using System.Security.Claims;
using PCR.Users.Data;

namespace PCR.Users.Services.Helpers
{
    public class GetSessionDetails 
    {
        bool _isNonPCR = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNon_PCRDB"]);
        public SessionDetails GetSessionValues(string accessToken)
        {
            SessionDetails sessionDetails = new SessionDetails();
            try
            {
                if (_isNonPCR)
                {
                    var simplePrinciple = JwtManager.GetPrincipal(accessToken);
                    var identity = simplePrinciple.Identity as ClaimsIdentity;

                    var roleClaim = (identity.FindFirst(ClaimTypes.Role));
                    int roleId = (Convert.ToInt32(roleClaim?.Value));

                    var userClaim = (identity.FindFirst(ClaimTypes.Sid));
                    int userId = (Convert.ToInt32(userClaim?.Value));

                    var userNameClaim = (identity.FindFirst(ClaimTypes.Name));
                    string userName = userNameClaim?.Value;

                    var databaseClaim = (identity.FindFirst(ClaimTypes.Authentication));
                    string databseName = databaseClaim?.Value;

                    sessionDetails.databaseId = databseName;
                    //sessionDetails.DatabaseId() = databseName;
                    sessionDetails.RoleID = roleId;
                    sessionDetails.UserId = userId;
                    sessionDetails.UserName = userName;
                }
            }
            catch
            {
                throw new Exception("Invalid databaseId.");
            }
            return sessionDetails;
        }
    }
}
