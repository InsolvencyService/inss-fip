using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Interfaces.CMP;

public interface IDataSourceProvider<T>
{
    Task<List<T>> GetDataFromViewAsync();
}
