using INSS.FIP.Common.Connectors;

namespace INSS.FIP.Connectors;

public class FipApiConnector : ApiConnector, IFipApiConnector
{
    public FipApiConnector(ILogger<ApiConnector> logger, HttpClient httpClient) : base(logger, httpClient) { }
}
