using System.Collections.ObjectModel;

namespace AyBorg.SDK.Common.Ports;

public class NumericCollectionPort : ValuePortGeneric<NumericCollectionPort, ReadOnlyCollection<double>>
{
    public NumericCollectionPort(string name, PortDirection direction, ReadOnlyCollection<double> value) : base(name, direction, value)
    {
    }

    public NumericCollectionPort(NumericCollectionPort other) : base(other)
    {
        Value = new ReadOnlyCollection<double>(other.Value);
    }

    public override PortBrand Brand => PortBrand.NumericCollection;
}
