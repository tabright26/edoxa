// Filename: LeagueOfLegendsAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Abstractions.Adapter;
using eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp;
using RiotSharp.Misc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthFactorValidatorAdapter : IAuthFactorValidatorAdapter
    {
        private const string ApiKey = "RGAPI-5ed57054-6ce4-4882-b51f-f609545f30a0";

        private static readonly RiotApi RiotApi;

        private readonly IAuthFactorRepository _authFactorRepository;

        static LeagueOfLegendsAuthFactorValidatorAdapter()
        {
            RiotApi = RiotApi.GetDevelopmentInstance(ApiKey);
        }

        public LeagueOfLegendsAuthFactorValidatorAdapter(IAuthFactorRepository authFactorRepository)
        {
            _authFactorRepository = authFactorRepository;
        }

        public Game Game => Game.LeagueOfLegends;

        public async Task<ValidationResult> ValidateAuthFactorAsync(UserId userId, AuthFactor authFactor)
        {
            await _authFactorRepository.RemoveAuthFactorAsync(userId, Game);

            var summoner = await RiotApi.Summoner.GetSummonerByAccountIdAsync(Region.Na, authFactor.PlayerId);

            if (summoner.ProfileIconId != Convert.ToInt32(authFactor.Key))
            {
                return new ValidationFailure("", "").ToResult();
            }

            return new ValidationResult();
        }
    }
}
