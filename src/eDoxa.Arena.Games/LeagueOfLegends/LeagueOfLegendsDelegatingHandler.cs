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

using Microsoft.Extensions.Options;

namespace eDoxa.Arena.Games.LeagueOfLegends
{
    internal sealed class LeagueOfLegendsDelegatingHandler : DelegatingHandler
    {
        private readonly LeagueOfLegendsOptions _leagueOfLegendsOptions;

        public LeagueOfLegendsDelegatingHandler(IOptions<LeagueOfLegendsOptions> leagueOfLegendsOptionsAccessor)
        {
            _leagueOfLegendsOptions = leagueOfLegendsOptionsAccessor.Value;
        }

        
        
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Riot-Token", _leagueOfLegendsOptions.RiotToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
