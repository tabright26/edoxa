﻿// Filename: LeagueOfLegendsAuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Storage.Azure.Extensions;

using FluentValidation.Results;

using Microsoft.Azure.Storage;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationGeneratorAdapter : AuthenticationGeneratorAdapter<LeagueOfLegendsRequest>
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly CloudStorageAccount _storageAccount;

        public LeagueOfLegendsAuthenticationGeneratorAdapter(
            ILeagueOfLegendsService leagueOfLegendsService,
            IAuthenticationRepository authenticationRepository,
            CloudStorageAccount storageAccount
        )
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _authenticationRepository = authenticationRepository;
            _storageAccount = storageAccount;
        }

        public override Game Game => Game.LeagueOfLegends;

        public override async Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, LeagueOfLegendsRequest request)
        {
            var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByNameAsync(Region.Na, request.SummonerName);

            if (summoner == null)
            {
                return new ValidationFailure(
                    string.Empty,
                    $"{Game} summoner's name doesn't exists. Note: Only NA server is supported for the moment").ToResult();
            }

            if (await _authenticationRepository.AuthenticationExistsAsync(userId, Game))
            {
                await _authenticationRepository.RemoveAuthenticationAsync(userId, Game);
            }

            await _authenticationRepository.AddAuthenticationAsync(userId, Game, await this.GenerateAuthFactor(summoner));

            return new ValidationResult();
        }

        private async Task<LeagueOfLegendsGameAuthentication> GenerateAuthFactor(Summoner summoner)
        {
            const int ProfileIconIdMinIndex = 0;
            const int ProfileIconIdMaxIndex = 28;

            var random = new Random();

            var currentSummonerProfileIconId = summoner.ProfileIconId;

            var expectedSummonerProfileIconId = summoner.ProfileIconId;

            while (expectedSummonerProfileIconId == summoner.ProfileIconId)
            {
                expectedSummonerProfileIconId = random.Next(ProfileIconIdMinIndex, ProfileIconIdMaxIndex);
            }

            var t = await this.DownloadSummonerProfileIconIdAsync(currentSummonerProfileIconId);
            var y = await this.DownloadSummonerProfileIconIdAsync(expectedSummonerProfileIconId);

            return new LeagueOfLegendsGameAuthentication(
                PlayerId.Parse(summoner.AccountId),
                new LeagueOfLegendsGameAuthenticationFactor(
                    currentSummonerProfileIconId,
                    t,
                    expectedSummonerProfileIconId,
                    y));
        }

        public async Task<string> DownloadSummonerProfileIconIdAsync(int summonerProfileIconId)
        {
            var container = _storageAccount.GetBlobContainer();

            var blobReference = container.GetBlockBlobReference($"games/leagueoflegends/summonerProfileIconIds/{summonerProfileIconId}.png");

            using var memoryStream = new MemoryStream();

            await blobReference.DownloadToStreamAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var b64String = Convert.ToBase64String(bytes);

            return "data:image/png;base64," + b64String;
        }
    }
}