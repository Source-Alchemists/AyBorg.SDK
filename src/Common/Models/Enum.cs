namespace AyBorg.SDK.Common.Models;

public sealed record Enum
{
    public string? Name { get; set; }
    public string[]? Names { get; set; }

    public bool Equals(Enum? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.Name == null || Name == null) return false;
        if (Name != other.Name) return false;
        if (Names == null || other.Names == null) return false;

        foreach (string n in Names)
        {
            if (!other.Names.Contains(n)) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Names);
    }
}
