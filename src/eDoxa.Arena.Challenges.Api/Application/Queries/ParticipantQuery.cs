// Filename: ParticipantQuery.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Api.Application.Queries
{
    public sealed partial class ParticipantQuery
    {
        private readonly IMapper _mapper;

        public ParticipantQuery(ChallengesDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            Participants = context.Participants.AsNoTracking();
        }

        private IQueryable<ParticipantModel> Participants { get; }

        private async Task<IReadOnlyCollection<ParticipantModel>> FindChallengeParticipantsAsNoTrackingAsync(Guid challengeId)
        {
            var participants = from participant in Participants.Include(participant => participant.Matches)
                                   .Include(participant => participant.Challenge)
                                   .AsExpandable()
                               where participant.Challenge.Id == challengeId
                               select participant;

            return await participants.ToListAsync();
        }

        private async Task<ParticipantModel> FindParticipantAsNoTrackingAsync(Guid participantId)
        {
            var participants = from participant in Participants.Include(participant => participant.Matches)
                                   .Include(participant => participant.Challenge)
                                   .AsExpandable()
                               where participant.Id == participantId
                               select participant;

            return await participants.SingleOrDefaultAsync();
        }
    }

    public sealed partial class ParticipantQuery : IParticipantQuery
    {
        public async Task<IReadOnlyCollection<ParticipantViewModel>> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await this.FindChallengeParticipantsAsNoTrackingAsync(challengeId);

            return _mapper.Map<IReadOnlyCollection<ParticipantViewModel>>(participants);
        }

        [ItemCanBeNull]
        public async Task<ParticipantViewModel> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await this.FindParticipantAsNoTrackingAsync(participantId);

            return _mapper.Map<ParticipantViewModel>(participant);
        }
    }
}
