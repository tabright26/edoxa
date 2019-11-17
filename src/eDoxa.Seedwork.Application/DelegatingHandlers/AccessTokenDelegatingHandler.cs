// Filename: AccessTokenDelegateHandler.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using IdentityServer4;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.DelegatingHandlers
{
    public sealed class AccessTokenDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AccessTokenDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _httpContextAccesor.HttpContext.GetTokenAsync(IdentityServerConstants.TokenTypes.AccessToken);

            if (token != null)
            {
                request.SetBearerToken(token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
