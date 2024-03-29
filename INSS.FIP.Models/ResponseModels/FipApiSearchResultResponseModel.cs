﻿using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.ResponseModels;

[ExcludeFromCodeCoverage]
public class FipApiSearchResultResponseModel
{
    public int? IpNumber { get; set; }

    public string Title { get; set; }

    public string FirstNames { get; set; }

    public string LastName { get; set; }

    public string Company { get; set; }

    public string Town { get; set; }

    public string Postcode { get; set; }
}
