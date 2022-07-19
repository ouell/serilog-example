namespace Serilog.Example.Api.Configuration;

public static class ApiExtensions
{
    public static void ApiConfig(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}