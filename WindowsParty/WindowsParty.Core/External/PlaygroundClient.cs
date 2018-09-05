using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;
using Newtonsoft.Json;
using System.Configuration;

namespace WindowsParty.Core.External
{
    public class PlaygroundClient : IPlaygroundClient
    {
        private readonly string _baseUri;

        public PlaygroundClient()
        {
            _baseUri = ConfigurationManager.AppSettings["PlaygroundUri"];
        }

        public async Task<TokenResponse> GetToken(TokenRequest tokenRequest)
        {
            var client = new RestClient($"{_baseUri}/tokens");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=password&username={tokenRequest.UserName}&password={tokenRequest.Password}",
                ParameterType.RequestBody);
            var response = await client.ExecuteTaskAsync<TokenResponse>(request);

            return response.Data;
        }

        public async Task<ServerListResponse> GetServerList(ServerListRequest serverListRequest)
        {
            var client = new RestClient($"{_baseUri}/servers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + serverListRequest.Token);
            var response = await client.ExecuteTaskAsync<IList<ServerInfo>>(request); // does not work with array:/
            var data = JsonConvert.DeserializeObject<IList<ServerInfo>>(response.Content);

            return new ServerListResponse(data);
        }
    }
}
