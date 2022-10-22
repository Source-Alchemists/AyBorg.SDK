using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Shapes;
using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Cropping.Operations;

public record CropParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };

    public IReadOnlyPixelBuffer? Input { get; init; }

    public Rectangle Rectangle { get; init; }
}