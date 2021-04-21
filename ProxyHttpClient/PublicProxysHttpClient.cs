using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyHttpClient
{
    public class PublicProxysHttpClient
    {
        private readonly HttpClient _client;
        public PublicProxysHttpClient(HttpClient client)
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