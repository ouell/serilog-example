using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Serilog.Example.Api.Client.Interfaces;
using Serilog.Example.Api.Models;

namespace Serilog.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenWeatherController : ControllerBase
{
    private readonly IOpenWeather _openWeather;
    private readonly ILogger _logger;

    public OpenWeatherController(IOpenWeather openWeather, ILogger logger)
    {
        _openWeather = openWeather;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get(string city, string apiKey)
    {
        var request = new OpenWeatherRequest(city, apiKey);
        var result = await _openWeather.GetWeather(request);
        
        LogContext.PushProperty("TestePropriedade", "Teste da propriedade");
        if (result.IsSuccessStatusCode)
        {
            _logger.Information("Successfully retrieved weather for {City} " +
                                "with result: {@Result}", city, result.Content);
            return Ok(result.Content);
        }
        
        _logger.Error("Failed to retrieve weather for {City} error: {@Error}", city, result.Error);
        return BadRequest(result.Error.Content);
    }

}