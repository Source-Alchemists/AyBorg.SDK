namespace Atomy.SDK.DAL
{
    public record EnumRecord
    {
        public string Name { get; set; } = string.Empty;
        public string[] Names { get; set; } = new string[0];
    }
}