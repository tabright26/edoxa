﻿// Filename: ChallengeRepository.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository
    {
        private readonly IDictionary<Guid, IChallenge> _materializedIds = new Dictionary<Guid, IChallenge>();
        private readonly IDictionary<IChallenge, ChallengeModel> _materializedObjects = new Dictionary<IChallenge, ChallengeModel>();
        private readonly IMapper _mapper;
        private readonly ChallengesDbContext _context;

        public ChallengeRepository(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(int? game = null, int? state = null)
        {
            var challenges = from challenge in _context.Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where (game == null || challenge.Game == game) && (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<ChallengeModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in _context.Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }

        private async Task<bool> AnyChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in _context.Challenges.AsExpandable()
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
            var challengeModel = _mapper.Map<ChallengeModel>(challenge);

            _context.Challenges.Add(challengeModel);

            _materializedObjects[challenge] = challengeModel;
        }

        public async Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null)
        {
            var challenges = await this.FetchChallengeModelsAsync(game?.Value, state?.Value);

            return challenges.Select(
                    challengeModel =>
                    {
                        var challenge = _mapper.Map<IChallenge>(challengeModel);

                        _materializedObjects[challenge] = challengeModel;

                        _materializedIds[challenge.Id] = challenge;

                        return challenge;
                    })
                .ToList();
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            if (_materializedIds.TryGetValue(challengeId, out var challenge))
            {
                return challenge;
            }

            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            if (challengeModel == null)
            {
                return null;
            }

            challenge = _mapper.Map<IChallenge>(challengeModel);

            _materializedObjects[challenge] = challengeModel;

            _materializedIds[challengeId] = challenge;

            return challenge;
        }

        public async Task<bool> AnyChallengeAsync(ChallengeId challengeId)
        {
            return await this.AnyChallengeModelAsync(challengeId);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var (challenge, challengeModel) in _materializedObjects)
            {
                this.CopyChanges(challenge, challengeModel);
            }

            await _context.CommitAsync(cancellationToken: cancellationToken);

            foreach (var (challenge, challengeModel) in _materializedObjects)
            {
                _materializedIds[challengeModel.Id] = challenge;
            }
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

            foreach (var participant in _mapper.Map<ICollection<ParticipantModel>>(participants))
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

            foreach (var match in _mapper.Map<ICollection<MatchModel>>(matches))
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
