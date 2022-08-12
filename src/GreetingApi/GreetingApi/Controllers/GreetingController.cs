using System;
using System.Net.Http;
using System.Threading.Tasks;
using GreetingGrpcService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace GreetingApi.Controllers
{
    public class GreetingController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ExternalServicesHostsOptions externalServicesHostsOptions;

        public GreetingController(IHttpClientFactory httpClientFactory, ExternalServicesHostsOptions externalServicesHostsOptions)
        {
            this.httpClientFactory = httpClientFactory;
            this.externalServicesHostsOptions = externalServicesHostsOptions;
        }

        [HttpGet]
        [Route("config")]
        public IActionResult ShowConfig()
        {
            return Ok(externalServicesHostsOptions);
        }

        [HttpGet]
        [Route("rest")]
        public async Task<IActionResult> HelloRest([FromQuery] string name)
        {
            try
            {
                var client = httpClientFactory.CreateClient("rest_client");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/greeting/hello?name={name}");
                var result = await client.SendAsync(request);
                if (!result.IsSuccessStatusCode) return BadRequest("HTTP call failed");
                var greeting = await result.Content.ReadAsStringAsync();
                return Ok(greeting);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed with exception: " + ex.ToString());
            }
        }

        [HttpGet]
        [Route("grpc")]
        public async Task<IActionResult> HelloGrpc([FromQuery] string name)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress(externalServicesHostsOptions.gRPC);
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(
                                  new HelloRequest { Name = name });

                return Ok(reply.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed with exception: " + ex.ToString());
            }
        }
    }
}
