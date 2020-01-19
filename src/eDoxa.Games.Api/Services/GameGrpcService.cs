// Filename: GameGrpcService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
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

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace eDoxa.Games.Api.Services
{
    public sealed class GameGrpcService : GameService.GameServiceBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IGameCredentialService _gameCredentialService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GameGrpcService(
            IChallengeService challengeService,
            IGameCredentialService gameCredentialService,
            IMapper mapper,
            ILogger<GameGrpcService> logger
        )
        {
            _challengeService = challengeService;
            _gameCredentialService = gameCredentialService;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<FindPlayerGameCredentialResponse> FindPlayerGameCredential(
            FindPlayerGameCredentialRequest request,
            ServerCallContext context
        )
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

        public override async Task<FindChallengeScoringResponse> FindChallengeScoring(FindChallengeScoringRequest request, ServerCallContext context)
        {
            return new FindChallengeScoringResponse
            {
                Scoring = await _challengeService.GetScoringAsync(request.Game.ToEnumeration<Game>())
            };
        }

        public override async Task FetchChallengeMatches(
            FetchChallengeMatchesRequest request,
            IServerStreamWriter<FetchChallengeMatchesResponse> responseStream,
            ServerCallContext context
        )
        {
            var game = request.Game.ToEnumeration<Game>();

            var startedAt = request.StartedAt.ToDateTime();

            var endedAt = request.EndedAt.ToDateTime();

            foreach (var participant in request.Participants)
            {
                var participantId = participant.Id.ParseEntityId<ParticipantId>();

                var gamePlayerId = participant.GamePlayerId.ParseStringId<PlayerId>();

                var matchIds = participant.Matches.Select(match => match.Id).ToImmutableHashSet();

                try
                {
                    var matches = await _challengeService.GetMatchesAsync(
                        game,
                        gamePlayerId,
                        startedAt,
                        endedAt,
                        matchIds);

                    var response = new FetchChallengeMatchesResponse
                    {
                        GamePlayerId = gamePlayerId,
                        Matches =
                        {
                            matches.Select(
                                match => new GameMatchDto
                                {
                                    GameUuid = match.GameUuid,
                                    GameCreatedAt = match.GameCreatedAt.ToTimestampUtc(),
                                    GameDuration = match.GameDuration.ToDuration(),
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

                    _logger.LogCritical(
                        JsonConvert.SerializeObject(
                            new
                            {
                                ParticipantId = participantId,
                                Game = game,
                                GamePlayerId = gamePlayerId,
                                StartedAt = startedAt,
                                EndedAt = endedAt,
                                MatchIds = matchIds.ToArray()
                            },
                            Formatting.Indented));

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
