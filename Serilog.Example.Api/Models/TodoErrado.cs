namespace Serilog.Example.Api.Models;

public record struct TodoErrado(Guid Id, string Title, string Body, bool Completed);