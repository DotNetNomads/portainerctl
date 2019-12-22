using System;
using System.Collections.Generic;
using RestSharp;

namespace PortainerClient.Api.Base
{
    /// <summary>
    /// Helpers for the Portainer API
    /// </summary>
    public static class BaseApiHelpers
    {
        /// <summary>
        /// Fill the request with provided parameters
        /// </summary>
        /// <param name="request">API request</param>
        /// <param name="parameters">List with request parameters</param>
        public static void AddParameters(this IRestRequest request,
            IEnumerable<(string paramName, object paramValue)> parameters)
        {
            foreach (var (paramName, paramValue) in parameters) request.AddParameter(paramName, paramValue);
        }

        /// <summary>
        /// Fill the request with provided parameters
        /// </summary>
        /// <param name="request">API request</param>
        /// <param name="parameters">List with request parameters</param>
        /// <exception cref="InvalidOperationException">Occurs when parameter type is not implemented</exception>
        public static void AddParameters(this IRestRequest request,
            IEnumerable<(string paramName, object value, ParamType type)> parameters)
        {
            foreach (var (paramName, value, type) in parameters)
            {
                switch (type)
                {
                    case ParamType.File:
                        request.AddFile(paramName, value.ToString());
                        break;
                    case ParamType.QueryParam:
                        request.AddQueryParameter(paramName, value.ToString());
                        break;
                    case ParamType.BodyParam:
                        request.AddParameter(paramName, value);
                        break;
                    case ParamType.JsonBody:
                        request.RequestFormat = DataFormat.Json;
                        request.AddJsonBody(value);
                        break;
                    default:
                        throw new InvalidOperationException($"You should define parameter type for {paramName} ");
                }
            }
        }
    }
}
