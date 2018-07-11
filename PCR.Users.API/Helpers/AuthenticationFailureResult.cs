using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PCR.Users.API.Helpers
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; }

        public HttpRequestMessage Request { get; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = Request.CreateResponse(HttpStatusCode.Unauthorized, ReasonPhrase);

            return response;
        }
    }
}