using System;
using System.Collections.Generic;

namespace INSS.FIP.Repository.Models
{
    public partial class AnonCaseName
    {
        public string Casename { get; set; } = null!;
        public string? Casereference { get; set; }
    }
}
