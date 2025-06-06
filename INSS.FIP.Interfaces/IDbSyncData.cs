using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Interfaces;

public interface IDbSyncData<TData>
{
    Task<bool> SynchronizeBankruptcyCreditorsAsync(string orderByColumn);
}
