namespace Examples.DependencyInjection;

public class CatsOnlyProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => input.Animal == "cat";

    public string Process(AnimalProcessorInput input) {
        return "animal is a cat!";
    }
}
