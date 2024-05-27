using LightChain;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.DependencyInjection;

internal static class ServiceRegistrations
{
    public static void AddServices(this IServiceCollection services) {
        services.AddTransient<IAnimalProcessor, CatsOnlyProcessor>();
        services.AddTransient<IAnimalProcessor, RedOnlyProcessor>();
        services.AddTransient<IAnimalProcessor, DefaultProcessor>();

        services.AddTransient<IChain<AnimalProcessorInput, string>, Chain<IAnimalProcessor, AnimalProcessorInput, string>>();

        services.AddTransient<Main>();
    }
}

