// Filename: ResponseVersionHandler.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.Application.Http.DelegatingHandlers
{
    public sealed class VersionDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            response.Version = request.Version;

            return response;
        }
    }
}
