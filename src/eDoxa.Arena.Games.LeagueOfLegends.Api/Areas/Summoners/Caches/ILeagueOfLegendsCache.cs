// Filename: ILeagueOfLegendsCache.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using RiotSharp.Endpoints.SummonerEndpoint;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Caches
{
    public interface ILeagueOfLegendsCache
    {
        Task AddSummonerAsync(UserId userId, Summoner summoner);

        Task RemoveSummonerAsync(UserId userId);

        Task<Summoner> GetSummonerAsync(UserId userId);

        Task<bool> SummonerExistsAsync(UserId userId);
    }
}
