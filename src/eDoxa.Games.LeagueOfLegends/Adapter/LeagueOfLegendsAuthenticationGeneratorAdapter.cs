// Filename: LeagueOfLegendsAuthenticationGeneratorAdapter.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

namespace eDoxa.Games.LeagueOfLegends.Adapter
{
    public sealed class LeagueOfLegendsAuthenticationGeneratorAdapter : AuthenticationGeneratorAdapter<LeagueOfLegendsRequest>
    {
        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IGameAuthenticationRepository _gameAuthenticationRepository;
        private readonly IGameCredentialRepository _gameCredentialRepository;

        public LeagueOfLegendsAuthenticationGeneratorAdapter(
            ILeagueOfLegendsService leagueOfLegendsService,
            IGameAuthenticationRepository gameAuthenticationRepository,
            IGameCredentialRepository gameCredentialRepository
        )
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _gameAuthenticationRepository = gameAuthenticationRepository;
            _gameCredentialRepository = gameCredentialRepository;
        }

        public override Game Game => Game.LeagueOfLegends;

        public override async Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, LeagueOfLegendsRequest request)
        {
            try
            {
                var result = new DomainValidationResult<object>();

                var summoner = await _leagueOfLegendsService.Summoner.GetSummonerByNameAsync(Region.Na, request.SummonerName);

                if (await _gameCredentialRepository.CredentialExistsAsync(PlayerId.Parse(summoner.AccountId), Game))
                {
                    result.AddFailedPreconditionError("Summoner's name is already linked by another eDoxa account");
                }

                if (result.IsValid)
                {
                    if (await _gameAuthenticationRepository.AuthenticationExistsAsync(userId, Game))
                    {
                        await _gameAuthenticationRepository.RemoveAuthenticationAsync(userId, Game);
                    }

                    var gameAuthentication = await this.GenerateAuthFactor(summoner);

                    await _gameAuthenticationRepository.AddAuthenticationAsync(userId, Game, gameAuthentication);

                    return gameAuthentication.Factor;
                }

                return result;
            }
            catch (RiotSharpException)
            {
                return DomainValidationResult<object>.Failure("Summoner name is invalid");
            }
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
            //var container = _storageAccount.GetBlobContainer();

            //var blobReference = container.GetBlockBlobReference($"games/leagueoflegends/summonerProfileIconIds/{summonerProfileIconId}.png");

            //await blobReference.DownloadToStreamAsync(memoryStream);

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://raw.communitydragon.org/")
            };

            await using var stream =
                await httpClient.GetStreamAsync($"latest/plugins/rcp-be-lol-game-data/global/default/v1/profile-icons/{summonerProfileIconId}.jpg");

            await using var memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream);

            return $"data:image/png;base64,{Convert.ToBase64String(memoryStream.ToArray())}";
        }
    }
}
