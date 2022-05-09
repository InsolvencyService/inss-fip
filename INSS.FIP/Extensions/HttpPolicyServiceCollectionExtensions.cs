using INSS.FIP.Common.Models.ClientOptionsModels;
using INSS.FIP.Common.Models.PollyModels;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System.Diagnostics.CodeAnalysis;

namespace INSS.FIP.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpPolicyServiceCollectionExtensions
{
    public static IServiceCollection AddPolicies(
        this IServiceCollection services,
        IPolicyRegistry<string> policyRegistry,
        string retryPolicyName,
        RetryPolicyOptions retryPolicyOptions)
    {
        policyRegistry?.Add(
            retryPolicyName,
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => r.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .OrResult(r => r?.Headers?.RetryAfter != null)
                .WaitAndRetryAsync(
                    retryPolicyOptions.Count,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryPolicyOptions.BackoffPower, retryAttempt))));

        return services;
    }

    public static IServiceCollection AddHttpClient<TClient, TImplementation, TClientOptions>(
        this IServiceCollection services,
        string retryPolicyName)
        where TClient : class
        where TImplementation : class, TClient
        where TClientOptions : ClientOptionsModel
    {
        return services
            .AddHttpClient<TClient, TImplementation>()
            .ConfigureHttpClient((sp, options) =>
            {
                TClientOptions? httpClientOptions = sp.GetRequiredService<TClientOptions>();
                if (httpClientOptions != null)
                {
                    options.BaseAddress = httpClientOptions.BaseAddress;
                    options.Timeout = httpClientOptions.Timeout;

                    if (!string.IsNullOrWhiteSpace(httpClientOptions?.ApiKey))
                    {
                        options.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", httpClientOptions.ApiKey);
                    }
                }
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            })
            .AddPolicyHandlerFromRegistry(retryPolicyName)
            .Services;
    }
}
