﻿// Filename: ChallengeRecurringJob.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Challenges.Dtos;
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

        private async Task<IEnumerable<ChallengeDto>> FetchChallengesAsync(GameDto game)
        {
            var challenges = new List<ChallengeDto>();

            var fetchInProgressChallengesRequest = new FetchChallengesRequest
            {
                Game = game,
                State = ChallengeStateDto.InProgress
            };

            var fetchInProgressChallengesResponse = await _challengeServiceClient.FetchChallengesAsync(fetchInProgressChallengesRequest);

            challenges.AddRange(fetchInProgressChallengesResponse.Challenges);

            var fetchEndedChallengesRequest = new FetchChallengesRequest
            {
                Game = game,
                State = ChallengeStateDto.Ended
            };

            var fetchEndedChallengesResponse = await _challengeServiceClient.FetchChallengesAsync(fetchEndedChallengesRequest);

            challenges.AddRange(fetchEndedChallengesResponse.Challenges);

            return challenges;
        }

        public async Task SynchronizeChallengesAsync(GameDto game)
        {
            foreach (var challenge in await this.FetchChallengesAsync(game))
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

                if (challenge.State == ChallengeStateDto.Ended)
                {
                    var synchronizeChallengeRequest = new SynchronizeChallengeRequest
                    {
                        ChallengeId = challenge.Id
                    };

                    await _challengeServiceClient.SynchronizeChallengeAsync(synchronizeChallengeRequest);
                }
            }
        }
    }
}