using Common.Fixer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Fixer
{
    public class ApiClient : IDisposable
    {
        private const string APIKEY = "0d584ee9f951f6327002973520e607b8";
        private const string APIURI = "http://data.fixer.io/api/";

        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<FixerResponse<LatestRatesResponse>> GetHistorical(DateTime date, params string[] symbols)
        {
            var uriBuilder = new UriBuilder(APIURI + date.ToString("yyyy-MM-dd"))
                .AddQueryParam("access_key", APIKEY)
                .AddQueryParam("symbols", string.Join(',', symbols));

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

            return await Send<LatestRatesResponse>(request);
        }

        public async Task<FixerResponse<LatestRatesResponse>> GetLatest(params string[] symbols)
        {
            var uriBuilder = new UriBuilder(APIURI + "latest")
                .AddQueryParam("access_key", APIKEY)
                .AddQueryParam("symbols", string.Join(',', symbols));

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

            return await Send<LatestRatesResponse>(request);
        }

        private async Task<FixerResponse<T>> Send<T>(HttpRequestMessage request) where T : IResponse
        {
            try
            {
                var httpResponse = await httpClient.SendAsync(request);
                var content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<Response>(content);
                    if (response.Success)
                    {
                        return new FixerResponse<T>
                        {
                            Success = true,
                            Content = JsonConvert.DeserializeObject<T>(content)
                        };
                    }
                }

                return new FixerResponse<T>
                {
                    Success = false,
                    Error = JsonConvert.DeserializeObject<ErrorResponse>(content).Error
                };

            }
            catch
            {
                // exception handling
                throw;
            }
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
