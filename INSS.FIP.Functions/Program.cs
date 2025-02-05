using INSS.FIP.DataAccess;
using INSS.FIP.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using INSS.FIP.Data;
using INSS.FIP.Data.FCMCDataSource;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
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
    })
    .Build();

host.Run();
