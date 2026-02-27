using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSS.FIP.Data.CMPDataSource
{
    public static class SourceCMPDbContextExtension
    {
        public static string GetViewNameWithSchema<T>(this SourceCMPDbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var qualifiedViewName = entityType?.GetSchemaQualifiedViewName();
            return qualifiedViewName ?? "Unknown (Check configured values for CMPDataViewNameSchema and CMPDataViewName)";
        }
    }
}
