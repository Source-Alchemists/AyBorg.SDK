using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Converting.Operations;
using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Converting;

public class GrayscaleConverter : Operation<GrayscaleConverterDescription, GrayscaleConverterParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(GrayscaleConverterParameters parameters)
    {
        if(parameters.Input == null)
            throw new ArgumentNullException(nameof(parameters.Input));

        var description = Descriptions.Where(o => o.GetType() == typeof(GrayscaleConverterDescription) 
                                            && o.InputType == parameters.Input.GetType()
                                            && o.OutputType == parameters.OutputType).FirstOrDefault();
        if (description == null)
            throw new InvalidOperationException($"No grayscale converter found for {parameters.Input.GetType()} to {parameters.OutputType}.");
        
        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}