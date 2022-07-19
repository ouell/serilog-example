using Microsoft.AspNetCore.Mvc;
using Serilog.Example.Api.Client.Interfaces;
using Serilog.Example.Api.Models;

namespace Serilog.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoTesteController : ControllerBase
{
    private readonly ITodoExternalClient _client;
    private readonly ILogger _logger;

    public TodoTesteController(ITodoExternalClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateTodo([FromBody] Todo body)
    {
        _logger.Information("Requisição para criação recebida: {@Body}", body);

        var result = await _client.CreateTodo(body);
        if (result.IsSuccessStatusCode)
        {
            _logger.Information("Criação foi realizada com sucesso: {@Result}", result);

            return Ok(result.Content);
        }

        _logger.Error("Requisição para criação dos dados {@Body} falhou: {@Result}", body, result);
        return BadRequest();
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateTodo([FromBody] Todo body)
    {
        _logger.Information("Requisição de update recebida com o body: {@Todo}", body);

        var result = await _client.UpdateTodo(body.Id, body);
        if (result.IsSuccessStatusCode)
        {
            _logger.Information("Requisição para o cliente externo foi bem sucedida: {@Result}", result);
            return Ok(result.Content);
        }

        _logger.Error("Requisição de update para o id {Id} falhou: {@Result}", body.Id, result);
        return BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        _logger.Information("Requisição de listagem recebida");

        var result = await _client.Get();
        if (result.IsSuccessStatusCode)
        {
            _logger.Information("Consulta realizada com sucesso: {@Result}", result);
            return Ok(result.Content);
        }

        _logger.Error("Consulta falhou: {@Result}", result);
        return BadRequest();
    }
}