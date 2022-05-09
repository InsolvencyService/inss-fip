using INSS.FIP.FnApp.Services;
using INSS.FIP.Repository.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

[assembly: FunctionsStartup(typeof(INSS.FIP.FnApp.Startup))]

namespace INSS.FIP.FnApp;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = builder.GetContext().Configuration;

        //   builder.Services.AddApplicationInsightsTelemetry();
        builder.Services.AddHttpClient();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddTransient((s) =>
        {
            var connectionString = Environment.GetEnvironmentVariable("iirwebdbContextConnectionString");
            return new iirwebdbContext(connectionString);
        });

        builder.Services.AddTransient<IAuthBodyService, AuthBodyService>();
        builder.Services.AddTransient<IInsolvencyPractitionerService, InsolvencyPractitionerService>();
        builder.Services.AddTransient<IWebMessageService, WebMessageService>();
    }
}