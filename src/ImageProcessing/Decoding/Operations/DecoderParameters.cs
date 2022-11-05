using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Decoding.Operations;

public record DecoderParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = 1 };
    
    public Stream? Input { get; set; }

    public Type OutputType { get; init; } = null!;
}