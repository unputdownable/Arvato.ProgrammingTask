using Common.API.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task2.Web.Services
{
    public class RatesApi
    {
        private readonly HttpClient httpClient;

        public RatesApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> Convert(string from, string to, decimal amount, DateTime? date = null)
        {
            var uri = $"convert?from={from}&to={to}&amount={amount}";
            if (date is not null) uri += $"&date={(DateTime)date:yyyy-MM-dd}";

            var response = await httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ConvertResponse>(content), Formatting.Indented);
            else
                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<ErrorResponse>(content), Formatting.Indented);
        }
    }
}
