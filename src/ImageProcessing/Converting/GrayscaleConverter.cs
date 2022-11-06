using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Converting.Operations;
using Autodroid.SDK.ImageProcessing.Operations;

namespace Autodroid.SDK.ImageProcessing.Converting;

public class GrayscaleConverter : Operation<GrayscaleConverterDescription, GrayscaleConverterParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(GrayscaleConverterParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(GrayscaleConverterDescription) 
                                            && o.InputType == parameters.Input.GetType()
                                            && o.OutputType == parameters.OutputType).FirstOrDefault();
        if (description == null)
            throw new InvalidOperationException($"No grayscale converter found for {parameters.Input.GetType()} to {parameters.OutputType}.");
        
        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}