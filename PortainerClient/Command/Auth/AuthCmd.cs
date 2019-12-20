using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api.Model;
using PortainerClient.Config;
using PortainerClient.Helpers;
using RestSharp;

namespace PortainerClient.Command
{
    [Command(Name = "auth", Description = "Authorize this client in Portainer (required before using)")]
    public class AuthCmd : ICommand
    {
        [Option("--url", "Portainer url", CommandOptionType.SingleValue)]
        [Required]
        public string PortainerUrl { get; set; }

        [Option("--user", "User name", CommandOptionType.SingleValue)]
        [Required]
        public string User { get; set; }

        [Option("--password", "Password", CommandOptionType.SingleValue)]
        [Required]
        public string Password { get; set; }

        public AuthCmd()
        {
        }

        private static void Authorize(string url, string user, string password)
        {
            var client = new RestClient(url + "/api");
            IRestResponse<TokenInfo> tokenInfoResponse;
            try
            {
                var request = new RestRequest("auth", Method.POST, DataFormat.Json);
                request.AddJsonBody(new AuthInfo {Username = user, Password = password});
                tokenInfoResponse = client.Execute<TokenInfo>(request);
            }
            catch (Exception e)
            {
                throw new Exception($"Authorization error. Detailed message: {e.Message}");
            }

            if (tokenInfoResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Authorization error. Detailed message: {tokenInfoResponse.Content}");
            }

            var configModel = new ConfigModel
            {
                Url = url + "/api",
                Token = tokenInfoResponse.Data.Jwt
            };
            configModel.Save();
        }

        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            try
            {
                Authorize(PortainerUrl, User, Password);
                console.WriteLine("Authorized. Now you can execute any commands");
                return 0;
            }
            catch (Exception ex)
            {
                console.ForegroundColor = ConsoleColor.Red;
                console.Error.WriteLine($"Login error: {ex.Message}");
                return 1;
            }
        }
    }
}