// Filename: HttpClientBuilderExtensions.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net;

using Microsoft.Extensions.DependencyInjection;

using Polly;
using Polly.Extensions.Http;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddRetryPolicyHandler(this IHttpClientBuilder builder, int retryCount = 6)
        {
            return builder.AddPolicyHandler(
                HttpPolicyExtensions.HandleTransientHttpError()
                    .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(retryCount, sleepDuration => TimeSpan.FromSeconds(Math.Pow(2, sleepDuration))));
        }

        public static IHttpClientBuilder AddCircuitBreakerPolicyHandler(this IHttpClientBuilder builder)
        {
            return builder.AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }
}
