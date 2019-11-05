// Filename: ChallengeService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Challenges.Api.HttpClients;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Challenges.Api.Areas.Challenges.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGamesHttpClient _gamesHttpClient;

        public ChallengeService(IChallengeRepository challengeRepository, IGamesHttpClient gamesHttpClient)
        {
            _challengeRepository = challengeRepository;
            _gamesHttpClient = gamesHttpClient;
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task<ValidationResult> CreateChallengeAsync(
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeDuration duration,
            IDateTimeProvider createAt,
            CancellationToken cancellationToken = default
        )
        {
            var scoring = await _gamesHttpClient.GetChallengeScoringAsync(game);

            var challenge = new Challenge(name, game, bestOf, entries, new ChallengeTimeline(createAt, duration), new Scoring(scoring));

            _challengeRepository.Create(challenge);

            await _challengeRepository.CommitAsync(cancellationToken);

            return new ValidationResult();
        }

        public async Task<ValidationResult> RegisterParticipantAsync(
            IChallenge challenge,
            UserId userId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        )
        {
            if (challenge.SoldOut)
            {
                return new ValidationFailure("_error", "The challenge was sold out.").ToResult();
            }

            if (challenge.ParticipantExists(userId))
            {
                return new ValidationFailure("_error", "The user already is registered.").ToResult();
            }

            challenge.Register(new Participant(userId, playerId, registeredAt));

            if (challenge.SoldOut)
            {
                challenge.Start(registeredAt);
            }

            await _challengeRepository.CommitAsync(cancellationToken);

            return new ValidationResult();
        }

        public async Task SynchronizeAsync(
            Game game,
            TimeSpan interval,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        )
        {
            foreach (var challenge in await _challengeRepository.FetchChallengesAsync(game, ChallengeState.InProgress))
            {
                foreach (var participant in challenge.Participants)
                {
                    foreach (var match in await _gamesHttpClient.GetChallengeMatchesAsync(
                        game,
                        participant.PlayerId,
                        challenge.Timeline.StartedAt,
                        challenge.Timeline.EndedAt))
                    {
                        participant.Snapshot(
                            new StatMatch(
                                challenge.Scoring,
                                new GameStats(match.Stats),
                                new GameReference(match.Reference),
                                synchronizedAt));
                    }

                    await _challengeRepository.CommitAsync(cancellationToken);
                }
            }
        }

        //private async Task CloseAsync(IDateTimeProvider closedAt, CancellationToken cancellationToken = default)
        //{
        //    var challenges = await _challengeRepository.FetchChallengesAsync(null, ChallengeState.Ended);

        //    foreach (var challenge in challenges.OrderByDescending(challenge => challenge.Timeline.EndedAt))
        //    {
        //        await this.Synchronize(challenge);

        //        challenge.Close(closedAt);

        //        await _challengeRepository.CommitAsync(cancellationToken);
        //    }
        //}
    }
}
