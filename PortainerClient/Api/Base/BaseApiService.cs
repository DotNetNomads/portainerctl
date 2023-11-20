using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
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
        private RestClient? _client;

        private RestClient ResolveClient()
        {
            if (_client != null)
                return _client;
            var config = ConfigModel.Load();
            var options = new RestClientOptions
            {
                BaseUrl = new Uri(config.Url ?? throw new InvalidOperationException("Invalid URL from config")),
                Authenticator =
                    new JwtAuthenticator(config.Token ?? throw new InvalidOperationException("Invalid Token from URL"))
            };
            _client ??= new RestClient(options);
            return _client;
        }

        /// <summary>
        /// Do Get request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="debug"></param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Response type</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Get<T>(string resource, bool debug = false,
            params (string paramName, object paramValue)[] parameters)
            where T : new() =>
            ExecuteRequest<T>(
                resource,
                Method.Get,
                request => request.AddParameters(parameters), debug);

        /// <summary>
        /// Do Post request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="debug">Print content of request and response</param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Parsed instance of T</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Post<T>(string resource, bool debug = false,
            params (string? paramName, object value, ParamType type)[] parameters)
            where T : new() => ExecuteRequest<T>(
            resource,
            Method.Post,
            request => request.AddParameters(parameters), debug);

        /// <summary>
        /// Do Put request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="debug">Debug request / response to Portainer</param>
        /// <param name="parameters">List of request parameters</param>
        /// <typeparam name="T">Parsed instance of T</typeparam>
        /// <returns>Parsed instance of T</returns>
        protected T Put<T>(string resource,
            bool debug,
            params (string? paramName, object value, ParamType type)[] parameters)
            where T : new() => ExecuteRequest<T>(
            resource,
            Method.Put,
            request => request.AddParameters(parameters), debug);

        /// <summary>
        /// Do Delete request and return parsed response
        /// </summary>
        /// <param name="resource">API resource</param>
        /// <param name="parameters">List of request parameters</param>
        protected void Delete(string resource, params (string paramName, object paramValue)[] parameters) =>
            ExecuteRequest(resource, Method.Get, request => request.AddParameters(parameters));

        private void ExecuteRequest(string resource, Method method, Action<RestRequest>? requestConfig = null)
        {
            var request = new TraceRequest(resource, method);
            requestConfig?.Invoke(request);
            var response = ResolveClient().Execute(request);
            if (response.IsSuccessful) return;
            Debug.Assert(response.Content != null, "response.Content != null");
            throw ParseError(request.Resource, response);
        }

        private T ExecuteRequest<T>(string resource, Method method, Action<RestRequest>? requestConfig = null,
            bool debug = false)
            where T : new()
        {
            var request = new TraceRequest(resource, method, debug);
            requestConfig?.Invoke(request);

            var response = ResolveClient().Execute<T>(request);
            if (!response.IsSuccessful)
            {
                Debug.Assert(response.Content != null, "response.Content != null");
                throw ParseError(request.Resource, response);
            }

            Debug.Assert(response.Data != null, "response.Data != null");
            return response.Data;
        }

        private static InvalidOperationException ParseError(string resource, RestResponse responseData)
        {
            ApiError? errorInfo = null;
            if (string.IsNullOrWhiteSpace(responseData.Content))
            {
                return new InvalidOperationException($"Request {resource}: {responseData.StatusDescription}");
            }

            if (responseData != null) errorInfo = JsonSerializer.Deserialize<ApiError>(responseData.Content);

            return new InvalidOperationException(
                $"Request {resource}: {(errorInfo != null ? $"{errorInfo.message}, details: {errorInfo.details}" : "no information")}");
        }

        /// <summary>
        /// Set default ACLs to resource
        /// </summary>
        /// <param name="memberships"></param>
        /// <param name="debug"></param>
        /// <param name="resourceId"></param>
        protected void SetAcl(IEnumerable<Membership> memberships, bool debug, int resourceId)
        {
            var resourceControlRequest = new ResourceControlRequest
            {
                Public = false,
                AdministratorsOnly = false,
                Users = Array.Empty<int>(),
                Teams = memberships.Select(m => m.TeamId).ToArray(),
            };
            Put<ResourceControl>($"resource_controls/{resourceId}", debug,
                ("", resourceControlRequest, ParamType.JsonBody));
        }
    }
}
