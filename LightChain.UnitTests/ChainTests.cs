using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace LightChain.UnitTests;

public class ChainTests
{
    private List<IProcessor<Input, string>> processors;
    private Chain<IProcessor<Input, string>, Input, string> chain;

    [SetUp]
    public void Setup() {
        processors = new List<IProcessor<Input, string>> {
            Substitute.For<IProcessor<Input, string>>(),
            Substitute.For<IProcessor<Input, string>>()
        };
        chain = new Chain<IProcessor<Input, string>, Input, string>(processors);
    }

    [Test]
    public void Processor_First_Called_Successfully() {
        processors.First().Condition(null).ReturnsForAnyArgs(true);
        processors.First().Process(null).ReturnsForAnyArgs("some value");

        var input = new Input
        {
            Value = "some value"
        };

        var result = chain.Run(input);

        Assert.That(result, Is.EqualTo("some value"));

        processors.First().Received(1).Condition(input);
        processors.First().Received(1).Process(input);
    }

    [Test]
    public void Processor_Second_Called_Successfully() {
        processors.First().Condition(null).ReturnsForAnyArgs(false);
        processors.First().Process(null).ReturnsForAnyArgs("first value");

        processors.ElementAt(1).Condition(null).ReturnsForAnyArgs(true);
        processors.ElementAt(1).Process(null).ReturnsForAnyArgs("second value");

        var input = new Input
        {
            Value = "some value"
        };

        var result = chain.Run(input);

        Assert.That(result, Is.EqualTo("second value"));

        processors.First().Received(1).Condition(input);
        processors.First().DidNotReceive().Process(Arg.Any<Input>());

        processors.ElementAt(1).Received(1).Condition(input);
        processors.ElementAt(1).Received(1).Process(input);
    }

    public class Input
    {
        public string Value { get; set; }
    }
}