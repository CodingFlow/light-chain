namespace LightChain;

/// <summary>
/// Base processor interface used by the library. All processors must implement this interface.
/// It is recommended to create your own interfaces derived from this interface instead of using
/// this interface directly.
/// </summary>
/// <typeparam name="TInput">Input passed to both <see cref="Condition(TInput)">Condition</see> and <see cref="Process(TInput)">Process</see>.</typeparam>
/// <typeparam name="TOutput">Processor output.</typeparam>
public interface IProcessor<TInput, TOutput>
{
    /// <summary>
    /// Condition that determines whether processor should execute.
    /// </summary>
    /// <param name="input">Input used by the condition to determine if processor should execute.</param>
    /// <returns>Boolean indicating if condition should execute.</returns>
    public bool Condition(TInput input);

    /// <summary>
    /// Executes the processor.
    /// </summary>
    /// <param name="input">Input used by the processor to create its output.</param>
    /// <returns>Processor output.</returns>
    public TOutput Process(TInput input);
}
