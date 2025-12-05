using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;
using INSS.FIP.DataAccess;
using INSS.FIP.Functions.Helper;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient<SourceDbContext>(_ =>
        {
            var connectionString = Environment.GetEnvironmentVariable("sourceDbContextConnectionString");
            return new SourceDbContext(connectionString);
        });

        services.AddTransient<iirwebdbContext>(_ =>
        {
            var connectionString = Environment.GetEnvironmentVariable("iirwebdbContextConnectionString");
            return new iirwebdbContext(connectionString);
        });

        services.AddTransient<IAuthBodyProvider, AuthBodyProvider>();
        services.AddTransient<IInsolvencyPractitionerProvider, InsolvencyPractitionerProvider>();
        services.AddTransient<IWebMessageProvider, WebMessageProvider>();
        services.AddTransient<IDbSync, DbSync>();
    })
    .Build();

host.Run();
