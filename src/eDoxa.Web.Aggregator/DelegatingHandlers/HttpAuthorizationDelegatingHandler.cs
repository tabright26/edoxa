// Filename: HttpAuthorizationDelegatingHandler.cs
// Date Created: 2019-06-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using static IdentityServer4.IdentityServerConstants;

namespace eDoxa.Web.Aggregator.DelegatingHandlers
{
    public sealed class HttpAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public HttpAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        [ItemNotNull]
        protected override async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _httpContextAccesor.HttpContext.GetTokenAsync(TokenTypes.AccessToken);

            if (token != null)
            {
                request.SetBearerToken(token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
