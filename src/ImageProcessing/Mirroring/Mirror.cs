using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Mirroring.Operations;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Mirroring;

public class Mirror : Operation<MirrorDescription, MirrorParameters, IPixelBuffer>
{
    public override IPixelBuffer Execute(MirrorParameters parameters)
    {
        var description = Descriptions.Where(o => o.GetType() == typeof(MirrorDescription)
                                                && o.InputType == parameters.Input!.GetType()).FirstOrDefault();

        if (description == null)
            throw new InvalidOperationException($"No mirror found for {parameters.Input!.GetType()}.");

        return (IPixelBuffer)description.Operation!.DynamicInvoke(parameters)!;
    }
}