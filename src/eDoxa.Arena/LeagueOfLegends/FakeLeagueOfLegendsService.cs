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

using Bogus;

using eDoxa.Arena.LeagueOfLegends.Abstractions;
using eDoxa.Arena.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.LeagueOfLegends
{
    public sealed class FakeLeagueOfLegendsService : ILeagueOfLegendsService
    {
        private readonly Random _random = new Random();

        public Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime)
        {
            var faker = new Faker<LeagueOfLegendsMatchReferenceDto>();

            //return Task.FromResult(faker.Generate(_random.Next(0, 10)).ToArray());

            return Task.FromResult(Array.Empty<LeagueOfLegendsMatchReferenceDto>());
        }

        public Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId)
        {
            var faker = new Faker<LeagueOfLegendsMatchDto>();

            //return Task.FromResult(faker.Generate());

            return Task.FromResult(new LeagueOfLegendsMatchDto());
        }
    }
}
