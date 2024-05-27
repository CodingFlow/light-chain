# Light Chain

[![Nuget](https://img.shields.io/nuget/v/LightChain)](https://www.nuget.org/packages/LightChain)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/CodingFlow/light-chain/pull-request.yml)
[![Nuget](https://img.shields.io/nuget/dt/LightChain)](https://www.nuget.org/packages/LightChain)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/CodingFlow)](https://github.com/sponsors/CodingFlow)

Lightweight library for implementing simplified version of [chain of responsibility](https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern) in C#.

The inspiration for this library came from figuring out a way to break up `if/else` chains into loosely coupled, separate units to improve maintainability through separation of concerns.

Example of `if/else` chain:

```c#
public class Main
{
    public string Run()
    {
        var result = ProcessAnimal("dog", "red", 100);

        return result;
    }

    private string ProcessAnimal(string animal, string color, int height)
    {
        string result;

        if (animal == "cat") {
            result = "animal is a cat!";
        } else if (color == "red" {
            result = "animal is red!";
        } else {
            result = "it is an animal";
        }

        return result;
    }
}
```

As we can see from the example, all the blocks of conditions and processing are stuck together in the `if/else` construct within the same class. The `if/else` construct itself adds a bit of noise. It is difficult to view and change high level concerns only, such as order of each case.

# Getting Started

## Installation

Add the library via NuGet to the project(s) that you want to use Light Chain:

- Either via Project > Manage NuGet Packages... / Browse / search for
  light-chain / Install
- Or by running a command in the Package Manager Console

```c#
Install-Package LightChain
```

## Usage

Create a type to hold the input:

```c#
public class AnimalProcessorInput
{
    public required string Animal { get; set; }
    public required string Color { get; set; }
    public required int Height { get; set; }
}
```

Derive a processor interface from `IProcessor`:

```c#
using LightChain;

public interface IAnimalProcessor : IProcessor<AnimalProcessorInput, string>
{
}
```

Create each of the processors derived from the interface you just created:

```c#
public class CatsOnlyProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => input.Animal == "cat";

    public string Process(AnimalProcessorInput input)
    {
        return "animal is a cat!";
    }
}
```

```c#
public class RedOnlyProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => input.Color == "red";

    public string Process(AnimalProcessorInput input)
    {
        return "animal is red!";
    }
}
```

```c#
public class DefaultProcessor : IAnimalProcessor
{
    public bool Condition(AnimalProcessorInput input) => true;

    public string Process(AnimalProcessorInput input)
    {
        return "it is an animal";
    }
}
```

Now you can create the chain and use it with the input:

```c#
using LightChain;

public class Main
{
    public string Run()
    {
        var result = ProcessAnimal("dog", "red", 100);

        return result;
    }

    private string ProcessAnimal(string animal, string color, int height)
    {
        var processors = new List<IAnimalProcessor>
        {
            new CatsOnlyProcessor(),
            new RedOnlyProcessor(),
            new DefaultProcessor(),
        };
        var animalProcessor = new Chain<IAnimalProcessor, AnimalProcessorInput, string>(processors);

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
```

Be aware that the order of the processors in the list matters: the first processor whose condition returns `true` will handle returning the output.

### Dependency Injection

Using a dependency injection framework, the processor list and chain instance can be defined separately from the main class via the dependency injection framework.

Using Microsoft.Extensions.DependencyInjection the `Main` class can be refactored:

```c#
using LightChain;

public class Main
{
    private readonly IChain<AnimalProcessorInput, string> animalProcessor;

    public Main(IChain<AnimalProcessorInput, string> animalProcessor)
    {
        this.animalProcessor = animalProcessor;
    }

    public string Run()
    {
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
```

The main, processors, and chain classes can be registered with the DI framework:

```c#
using Microsoft.Extensions.DependencyInjection;
using LightChain;

internal static class ServiceRegistrations
{
    public static void AddServices(this IServiceCollection services)
    {
        // processor registration order matters
        services.AddTransient<IAnimalProcessor, CatsOnlyProcessor>();
        services.AddTransient<IAnimalProcessor, RedOnlyProcessor>();
        services.AddTransient<IAnimalProcessor, DefaultProcessor>();

        services.AddTransient<IChain<AnimalProcessorInput, string>, Chain<IAnimalProcessor, AnimalProcessorInput, string>>();

        services.AddTransient<Main>();
    }
}
```

The end result is improved separation of concerns such that the main class no longer needs to change due to any modifications related to processors:

- Adding or removing processors from the chain.
- Reordering processors in the chain.
- Changing implementation details of a processor.

Also, each processor is completely separate from each other and the chain.
