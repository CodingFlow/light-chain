namespace LightChain;

/// <inheritdoc cref="IChain{TInput, TOutput}" />
/// <typeparam name="TProcessor">Interface for processors to be used. Recommended to be derived from <see cref="IProcessor{T}"/>.</typeparam>
public class Chain<TProcessor, TInput, TOutput> : IChain<TInput, TOutput> where TProcessor : IProcessor<TInput, TOutput>
{
    private readonly IEnumerable<TProcessor> processors;

    public Chain(IEnumerable<TProcessor> processors) {
        this.processors = processors;
    }

    public TOutput Run(TInput input) {
        return processors
            .First(p => p.Condition(input))
            .Process(input);
    }
}