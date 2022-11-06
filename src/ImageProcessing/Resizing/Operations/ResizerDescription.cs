using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Resizing.Operations;

public record ResizerDescription : OperationDescription
{
    public ResizerDescription() : base() { }
    public ResizerDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}