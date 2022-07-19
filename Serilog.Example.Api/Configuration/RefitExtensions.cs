using Refit;
using Serilog.Example.Api.Client.Interfaces;

namespace Serilog.Example.Api.Configuration;

public static class RefitExtensions
{
    public static void RefitRegister(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<ITodoExternalClient>()
               .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(configuration["ExternalClient:Uri"]));
        
        services.AddRefitClient<IOpenWeather>()
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(configuration["OpenWeather:Uri"]));
    }
}