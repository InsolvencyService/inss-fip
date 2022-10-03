using INSS.FIP.Interfaces;
using Microsoft.Extensions.Logging;

namespace INSS.FIP.Services;

public class FipApiConnector : ApiConnector, IFipApiConnector
{
    public FipApiConnector(ILogger<ApiConnector> logger, HttpClient httpClient) : base(logger, httpClient) { }
}
