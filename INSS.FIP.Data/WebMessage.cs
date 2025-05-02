using System;
using System.Collections.Generic;

namespace INSS.FIP.Data
{
    public partial class GetWebPageBannerMessage
    {
        public int Id { get; set; }
        public string Application { get; set; } = null!;
        public string? Message { get; set; }
        public string? HideSearch { get; set; }
    }
}
