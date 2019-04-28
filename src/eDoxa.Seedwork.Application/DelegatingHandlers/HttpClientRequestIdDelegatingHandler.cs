// Filename: HttpClientRequestIdDelegatingHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Constants;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Application.DelegatingHandlers
{
    public sealed class HttpClientRequestIdDelegatingHandler : DelegatingHandler
    {
        [ItemCanBeNull]
        protected override async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            {
                request.Headers.Add(CustomHeaderNames.IdempotencyKey, Guid.NewGuid().ToString());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}