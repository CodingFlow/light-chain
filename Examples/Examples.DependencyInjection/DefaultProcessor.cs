namespace Examples.DependencyInjection;

public class DefaultProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => true;

    public string Process(AnimalProcessorInput input) {
        return "it is an animal";
    }
}
