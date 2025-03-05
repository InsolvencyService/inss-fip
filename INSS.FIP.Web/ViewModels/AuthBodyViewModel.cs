using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Web.ViewModels;

[ExcludeFromCodeCoverage]
public class AuthBodyViewModel
{
    public string? Abbreviation { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string AuthBodyAddressLine1 { get; set; } = null!;

    public string AuthBodyAddressLine2 { get; set; } = null!;

    public string AuthBodyAddressLine3 { get; set; } = null!;

    public string AuthBodyAddressLine4 { get; set; } = null!;

    public string AuthBodyAddressLine5 { get; set; } = null!;

    public string AuthBodyPostcode { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public string Website { get; set; } = null!;
}