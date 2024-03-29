using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using PortainerClient.Api.Model;
using PortainerClient.Config;
using RestSharp;

namespace PortainerClient.Command.Auth
{
    /// <summary>
    /// CMD command for authorization
    /// </summary>
    [Command(Name = "auth", Description = "Authorize this client in Portainer (required before using)")]
    public class AuthCmd : ICommand
    {
        /// <summary>
        /// Portainer instance URL
        /// </summary>
        [Option("--url", "Portainer url", CommandOptionType.SingleValue)]
        [Required]
        public string PortainerUrl { get; set; } = null!;

        /// <summary>
        /// User's name
        /// </summary>
        [Option("--user", "User name", CommandOptionType.SingleValue)]
        [Required]
        public string User { get; set; } = null!;

        /// <summary>
        /// Password
        /// </summary>
        [Option("--password", "Password", CommandOptionType.SingleValue)]
        [Required]
        public string Password { get; set; } = null!;

        private static void Authorize(string url, string user, string password)
        {
            var client = new RestClient(url + "/api");
            RestResponse<TokenInfo> tokenInfoResponse;
            try
            {
                var request = new RestRequest("auth", Method.Post);
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

            Debug.Assert(tokenInfoResponse.Data != null, "tokenInfoResponse.Data != null");
            var configModel = new ConfigModel
            {
                Url = url + "/api",
                Token = tokenInfoResponse.Data.Jwt
            };
            configModel.Save();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(tokenInfoResponse.Data.Jwt);
            var userId = jwtSecurityToken.Payload["id"].ToString();

            var workspace = new WorkspaceInfoModel
            {
                UserId = int.Parse(userId)
            };
            workspace.Memberships = new PortainerApiService().GetMemberships(workspace.UserId);
            workspace.Endpoints = new EndpointsApiService().GetEndpoints();
            workspace.Save();
        }

        /// <inheritdoc />
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
