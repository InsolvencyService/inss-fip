using System;
using System.Collections.Generic;

namespace INSS.FIP.Repository.Models
{
    public partial class SubjectIbro
    {
        public int CaseId { get; set; }
        public int SubjRefno { get; set; }
        public int RegBNo { get; set; }
        public DateTime? IbroAppFiledDate { get; set; }
        public DateTime? IbroHearingDate { get; set; }
        public DateTime? IbroOrderDate { get; set; }
        public DateTime? IbroNoOrderDate { get; set; }
        public DateTime? IbroDischargeDate { get; set; }
        public DateTime? IbroEndDate { get; set; }
    }
}
