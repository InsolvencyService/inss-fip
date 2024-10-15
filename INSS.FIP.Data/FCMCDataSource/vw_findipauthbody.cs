using System;

namespace INSS.FIP.Data.FCMCDataSource
{
    public partial class vw_findipauthbody
    {
        public string AuthBodyCode { get; set; } // varchar(5)
        public string? AuthBodyName { get; set; } // varchar(8000)
        public string? AuthBodyAddressLine1 { get; set; } // varchar(8000)
        public string? AuthBodyAddressLine2 { get; set; } // varchar(8000)
        public string? AuthBodyAddressLine3 { get; set; } // varchar(8000)
        public string? AuthBodyAddressLine4 { get; set; } // varchar(8000)
        public string? AuthBodyAddressLine5 { get; set; } // varchar(8000)
        public string? AuthBodyPostcode { get; set; } // varchar(8000)
    }
}
