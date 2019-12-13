﻿// Filename: ChallengeGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Responses;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

namespace eDoxa.Games.Api.Services
{
    public sealed class GameGrpcService : GameService.GameServiceBase
    {
        private readonly IChallengeService _challengeService;

        public GameGrpcService(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public override async Task<FetchChallengeScoringResponse> FetchChallengeScoring(FetchChallengeScoringRequest request, ServerCallContext context)
        {
            var scoring = await _challengeService.GetScoringAsync(request.Game.ToEnumeration<Game>());

            var response = new FetchChallengeScoringResponse();

            foreach (var (key, value) in scoring)
            {
                response.Scoring.Add(key, value);
            }

            return response;
        }

        public override async Task<FetchChallengeMatchesResponse> FetchChallengeMatches(FetchChallengeMatchesRequest request, ServerCallContext context)
        {
            var matches = await _challengeService.GetMatchesAsync(
                request.Game.ToEnumeration<Game>(),
                request.GamePlayerId.ParseStringId<PlayerId>(),
                request.StartedAt.ToDateTime(),
                request.EndedAt.ToDateTime());

            var response = new FetchChallengeMatchesResponse();

            foreach (var match in matches)
            {
                var matchDto = new MatchDto
                {
                    GameUuid = match.GameUuid,
                    Timestamp = match.Timestamp.ToTimestamp()
                };

                foreach (var (key, value) in match.Stats)
                {
                    matchDto.Stats.Add(key, value);
                }

                response.Matches.Add(matchDto);
            }

            return response;
        }
    }
}
