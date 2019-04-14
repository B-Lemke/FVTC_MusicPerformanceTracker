using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BAH.MusicPerformanceTracker.SL.Models
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {


            IEnumerable<string> apiKeyHeaderValues = null;

            //x-apiKey is a key name that we came up with, nothing special about it
            if (request.Headers.TryGetValues("x-apikey", out apiKeyHeaderValues))
            {
                //Api-key header exists, check if it matches
                var apiKeyHeaderValue = apiKeyHeaderValues.FirstOrDefault();
                if (apiKeyHeaderValue == "12345")
                {
                    //We're good, allow the traffic
                    return base.SendAsync(request, cancellationToken);
                }


            }

            //Disallow any other traffic
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;

        }
    }
}