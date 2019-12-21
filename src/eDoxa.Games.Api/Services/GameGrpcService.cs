// Filename: GameGrpcService.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Responses;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Grpc.Core;

using Microsoft.Extensions.Logging;

namespace eDoxa.Games.Api.Services
{
    public sealed class GameGrpcService : GameService.GameServiceBase
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger _logger;

        public GameGrpcService(IChallengeService challengeService, ILogger<GameGrpcService> logger)
        {
            _challengeService = challengeService;
            _logger = logger;
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
