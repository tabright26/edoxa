// Filename: ParticipantQuery.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Extensions;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Queries
{
    public sealed partial class ParticipantQuery
    {
        public ParticipantQuery(ChallengesDbContext context)
        {
            Participants = context.Set<ParticipantModel>().AsNoTracking();
        }

        private IQueryable<ParticipantModel> Participants { get; }

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
            var participants = await this.FetchChallengeParticipantModelsAsync(challengeId);

            return participants.Select(participant => participant.ToEntity()).ToList();
        }

        public async Task<Participant?> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await this.FindParticipantModelAsync(participantId);

            return participant.ToEntity();
        }
    }
}
