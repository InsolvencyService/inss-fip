using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Interfaces;

public interface IDataTargetRepository<T>
{
    Task TruncateAndInsertAsync(List<T> data);
}
