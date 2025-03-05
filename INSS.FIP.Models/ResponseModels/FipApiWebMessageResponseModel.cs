using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels;

[ExcludeFromCodeCoverage]
public class FipApiWebMessageResponseModel
{
    public bool HideSearch { get; set; }

    public string Message { get; set; }
}
