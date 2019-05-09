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

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class ChallengeQueries
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Challenge.Participants)}.{nameof(Participant.Matches)}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            ChallengeType type,
            Game game,
            ChallengeState state)
        {
            return await _context.Challenges
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId) && challenge.Game.HasFlag(game) &&
                                    challenge.Setup.Type.HasFlag(type) && challenge.Timeline.State.HasFlag(state))
                .OrderBy(challenge => challenge.Timeline.StartedAt)
                .ToListAsync();
        }

        private async Task<IEnumerable<Challenge>> FindChallengesAsNoTrackingAsync(ChallengeType type, Game game, ChallengeState state)
        {
            return await _context.Challenges
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(challenge => challenge.Game.HasFlag(game) && challenge.Setup.Type.HasFlag(type) && challenge.Timeline.State.HasFlag(state))
                .OrderBy(challenge => challenge.Game)
                .ThenBy(challenge => challenge.Setup.Type)
                .ThenBy(challenge => challenge.Timeline.State)
                .ThenBy(challenge => challenge.Timeline.StartedAt)
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

    public sealed partial class ChallengeQueries : IChallengeQueries
    {
        public async Task<Option<ChallengeListDTO>> FindChallengesAsync(ChallengeType type, Game game, ChallengeState state)
        {
            var challenges = await this.FindChallengesAsNoTrackingAsync(type, game, state);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }

        public async Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeAsNoTrackingAsync(challengeId);

            var mapper = _mapper.Map<ChallengeDTO>(challenge);

            return mapper != null ? new Option<ChallengeDTO>(mapper) : new Option<ChallengeDTO>();
        }

        public async Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(UserId userId, ChallengeType type, Game game, ChallengeState state)
        {
            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, type, game, state);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }
    }
}