using System.Reflection;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Serilog.Example.Api.Configuration;

public static class SerilogExtension
{
    public static void AddSerilogApi(IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithCorrelationId()
                    .Enrich.WithProperty("Environment", environment)
                    .Enrich.WithProperty("ApplicationName", $"API Serilog - {environment}")
                    .Filter.ByExcluding(c => c.Properties.Any(p => p.ToString().Contains("/swagger")))
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
                    .CreateLogger();
    }

    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat =
                $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}" +
                $"-{environment?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}"
        };
    }
}