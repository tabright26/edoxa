// Filename: FakeLeagueOfLegendsService.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.LeagueOfLegends.Abstractions;
using eDoxa.Arena.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks
{
    public sealed class MockLeagueOfLegendsService : ILeagueOfLegendsService
    {
        public Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime)
        {
            return Task.FromResult(Array.Empty<LeagueOfLegendsMatchReferenceDto>());
        }

        public Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId)
        {
            return Task.FromResult(new LeagueOfLegendsMatchDto());
        }
    }
}
