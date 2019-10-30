// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-10-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Services
{
    public sealed class LeagueOfLegendsCredentialService : ILeagueOfLegendsCredentialService
    {
        private const int ProfileIconIdMinIndex = 0;
        private const int ProfileIconIdMaxIndex = 28;

        private readonly Random _random = new Random();

        public int GenerateDifferentProfileIconId(int profileIconId)
        {
            var differentProfileIconId = profileIconId;

            while (differentProfileIconId == profileIconId)
            {
                differentProfileIconId = _random.Next(ProfileIconIdMinIndex, ProfileIconIdMaxIndex);
            }

            return differentProfileIconId;
        }
    }
}
