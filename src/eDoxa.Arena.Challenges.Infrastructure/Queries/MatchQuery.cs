// Filename: MatchQuery.cs
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
    public sealed partial class MatchQuery
    {
        private readonly IMapper _mapper;

        public MatchQuery(ChallengesDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            Matches = context.Matches.AsNoTracking();
        }

        private IQueryable<MatchModel> Matches { get; }

        public async Task<IReadOnlyCollection<MatchModel>> FindParticipantMatchesAsNoTrackingAsync(ParticipantId participantId)
        {
            return await Matches.Include(match => match.Participant).Where(match => match.Participant.Id == participantId).ToListAsync();
        }

        public async Task<MatchModel> FindMatchAsNoTrackingAsync(MatchId matchId)
        {
            return await Matches.Where(match => match.Id == matchId).SingleOrDefaultAsync();
        }
    }

    public sealed partial class MatchQuery : IMatchQuery
    {
        public async Task<IReadOnlyCollection<MatchViewModel>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await this.FindParticipantMatchesAsNoTrackingAsync(participantId);

            return _mapper.Map<IReadOnlyCollection<MatchViewModel>>(matches);
        }

        [ItemCanBeNull]
        public async Task<MatchViewModel> FindMatchAsync(MatchId matchId)
        {
            var match = await this.FindMatchAsNoTrackingAsync(matchId);

            return _mapper.Map<MatchViewModel>(match);
        }
    }
}
