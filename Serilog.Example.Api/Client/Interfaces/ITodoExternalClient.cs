using Refit;
using Serilog.Example.Api.Models;

namespace Serilog.Example.Api.Client.Interfaces;

public interface ITodoExternalClient
{
    [Post("/todos")]
    Task<ApiResponse<Todo>> CreateTodo([Body] Todo todo);

    [Put("/todos/{id}")]
    Task<ApiResponse<Todo>> UpdateTodo(int id, [Body] Todo todo);

    [Get("/todos")]
    Task<ApiResponse<Todo>> Get();
}