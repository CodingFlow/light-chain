namespace Examples.DependencyInjection;

public class RedOnlyProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => input.Color == "red";

    public string Process(AnimalProcessorInput input) {
        return "animal is red!";
    }
}
