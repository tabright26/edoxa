// Filename: ChallengeRecurringJob.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;

using Grpc.Core;

namespace eDoxa.Challenges.Worker.Application.RecurringJobs
{
    public sealed class ChallengeRecurringJob
    {
        private readonly ChallengeService.ChallengeServiceClient _challengeServiceClient;
        private readonly GameService.GameServiceClient _gameServiceClient;

        public ChallengeRecurringJob(ChallengeService.ChallengeServiceClient challengeServiceClient, GameService.GameServiceClient gameServiceClient)
        {
            _challengeServiceClient = challengeServiceClient;
            _gameServiceClient = gameServiceClient;
        }

        public async Task SynchronizeChallengeAsync(GameDto game)
        {
            var fetchChallengesRequest = new FetchChallengesRequest
            {
                Game = game,
                State = ChallengeStateDto.InProgress
            };

            var fetchChallengesResponse = await _challengeServiceClient.FetchChallengesAsync(fetchChallengesRequest);

            foreach (var challenge in fetchChallengesResponse.Challenges)
            {
                var fetchChallengeMatchesRequest = new FetchChallengeMatchesRequest
                {
                    Game = game,
                    Participants =
                    {
                        challenge.Participants.Select(
                            participant => new GameParticipantDto
                            {
                                Id = participant.Id,
                                PlayerId = participant.GamePlayerId,
                                StartedAt = participant.SynchronizedAt ?? challenge.Timeline.StartedAt,
                                EndedAt = challenge.Timeline.EndedAt
                            })
                    }
                };

                await foreach (var fetchChallengeMatchesResponse in _gameServiceClient.FetchChallengeMatches(fetchChallengeMatchesRequest)
                    .ResponseStream.ReadAllAsync())
                {
                    var snapshotChallengeParticipantRequest = new SnapshotChallengeParticipantRequest
                    {
                        ChallengeId = challenge.Id,
                        GamePlayerId = fetchChallengeMatchesResponse.GamePlayerId,
                        Matches =
                        {
                            fetchChallengeMatchesResponse.Matches
                        }
                    };

                    await _challengeServiceClient.SnapshotChallengeParticipantAsync(snapshotChallengeParticipantRequest);
                }

                var synchronizeChallengeRequest = new SynchronizeChallengeRequest
                {
                    ChallengeId = challenge.Id
                };

                await _challengeServiceClient.SynchronizeChallengeAsync(synchronizeChallengeRequest);
            }
        }
    }
}
