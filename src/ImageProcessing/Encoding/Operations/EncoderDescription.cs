using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Encoding.Operations;

public record EncoderDescription : OperationDescription
{
    public EncoderDescription() : base() { }
    public EncoderDescription(Type inputType, Type outputType, Delegate operation) 
        : base(inputType, outputType, operation)
    {
    }
}