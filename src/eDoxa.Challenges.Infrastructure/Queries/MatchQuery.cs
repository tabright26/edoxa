// Filename: MatchQuery.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Extensions;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Queries
{
    public sealed partial class MatchQuery
    {
        public MatchQuery(ChallengesDbContext context)
        {
            Matches = context.Set<MatchModel>().AsNoTracking();
        }

        private IQueryable<MatchModel> Matches { get; }

        private async Task<IReadOnlyCollection<MatchModel>> FetchParticipantMatchModelsAsync(Guid participantId)
        {
            var matches = from match in Matches.Include(match => match.Participant).AsExpandable()
                          where match.Participant.Id == participantId
                          select match;

            return await matches.ToListAsync();
        }

        private async Task<MatchModel> FindMatchModelAsync(Guid matchId)
        {
            var matches = from match in Matches.Include(match => match.Participant).AsExpandable()
                          where match.Id == matchId
                          select match;

            return await matches.SingleOrDefaultAsync();
        }
    }

    public sealed partial class MatchQuery : IMatchQuery
    {
        public async Task<IReadOnlyCollection<IMatch>> FetchParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await this.FetchParticipantMatchModelsAsync(participantId);

            return matches.Select(match => match.ToEntity()).ToList();
        }

        public async Task<IMatch?> FindMatchAsync(MatchId matchId)
        {
            var match = await this.FindMatchModelAsync(matchId);

            return match.ToEntity();
        }
    }
}
