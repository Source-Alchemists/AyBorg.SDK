using System.ComponentModel;

namespace AyBorg.SDK.Common;

public enum Unit
{
    [Description("1/s")]
    Hertz = 100,
    [Description("ns")]
    Nanoseconds = 101,
    [Description("µs")]
    Microseconds = 102,
    [Description("ms")]
    Milliseconds = 103,
    [Description("s")]
    Seconds = 104,
    [Description("min")]
    Minutes = 105,
    [Description("h")]
    Hours = 106,
    [Description("d")]
    Days = 107
}
