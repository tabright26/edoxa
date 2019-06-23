// Filename: ParticipantQuery.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Queries
{
    public sealed partial class ParticipantQuery
    {
        private const string NavigationPropertyPath = "Matches";

        private readonly IMapper _mapper;

        public ParticipantQuery(ChallengesDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            Participants = context.Participants.AsNoTracking();
        }

        private IQueryable<ParticipantModel> Participants { get; }

        public async Task<IReadOnlyCollection<ParticipantModel>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await Participants.Include(NavigationPropertyPath)
                .Include(participant => participant.Challenge)
                .Where(participant => participant.Challenge.Id == challengeId)
                .ToListAsync();
        }

        public async Task<ParticipantModel> FindParticipantAsNoTrackingAsync(ParticipantId participantId)
        {
            return await Participants.Include(NavigationPropertyPath).Where(participant => participant.Id == participantId).SingleOrDefaultAsync();
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
