using System;
using System.Collections.Generic;

namespace INSS.FIP.Repository.Models
{
    public partial class CiSelectionDecode
    {
        public short SelectionType { get; set; }
        public string SelectionField { get; set; } = null!;
    }
}
