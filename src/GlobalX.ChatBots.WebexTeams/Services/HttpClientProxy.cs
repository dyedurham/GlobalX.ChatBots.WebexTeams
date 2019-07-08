using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GlobalX.ChatBots.WebexTeams.Services
{
    public class HttpClientProxy : IHttpClientProxy
    {
        private const string BaseUrl = "https://api.ciscospark.com/v1";

        private readonly HttpClient _httpClient;

        public HttpClientProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // TODO put token into config
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");
        }

        public async Task<string> GetAsync(string path, string body = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{BaseUrl}{path}"),
                Content = body != null ? new StringContent(body, Encoding.UTF8, "application/json") : null
            };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseBody;
        }

        public async Task<string> PostAsync(string path, string body = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BaseUrl}{path}"),
                Content = body != null ? new StringContent(body, Encoding.UTF8, "application/json") : null
            };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseBody;
        }
    }
}
