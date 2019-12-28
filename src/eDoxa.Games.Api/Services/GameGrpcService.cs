﻿// Filename: GameGrpcService.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Responses;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Grpc.Core;

using Microsoft.Extensions.Logging;

namespace eDoxa.Games.Api.Services
{
    public sealed class GameGrpcService : GameService.GameServiceBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IGameCredentialService _gameCredentialService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GameGrpcService(IChallengeService challengeService, IGameCredentialService gameCredentialService, IMapper mapper, ILogger<GameGrpcService> logger)
        {
            _challengeService = challengeService;
            _gameCredentialService = gameCredentialService;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<FindPlayerGameCredentialResponse> FindPlayerGameCredential(FindPlayerGameCredentialRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var credential = await _gameCredentialService.FindCredentialAsync(userId, request.Game.ToEnumeration<Game>());

            if (credential == null)
            {
                throw context.NotFoundRpcException("Game credential not found.");
            }

            var response = new FindPlayerGameCredentialResponse
            {
                Credential = _mapper.Map<GameCredentialDto>(credential)
            };

            return context.Ok(response);
        }

        public override async Task<FetchChallengeScoringResponse> FetchChallengeScoring(FetchChallengeScoringRequest request, ServerCallContext context)
        {
            return new FetchChallengeScoringResponse
            {
                Scoring =
                {
                    await _challengeService.GetScoringAsync(request.Game.ToEnumeration<Game>())
                }
            };
        }

        public override async Task FetchChallengeMatches(
            FetchChallengeMatchesRequest request,
            IServerStreamWriter<FetchChallengeMatchesResponse> responseStream,
            ServerCallContext context
        )
        {
            foreach (var participant in request.Participants)
            {
                var gamePlayerId = participant.PlayerId.ParseStringId<PlayerId>();

                var participantId = participant.Id.ParseEntityId<ParticipantId>();

                var game = request.Game.ToEnumeration<Game>();

                try
                {
                    var matches = await _challengeService.GetMatchesAsync(
                        game,
                        gamePlayerId,
                        participant.StartedAt.ToDateTime(),
                        participant.EndedAt.ToDateTime());

                    var response = new FetchChallengeMatchesResponse
                    {
                        GamePlayerId = gamePlayerId,
                        Matches =
                        {
                            matches.Select(
                                match => new GameMatchDto
                                {
                                    GameUuid = match.GameUuid,
                                    Timestamp = match.Timestamp.ToTimestampUtc(),
                                    Stats =
                                    {
                                        match.Stats
                                    }
                                })
                        }
                    };

                    await responseStream.WriteAsync(response);
                }
                catch (Exception exception)
                {
                    _logger.LogCritical(exception, $"Failed to fetch {game} matches for the participant '{participantId}'. (gamePlayerId=\"{gamePlayerId}\")");

                    var response = new FetchChallengeMatchesResponse
                    {
                        GamePlayerId = gamePlayerId
                    };

                    await responseStream.WriteAsync(response);
                }
            }
        }
    }
}