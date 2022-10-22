using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Mirroring.Operations;

public record MirrorParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
    
    public IReadOnlyPixelBuffer? Input { get; set; }
    
    public MirrorMode MirrorMode { get; init; } = MirrorMode.Horizontal;
}