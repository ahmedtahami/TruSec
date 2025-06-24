using Microsoft.AspNetCore.Mvc;
using WeatherApp.DTOs;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private const string API_URL = "http://api.weatherapi.com/v1/current.json?key=6d76e5ed12f24b1092092128230709&q=Karachi&aqi=no";
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITempratureVitalsProcessor _tempratureVitalsProcessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITempratureVitalsProcessor tempratureVitalsProcessor)
        {
            _logger = logger;
            _tempratureVitalsProcessor = tempratureVitalsProcessor;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("GetWeather")]
        public async Task<IActionResult> GetWeatherAsync()
        {
            var httpClient = new HttpClient();
            var req = await httpClient.GetAsync(API_URL);
            var res = await req.Content.ReadFromJsonAsync<WeatherAPIResponse>();

            if (req.StatusCode == System.Net.HttpStatusCode.OK && res is not null)
            {
                double heatIndex = _tempratureVitalsProcessor.CalculateHeatIndex(res.current.temp_c, res.current.humidity);
                return Ok(res);
            }
            return BadRequest("Unable to fetch");
        }
    }
}