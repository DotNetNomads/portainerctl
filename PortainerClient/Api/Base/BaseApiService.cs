using System;
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
            foreach (var (paramName, paramValue) in parameters) request.AddParameter(paramName, paramValue);
            var response = ResolveClient().Execute<T>(request);
            if (!response.IsSuccessful)
            {
                throw new InvalidOperationException($"Request {resource} error: {response.Content}");
            }

            return response.Data;
        }
    }
}