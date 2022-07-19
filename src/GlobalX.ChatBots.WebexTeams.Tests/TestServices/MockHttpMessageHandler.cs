using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalX.ChatBots.WebexTeams.Tests.TestServices
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private string _response;
        private HttpStatusCode _statusCode;

        public List<string> Inputs { get; private set; }

        public MockHttpMessageHandler()
        {
            _response = null;
            _statusCode = HttpStatusCode.NotImplemented;

            Inputs = new List<string>();
        }

        public void SetResponse(HttpStatusCode statusCode)
        {
            SetResponse(statusCode, _response);
        }

        public void SetResponse(HttpStatusCode statusCode, string response)
        {
            _statusCode = statusCode;
            _response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var content = request.Content != null
                ? await request.Content.ReadAsStringAsync()
                : request.RequestUri.ToString();
            Inputs.Add(content);

            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }
}
