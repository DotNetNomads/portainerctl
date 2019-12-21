using System;
using PortainerClient.Api.Model;
using PortainerClient.Helpers;
using RestSharp;
using RestSharp.Authenticators;

namespace PortainerClient.Api
{
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

        protected T Get<T>(string resource, params (string paramName, object paramValue)[] parameters) where T : new()
        {
            var request = new RestRequest(resource, Method.GET);
            request.AddParameters(parameters);
            var response = ResolveClient().Execute<T>(request);
            if (response.IsSuccessful) return response.Data;
            throw ParseError(resource, response.Content);
        }

        protected void Delete(string resource, params (string paramName, object paramValue)[] parameters)
        {
            var request = new RestRequest(resource, Method.DELETE);
            request.AddParameters(parameters);
            var response = ResolveClient().Execute(request);
            if (response.IsSuccessful) return;
            throw ParseError(resource, response.Content);
        }

        private static InvalidOperationException ParseError(string resource, string responseData)
        {
            ApiError errorInfo = null;
            if (responseData != null)
            {
                errorInfo = SimpleJson.DeserializeObject<ApiError>(responseData);
            }

            return new InvalidOperationException(
                $"Request {resource}: {(errorInfo != null ? $"{errorInfo.message}, details: {errorInfo.details}" : "no information")}");
        }
    }
}
