namespace AyBorg.SDK.Common.Models;

public readonly record struct Line {
    public Point Position1 { get; init; }
    public Point Position2 { get; init; }
}