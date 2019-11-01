// Filename: LeagueOfLegendsAuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Abstractions.Adapter;
using eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Arena.Games.LeagueOfLegends.Requests;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthFactorGeneratorAdapter : AuthFactorGeneratorAdapter<LeagueOfLegendsRequest>
    {
        private const string ApiKey = "RGAPI-5ed57054-6ce4-4882-b51f-f609545f30a0";

        private static readonly RiotApi RiotApi;

        private readonly IAuthFactorRepository _authFactorRepository;

        static LeagueOfLegendsAuthFactorGeneratorAdapter()
        {
            RiotApi = RiotApi.GetDevelopmentInstance(ApiKey);
        }

        public LeagueOfLegendsAuthFactorGeneratorAdapter(IAuthFactorRepository authFactorRepository)
        {
            _authFactorRepository = authFactorRepository;
        }

        public override Game Game => Game.LeagueOfLegends;

        public override async Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, LeagueOfLegendsRequest request)
        {
            var summoner = await RiotApi.Summoner.GetSummonerByNameAsync(Region.Na, request.SummonerName);

            if (summoner == null)
            {
                return new ValidationFailure(string.Empty, $"{Game} summoner's name doesn't exists. Note: Only NA server is supported for the moment").ToResult();
            }

            if (await _authFactorRepository.AuthFactorExistsAsync(userId, Game))
            {
                await _authFactorRepository.RemoveAuthFactorAsync(userId, Game);
            }

            await _authFactorRepository.AddAuthFactorAsync(userId, Game, GenerateAuthFactor(summoner));

            return new ValidationResult();
        }

        private static AuthFactor GenerateAuthFactor(Summoner summoner)
        {
            const int ProfileIconIdMinIndex = 0;
            const int ProfileIconIdMaxIndex = 28;

            var random = new Random();

            var profileIconId = summoner.ProfileIconId;

            while (profileIconId == summoner.ProfileIconId)
            {
                profileIconId = random.Next(ProfileIconIdMinIndex, ProfileIconIdMaxIndex);
            }

            return new AuthFactor(PlayerId.Parse(summoner.AccountId), profileIconId);
        }
    }
}
