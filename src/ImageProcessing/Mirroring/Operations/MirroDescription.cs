using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Mirroring.Operations;
public record MirrorDescription : OperationDescription
{
    public MirrorDescription() : base() { }
    public MirrorDescription(Type inputType, Type outputType, Delegate operation)
        : base(inputType, outputType, operation)
    {
    }
}
