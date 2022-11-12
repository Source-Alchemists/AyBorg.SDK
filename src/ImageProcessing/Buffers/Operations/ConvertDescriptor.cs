using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Buffers.Operations;

public record ConvertDescription : OperationDescription
{
    public ConvertDescription() : base() { }
    public ConvertDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}