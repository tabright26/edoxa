// Filename: ChallengeQueries.cs
// Date Created: 2019-05-06
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    public sealed partial class ChallengeQuery
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Challenge.Participants)}.{nameof(Participant.Matches)}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeQuery(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(UserId userId, Game game) 
        {
            return await _context.Challenges
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId) && challenge.Game.HasFlag(game))
                .ToListAsync();
        }

        private async Task<IEnumerable<Challenge>> FindChallengesAsNoTrackingAsync(Game game)
        {
            return await _context.Challenges
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(challenge => challenge.Game.HasFlag(game))
                .OrderBy(challenge => challenge.Game)
                .ToListAsync();
        }

        private async Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Challenges
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(challenge => challenge.Id == challengeId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<Option<ChallengeListDTO>> FindChallengesAsync(Game game)
        {
            var challenges = await this.FindChallengesAsNoTrackingAsync(game);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }

        public async Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeAsNoTrackingAsync(challengeId);

            var mapper = _mapper.Map<ChallengeDTO>(challenge);

            return mapper != null ? new Option<ChallengeDTO>(mapper) : new Option<ChallengeDTO>();
        }

        public async Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(UserId userId, Game game)
        {
            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, game);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }
    }
}