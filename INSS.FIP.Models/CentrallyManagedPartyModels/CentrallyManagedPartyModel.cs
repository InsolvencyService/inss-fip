using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Models.CentrallyManagedPartyModels;

[ExcludeFromCodeCoverage]
public class CentrallyManagedPartyModel : IComparable<CentrallyManagedPartyModel>
{
    private string _sourceRef;
    private string _name;
    private string _addressLine1;
    private string _addressLine2;
    private string _addressLine3;
    private string _town;
    private string _county;
    private string _postCode;
    private string _country;

    //To comply with legacy CMP import routines
    //SourceRef must contains a non-empty string value, null should cause sync to fail importing to CreditorStore DB
    //AddressLine1 should always be empty string to preserve current ODS app behaviours
    //All other properties should return empty string if set to null
    //See findings from https://inssdigital.atlassian.net/wiki/x/BwByFwE
    
    public string SourceRef { get => string.IsNullOrEmpty(_sourceRef) ? null : _sourceRef; set => _sourceRef = value; }
    public string Name { get => _name ?? ""; set => _name = value; }
    public string AddressLine1 { get => ""; set => _addressLine1 = value; }
    public string AddressLine2 { get => _addressLine2 ?? ""; set => _addressLine2 = value; }
    public string AddressLine3 { get => _addressLine3 ?? ""; set => _addressLine3 = value; }
    public string Town { get => _town ?? ""; set => _town = value; }
    public string County { get => _county ?? ""; set => _county = value; }
    public string PostCode { get => _postCode ?? ""; set => _postCode = value; }
    public string Country { get => _country ?? ""; set => _country = value; }

    public int CompareTo(CentrallyManagedPartyModel other)
    {
        if (other == null) return 1;
        return string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }
}
