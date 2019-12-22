using System;
using PortainerClient.Api.Model;
using PortainerClient.Config;
using RestSharp;
using RestSharp.Authenticators;

namespace PortainerClient.Api.Base
{
    /// <summary>
    /// Base implementation of Portainer API service
    /// </summary>
    public abstract class BaseApiService
    {
        private RestClient _client;

        private RestClient ResolveClient()
        {
            if (_client != null)
                return _client;
            var config = ConfigModel.Load();
            _client ??= new RestClient(config.Url);
            _client.Authenticator = new JwtAuthenticator(config.Token);
            return _client;
        }

        /// <summary>
        /// Do Get request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Response type</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Get<T>(string resource, params (string paramName, object paramValue)[] parameters)
            where T : new() =>
            ExecuteRequest<T>(
                resource,
                Method.GET,
                request => request.AddParameters(parameters));

        /// <summary>
        /// Do Post request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Parsed instance of T</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Post<T>(string resource, params (string paramName, object value, ParamType type)[] parameters)
            where T : new() => ExecuteRequest<T>(
            resource,
            Method.POST,
            request => request.AddParameters(parameters));

        /// <summary>
        /// Do Put request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Parsed instance of T</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Put<T>(string resource,
            params (string paramName, object value, ParamType type)[] parameters)
            where T : new() => ExecuteRequest<T>(
            resource,
            Method.PUT,
            request => request.AddParameters(parameters));

        /// <summary>
        /// Do Delete request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="parameters">List of request parameters</param>
        protected void Delete(string resource, params (string paramName, object paramValue)[] parameters) =>
            ExecuteRequest(resource, Method.DELETE, request => request.AddParameters(parameters));

        private void ExecuteRequest(string resource, Method method, Action<IRestRequest> requestConfig = null)
        {
            var request = new RestRequest(resource, method);
            requestConfig?.Invoke(request);
            var response = ResolveClient().Execute(request);
            if (response.IsSuccessful) return;
            throw ParseError(request.Resource, response.Content);
        }

        private T ExecuteRequest<T>(string resource, Method method, Action<IRestRequest> requestConfig = null)
            where T : new()
        {
            var request = new RestRequest(resource, method);
            requestConfig?.Invoke(request);
            var response = ResolveClient().Execute<T>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw ParseError(request.Resource, response.Content);
        }

        private static InvalidOperationException ParseError(string resource, string responseData)
        {
            ApiError errorInfo = null;
            if (responseData != null) errorInfo = SimpleJson.DeserializeObject<ApiError>(responseData);

            return new InvalidOperationException(
                $"Request {resource}: {(errorInfo != null ? $"{errorInfo.message}, details: {errorInfo.details}" : "no information")}");
        }
    }
}
