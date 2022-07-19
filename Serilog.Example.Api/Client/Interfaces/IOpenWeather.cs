using Refit;
using Serilog.Example.Api.Models;

namespace Serilog.Example.Api.Client.Interfaces;

public interface IOpenWeather
{
    [Get("/weather")]
    Task<ApiResponse<WeatherResponse>> GetWeather(OpenWeatherRequest openWeatherRequest);
}