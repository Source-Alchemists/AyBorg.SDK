using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Encoding.Operations;

public record EncoderDescription : OperationDescription
{
    public EncoderDescription() : base() { }
    public EncoderDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}