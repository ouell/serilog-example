namespace Serilog.Example.Api.Models;

public record struct Todo(int Id, string Title, string Body, int UserId);