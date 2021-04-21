using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyHttpClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private readonly ProxysHttpClient _proxysHttp;
        private readonly PublicProxysHttpClient _publicProxy;
        public DevController(
            ProxysHttpClient proxysHttp,
            PublicProxysHttpClient publicProxy)
        {
            _proxysHttp = proxysHttp;
            _publicProxy = publicProxy;
        }

        // GET: api/<DevController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _proxysHttp.GetResponse("http://httpbin.org/get");
            var json = await response.Content.ReadAsStringAsync();
            return Ok(json);
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetPublic()
        {
            var response = await _publicProxy.GetResponse("http://httpbin.org/get");
            var json = await response.Content.ReadAsStringAsync();
            return Ok(json);
        }
    }
}
