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
        private readonly CurrentHttpClient _current;
        public DevController(
            ProxysHttpClient proxysHttp,
            CurrentHttpClient current)
        {
            _proxysHttp = proxysHttp;
            _current = current;
        }

        // GET: api/<DevController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Random random = new();

            int value = random.Next(1, 3);

            //var response = value switch
            //{
            //    1 => await _proxysHttp.GetResponse("http://httpbin.org/get"),
            //    2 => await _current.GetResponse("http://httpbin.org/get"),
            //    _ => await _proxysHttp.GetResponse("http://httpbin.org/get"),
            //};

            var response = await _proxysHttp.GetResponse("http://httpbin.org/get");
            var json = await response.Content.ReadAsStringAsync();
            return Ok(json);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var response = await _current.GetResponse("http://httpbin.org/get");
            var json = await response.Content.ReadAsStringAsync();
            return Ok(json);
        }
    }
}
