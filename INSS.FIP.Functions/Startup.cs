using System.Diagnostics.CodeAnalysis;
using INSS.FIP.Data;
using INSS.FIP.DataAccess;
using INSS.FIP.Functions;
using INSS.FIP.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace INSS.FIP.Functions;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = builder.GetContext().Configuration;

        //builder.Services.AddApplicationInsightsTelemetry();
        builder.Services.AddHttpClient();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddTransient(_ =>
        {
            var connectionString = Environment.GetEnvironmentVariable("iirwebdbContextConnectionString");
            return new iirwebdbContext(connectionString);
        });

        builder.Services.AddTransient<IAuthBodyProvider, AuthBodyProvider>();
        builder.Services.AddTransient<IInsolvencyPractitionerProvider, InsolvencyPractitionerProvider>();
        builder.Services.AddTransient<IWebMessageProvider, WebMessageProvider>();
    }
}