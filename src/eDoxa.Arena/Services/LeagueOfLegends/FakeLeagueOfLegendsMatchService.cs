// Filename: FakeLeagueOfLegendsMatchService.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Services.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Services.LeagueOfLegends
{
    public sealed class FakeLeagueOfLegendsMatchService : ILeagueOfLegendsMatchService
    {
        public Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime)
        {
            return Task.FromResult(new LeagueOfLegendsMatchReferenceDto[] { });
        }

        public Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId)
        {
            return Task.FromResult(new LeagueOfLegendsMatchDto());
        }
    }
}
