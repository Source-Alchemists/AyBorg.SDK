using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Buffers.Operations;

public record ConvertDescription : OperationDescription
{
    public ConvertDescription() : base() { }
    public ConvertDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}