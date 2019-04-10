// Filename: PolicyHandlers.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net;
using System.Net.Http;

using Polly;
using Polly.Extensions.Http;

namespace eDoxa.Security.Policies
{
    public static class PolicyHandlers
    {
        public static IAsyncPolicy<HttpResponseMessage> RetryPolicyHandler(int retryCount = 6)
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                                       .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                                       .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicyHandler(
            int handledEventsAllowedBeforeBreaking = 5,
            int durationOfBreakInSeconds = 30)
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                                       .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking, TimeSpan.FromSeconds(durationOfBreakInSeconds));
        }
    }
}