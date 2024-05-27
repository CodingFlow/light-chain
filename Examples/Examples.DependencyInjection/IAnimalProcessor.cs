using LightChain;

namespace Examples.DependencyInjection;

public interface IAnimalProcessor : IProcessor<AnimalProcessorInput, string>
{
}
