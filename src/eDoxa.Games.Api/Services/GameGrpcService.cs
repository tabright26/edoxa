// Filename: GameGrpcService.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

                var matches = await _challengeService.GetMatchesAsync(
                    request.Game.ToEnumeration<Game>(),
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
        }
    }
}
