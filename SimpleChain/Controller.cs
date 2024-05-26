namespace SimpleChain;

/// <inheritdoc cref="IController{TInput, TOutput}" />
/// <typeparam name="TProcessor">Interface for processors to be used. Recommended to be derived from <see cref="IProcessor{T}"/>.</typeparam>
public class Controller<TProcessor, TInput, TOutput> : IController<TInput, TOutput> where TProcessor : IProcessor<TInput, TOutput>
{
    private readonly IEnumerable<TProcessor> processors;

    public Controller(IEnumerable<TProcessor> processors) {
        this.processors = processors;
    }

    public TOutput Run(TInput input) {
        return processors
            .First(p => p.Condition(input))
            .Process(input);
    }
}