using Microsoft.AspNetCore.Mvc;
using RootServiceNamespace;
using SampleService.Service;
using System.Net.Http;

namespace SampleService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRootServiceClient _rootServiceClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            /*, IHttpClientFactory httpClientFactory,*/ IRootServiceClient rootServiceClient)
        {
            _logger = logger;
            /*_httpClientFactory = httpClientFactory;
            RootServiceClient _rootServiceClient = new RootServiceClient("http://localhost:5202", _httpClientFactory.CreateClient("RootServiceClient"));*/

            _rootServiceClient = rootServiceClient;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            
            return await _rootServiceClient.Get();

            /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}