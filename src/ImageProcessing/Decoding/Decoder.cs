using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Decoding.Operations;
using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Decoding;

public class Decoder : Operation<DecoderDescription, DecoderParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(DecoderParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(DecoderDescription)  
                                            && o.OutputType == parameters.OutputType).FirstOrDefault();
        if (description == null)
            throw new InvalidOperationException($"No decoder found for {parameters.Input!.GetType()} to {parameters.OutputType}.");

        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}