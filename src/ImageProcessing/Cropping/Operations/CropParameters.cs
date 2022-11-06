using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Shapes;
using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Cropping.Operations;

public record CropParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };

    public IReadOnlyPixelBuffer? Input { get; init; }

    public Rectangle Rectangle { get; init; }
}