// Filename: IdentityDelegatingHandler.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using static IdentityServer4.IdentityServerConstants;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.DelegatingHandlers
{
    public sealed class IdentityDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public IdentityDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
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
