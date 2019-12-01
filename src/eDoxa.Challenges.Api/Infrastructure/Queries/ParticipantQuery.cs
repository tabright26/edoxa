// Filename: ParticipantQuery.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Api.Infrastructure.Queries
{
    public sealed partial class ParticipantQuery
    {
        public ParticipantQuery(ChallengesDbContext context, IMapper mapper)
        {
            Mapper = mapper;
            Participants = context.Participants.AsNoTracking();
        }

        private IQueryable<ParticipantModel> Participants { get; }

        public IMapper Mapper { get; }

        private async Task<IReadOnlyCollection<ParticipantModel>> FetchChallengeParticipantModelsAsync(Guid challengeId)
        {
            var participants = from participant in Participants.Include(participant => participant.Matches)
                                   .Include(participant => participant.Challenge)
                                   .AsExpandable()
                               where participant.Challenge.Id == challengeId
                               select participant;

            return await participants.ToListAsync();
        }

        private async Task<ParticipantModel> FindParticipantModelAsync(Guid participantId)
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
        public async Task<IReadOnlyCollection<Participant>> FetchChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participantModels = await this.FetchChallengeParticipantModelsAsync(challengeId);

            return Mapper.Map<IReadOnlyCollection<Participant>>(participantModels);
        }

        public async Task<Participant?> FindParticipantAsync(ParticipantId participantId)
        {
            var participantModel = await this.FindParticipantModelAsync(participantId);

            return Mapper.Map<Participant>(participantModel);
        }
    }
}
