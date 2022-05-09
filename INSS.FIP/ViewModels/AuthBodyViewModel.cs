using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.ViewModels;

[ExcludeFromCodeCoverage]
public class AuthBodyViewModel
{
    public string? Abbreviation { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Telephone { get; set; } = null!;
    public string Fax { get; set; } = null!;
    public string Website { get; set; } = null!;
}
