using System.Collections.Generic;
using System.Threading.Tasks;

namespace INSS.FIP.Interfaces.CMP;

public interface IDataSourceProvider<T>
{
    Task<List<T>> GetDataFromViewAsync();
}
