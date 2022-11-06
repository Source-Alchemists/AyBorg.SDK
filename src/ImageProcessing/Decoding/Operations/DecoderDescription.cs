using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Decoding.Operations;

public record DecoderDescription : OperationDescription
{
    public DecoderDescription() : base() { }
    public DecoderDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}