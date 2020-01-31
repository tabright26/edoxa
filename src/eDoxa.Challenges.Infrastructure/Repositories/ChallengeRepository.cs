// Filename: ChallengeRepository.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Infrastructure.Extensions;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository : Repository<IChallenge, ChallengeModel>
    {
        public ChallengeRepository(ChallengesDbContext context)
        {
            UnitOfWork = context;
            Challenges = context.Set<ChallengeModel>();
        }

        private DbSet<ChallengeModel> Challenges { get; }

        private IUnitOfWork UnitOfWork { get; }

        private async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(int? game = null, int? state = null)
        {
            var challenges = from challenge in Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where (game == null || challenge.Game == game) && (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<ChallengeModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }

        private async Task<bool> ChallengeModelExistsAsync(Guid challengeId)
        {
            var challenges = from challenge in Challenges.AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.AnyAsync();
        }
    }

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        public void Create(IEnumerable<IChallenge> challenges)
        {
            foreach (var challenge in challenges)
            {
                this.Create(challenge);
            }
        }

        public void Create(IChallenge challenge)
        {
            var challengeModel = challenge.ToModel();

            Challenges.Add(challengeModel);

            Mappings[challenge] = challengeModel;
        }

        public void Delete(IChallenge challenge)
        {
            var challengeModel = Mappings[challenge];

            Mappings.Remove(challenge);

            Challenges.Remove(challengeModel);
        }

        public async Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null)
        {
            var challenges = await this.FetchChallengeModelsAsync(game?.Value, state?.Value);

            return challenges.Select(Selector).ToList();

            IChallenge Selector(ChallengeModel challengeModel)
            {
                var challenge = challengeModel.ToEntity();

                Mappings[challenge] = challengeModel;

                return challenge;
            }
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await this.FindChallengeOrNullAsync(challengeId) ?? throw new InvalidOperationException("Challenge doesn't exists.");
        }

        public async Task<bool> ChallengeExistsAsync(ChallengeId challengeId)
        {
            return await this.ChallengeModelExistsAsync(challengeId);
        }

        public async Task<IChallenge?> FindChallengeOrNullAsync(ChallengeId challengeId)
        {
            var challenge = Mappings.Keys.SingleOrDefault(x => x.Id == challengeId);

            if (challenge != null)
            {
                return challenge;
            }

            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            if (challengeModel == null)
            {
                return null;
            }

            challenge = challengeModel.ToEntity();

            Mappings[challenge] = challengeModel;

            return challenge;
        }

        public override async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            foreach (var (challenge, challengeModel) in Mappings)
            {
                this.CopyChanges(challenge, challengeModel);
            }

            await UnitOfWork.CommitAsync(dispatchDomainEvents, cancellationToken);
        }

        private void CopyChanges(IChallenge challenge, ChallengeModel challengeModel)
        {
            challengeModel.DomainEvents = challenge.DomainEvents.ToList();

            challenge.ClearDomainEvents();

            challengeModel.State = challenge.Timeline.State.Value;

            challengeModel.SynchronizedAt = challenge.SynchronizedAt;

            challengeModel.Timeline.StartedAt = challenge.Timeline.StartedAt;

            challengeModel.Timeline.ClosedAt = challenge.Timeline.ClosedAt;

            foreach (var participantModel in challengeModel.Participants)
            {
                this.CopyChanges(challenge.Participants.Single(participant => participant.Id == participantModel.Id), participantModel);
            }

            var participants =
                challenge.Participants.Where(participant => challengeModel.Participants.All(participantModel => participantModel.Id != participant.Id));

            foreach (var participant in participants.Select(participant => participant.ToModel()).ToList())
            {
                challengeModel.Participants.Add(participant);
            }
        }

        private void CopyChanges(Participant participant, ParticipantModel participantModel)
        {
            participantModel.DomainEvents = participant.DomainEvents.ToList();

            participant.ClearDomainEvents();

            participantModel.SynchronizedAt = participant.SynchronizedAt;

            foreach (var matchModel in participantModel.Matches)
            {
                this.CopyChanges(participant.Matches.Single(match => match.Id == matchModel.Id), matchModel);
            }

            var matches = participant.Matches.Where(match => participantModel.Matches.All(matchModel => matchModel.Id != match.Id));

            foreach (var match in matches.Select(match => match.ToModel()))
            {
                participantModel.Matches.Add(match);
            }
        }

        private void CopyChanges(IMatch match, MatchModel matchModel)
        {
            matchModel.DomainEvents = match.DomainEvents.ToList();

            match.ClearDomainEvents();
        }
    }
}
