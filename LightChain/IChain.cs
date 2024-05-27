namespace LightChain;

/// <summary>
/// Orchestrator of the chain of responsibility.
/// </summary>
/// <typeparam name="TInput">Object containing inputs to processors.</typeparam>
public interface IChain<TInput, TOutput>
{
    /// <summary>
    /// Execute the chain of processors with given input. The input is passed to each processor's 
    /// <see cref="IProcessor{TInput, TOutput}.Condition(TInput)">Condition</see> and
    /// <see cref="IProcessor{TInput, TOutput}.Process(TInput)">Process</see> methods.
    /// </summary>
    /// <param name="input">Input passed to processors.</param>
    /// <returns>Output of processor able to handle input.</returns>
    TOutput Run(TInput input);
}