// Filename: HttpClientAuthorizationDelegatingHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using IdentityServer4;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace eDoxa.Seedwork.Application.DelegatingHandlers
{
    public sealed class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly HttpContext _httpContext;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor accesor)
        {
            _httpContext = accesor.HttpContext ?? throw new ArgumentNullException(nameof(accesor));
        }

        [ItemCanBeNull]
        protected override async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage message, CancellationToken cancellationToken)
        {
            var authorization = _httpContext.Request.Headers[HeaderNames.Authorization];

            if (!string.IsNullOrEmpty(authorization))
            {
                message.Headers.Add(
                    HeaderNames.Authorization,
                    new List<string>
                    {
                        authorization
                    }
                );
            }

            var token = await _httpContext.GetTokenAsync(IdentityServerConstants.TokenTypes.AccessToken);

            if (token != null)
            {
                message.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            }

            return await base.SendAsync(message, cancellationToken);
        }
    }
}