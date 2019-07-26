// Filename: HttpPolicies.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net;
using System.Net.Http;

using Polly;
using Polly.Extensions.Http;

namespace eDoxa.Seedwork.Monitoring
{
    public static class HttpPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
