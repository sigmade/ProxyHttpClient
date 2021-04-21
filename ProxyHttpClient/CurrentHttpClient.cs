using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyHttpClient
{
    public class CurrentHttpClient
    {
        private readonly HttpClient _client;
        public CurrentHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            var response = await _client.GetAsync(url);

            return response;
        }
    }
}