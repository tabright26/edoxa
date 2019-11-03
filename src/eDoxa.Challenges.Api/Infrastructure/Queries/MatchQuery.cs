// Filename: MatchQuery.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Api.Infrastructure.Queries
{
    public sealed partial class MatchQuery
    {
        public MatchQuery(ChallengesDbContext context, IMapper mapper)
        {
            Mapper = mapper;
            Matches = context.Matches.AsNoTracking();
        }

        private IQueryable<MatchModel> Matches { get; }

        public IMapper Mapper { get; }

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
            var matchModels = await this.FetchParticipantMatchModelsAsync(participantId);

            return Mapper.Map<IReadOnlyCollection<IMatch>>(matchModels);
        }

        public async Task<IMatch?> FindMatchAsync(MatchId matchId)
        {
            var matchModel = await this.FindMatchModelAsync(matchId);

            return Mapper.Map<IMatch>(matchModel);
        }
    }
}
