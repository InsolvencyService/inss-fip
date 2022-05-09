﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.FnApp.Models.RequestModels.WebMessage;

[ExcludeFromCodeCoverage]
public class WebMessageRequestModel
{
    [Required]
    public string? ApplicationPrefix { get; set; }
}
