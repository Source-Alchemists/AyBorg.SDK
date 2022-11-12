using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Resizing.Operations;

public record ResizerDescription : OperationDescription
{
    public ResizerDescription() : base() { }
    public ResizerDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}