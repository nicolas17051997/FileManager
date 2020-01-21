using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace File_Sharing.Help
{
    public static class GetUrl
    {
        public static Uri GetUri(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80),
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return uriBuilder.Uri;
        }
    }
}
