using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.DependencyInjection;

public class Startup
{
    public string Run() {
        Main main;

        using (
            var host = CreateHostBuilder().Build()
        ) {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            main = services.GetRequiredService<Main>();
        }

        var result = main.Run();

        return result;
    }

    private static IHostBuilder CreateHostBuilder() {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => {
                services.AddServices();
            });
    }
}
