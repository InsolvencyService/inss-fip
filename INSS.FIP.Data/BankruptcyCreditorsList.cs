using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Data;

public partial class BankruptcyCreditorsList
{
    public int Id { get; set; }
    public string SourceRef { get; set; }
    public string Name { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
}
