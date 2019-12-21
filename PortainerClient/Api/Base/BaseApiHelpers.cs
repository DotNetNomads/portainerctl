using System.Collections.Generic;
using RestSharp;

namespace PortainerClient.Api
{
    public static class BaseApiHelpers
    {
        public static void AddParameters(this RestRequest request,
            IEnumerable<(string paramName, object paramValue)> parameters)
        {
            foreach (var (paramName, paramValue) in parameters) request.AddParameter(paramName, paramValue);
        }
    }
}
