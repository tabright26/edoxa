// Filename: LeagueOfLegendsDelegatingHandler.cs
// Date Created: 2019-06-29
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

using Microsoft.Extensions.Options;

namespace eDoxa.Arena.LeagueOfLegends
{
    internal sealed class LeagueOfLegendsDelegatingHandler : DelegatingHandler
    {
        private readonly LeagueOfLegendsOptions _leagueOfLegendsOptions;

        public LeagueOfLegendsDelegatingHandler(IOptions<LeagueOfLegendsOptions> leagueOfLegendsOptionsAccessor)
        {
            _leagueOfLegendsOptions = leagueOfLegendsOptionsAccessor.Value;
        }

        [NotNull]
        [ItemNotNull]
        protected override async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Riot-Token", _leagueOfLegendsOptions.RiotToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
