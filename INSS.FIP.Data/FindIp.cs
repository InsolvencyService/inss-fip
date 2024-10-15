using System;

namespace INSS.FIP.Data
{
    public partial class FindIp
    {
        public int IpNo { get; set; }
        public string? Forenames { get; set; }
        public string? Surname { get; set; }
        public string? RegisteredFirmName { get; set; }
        public string? RegisteredAddressLine3 { get; set; }
        public string? RegisteredAddressLine4 { get; set; }
        public string? RegisteredAddressLine5 { get; set; }
        public string? RegisteredPostCode { get; set; }
        public string? RegisteredPhone { get; set; }
        public string? IpEmailAddress { get; set; }
        public string IncludeOnInternet { get; set; } = " ";
        public string? LicensingBody { get; set; }
    }
}
