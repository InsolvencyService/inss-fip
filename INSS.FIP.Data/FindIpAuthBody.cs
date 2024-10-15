using System;

namespace INSS.FIP.Data
{
    public partial class FindIpAuthBody
    {
        public string? AuthBodyCode { get; set; }             
        public string? AuthBodyName { get; set; }             
        public string? AuthBodyAddressLine1 { get; set; }     
        public string? AuthBodyAddressLine2 { get; set; }      
        public string? AuthBodyAddressLine3 { get; set; }      
        public string? AuthBodyAddressLine4 { get; set; }       
        public string? AuthBodyAddressLine5 { get; set; }     
        public string? AuthBodyPostcode { get; set; }           
    }
}
