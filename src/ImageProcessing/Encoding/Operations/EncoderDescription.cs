using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Encoding.Operations;

public record EncoderDescription : OperationDescription
{
    public EncoderDescription() : base() { }
    public EncoderDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}