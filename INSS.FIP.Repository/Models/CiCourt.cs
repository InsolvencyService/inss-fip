﻿using System;
using System.Collections.Generic;

namespace INSS.FIP.Repository.Models
{
    public partial class CiCourt
    {
        public string Court { get; set; } = null!;
        public int? CourtId { get; set; }
        public string CourtName { get; set; } = null!;
    }
}
