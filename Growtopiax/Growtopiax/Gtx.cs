using System;
using System.Net;

namespace Growtopiax
{
    public class GrowtopiaxClient : WebClient
    {
        public GrowtopiaxClient() { }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            request.UserAgent = "Growtopiax-Client";

            return request;
        }
    }
}
