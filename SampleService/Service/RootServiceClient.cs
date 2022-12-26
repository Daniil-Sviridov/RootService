using RootServiceNamespace;

namespace SampleService.Service
{
    public interface IRootServiceClient
    {
        public RootServiceNamespace.RootServiceClient RootServiceClient { get; }
        public Task<IEnumerable<WeatherForecast>> Get();
    }
    public class RootServiceClient : IRootServiceClient
    {
        private readonly ILogger<RootServiceClient> _logger;
        private readonly RootServiceNamespace.RootServiceClient _rootServiceClient;

        RootServiceNamespace.RootServiceClient IRootServiceClient.RootServiceClient => _rootServiceClient;

        public RootServiceClient(HttpClient httpClient, ILogger<RootServiceClient> logger)
        {
            _logger = logger;
            _rootServiceClient = new RootServiceNamespace.RootServiceClient("http://localhost:5202", httpClient);
        }

        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _rootServiceClient.GetWeatherForecastAsync();
        }
    }
}
