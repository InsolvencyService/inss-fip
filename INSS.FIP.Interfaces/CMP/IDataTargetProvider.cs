using System.Collections.Generic;
using System.Threading.Tasks;

namespace INSS.FIP.Interfaces.CMP;

public interface IDataTargetProvider<T>
{
    Task TruncateAndInsertAsync(List<T> data);
}
