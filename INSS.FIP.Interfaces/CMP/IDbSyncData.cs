using System.Threading.Tasks;

namespace INSS.FIP.Interfaces.CMP;

public interface IDbSyncData<TData>
{
    Task<bool> SynchronizeDataAsync();
}
