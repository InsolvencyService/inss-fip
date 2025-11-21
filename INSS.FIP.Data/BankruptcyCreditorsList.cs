
namespace INSS.FIP.Data;

public class BankruptcyCreditorsList : IComparable<BankruptcyCreditorsList>
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

    public int CompareTo(BankruptcyCreditorsList? other)
    {
        if (other == null) return 1;
        return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
    }
}
