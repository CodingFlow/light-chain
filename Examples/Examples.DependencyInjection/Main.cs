using LightChain;

namespace Examples.DependencyInjection;

public class Main
{
    private readonly IChain<AnimalProcessorInput, string> animalProcessor;

    public Main(IChain<AnimalProcessorInput, string> animalProcessor) {
        this.animalProcessor = animalProcessor;
    }

    public string Run() {
        var input = new AnimalProcessorInput
        {
            Animal = "dog",
            Color = "red",
            Height = 100,
        };

        var result = animalProcessor.Run(input);

        return result;
    }
}
