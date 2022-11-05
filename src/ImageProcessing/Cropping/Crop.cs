using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Cropping.Operations;
using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Cropping;

public class Crop : Operation<CropDescription, CropParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(CropParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(CropDescription)
                                                && o.InputType == parameters.Input!.GetType()).FirstOrDefault();

        if (description == null)
            throw new InvalidOperationException($"No crop found for {parameters.Input!.GetType()}.");

        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}