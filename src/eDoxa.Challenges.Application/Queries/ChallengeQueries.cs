// Filename: ChallengeQueries.cs
// Date Created: 2019-04-14
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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Domain.Common.Enums;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class ChallengeQueries
    {
        private static readonly string ExpandParticipants = nameof(Challenge.Participants);
        private static readonly string ExpandParticipantMatches = $"{ExpandParticipants}.{nameof(Participant.Matches)}";
        private static readonly string ExpandParticipantMatchStats = $"{ExpandParticipantMatches}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            Game game,
            ChallengeType type,
            ChallengeState state)
        {
            return await _context.Challenges.AsNoTracking()
                .Include(ExpandParticipantMatchStats)
                .Where(
                    challenge => challenge.Participants.Any(participant => participant.UserId == userId) &&
                                 (challenge.Game & game) != Game.None &&
                                 (challenge.Setup.Type & type) != ChallengeType.None &&
                                 (challenge.Timeline.State & state) != ChallengeState.None
                )
                .OrderBy(challenge => challenge.Timeline.StartedAt)
                .ToListAsync();
        }

        private async Task<IEnumerable<Challenge>> FindChallengesAsNoTrackingAsync(Game game, ChallengeType type, ChallengeState state)
        {
            return await _context.Challenges.AsNoTracking()
                .Include(ExpandParticipantMatchStats)
                .Where(
                    challenge => (challenge.Game & game) != Game.None &&
                                 (challenge.Setup.Type & type) != ChallengeType.None &&
                                 (challenge.Timeline.State & state) != ChallengeState.None
                )
                .OrderBy(challenge => challenge.Game)
                .ThenBy(challenge => challenge.Setup.Type)
                .ThenBy(challenge => challenge.Timeline.State)
                .ThenBy(challenge => challenge.LiveData.Entries)
                .ThenBy(challenge => challenge.Timeline.StartedAt)
                .ToListAsync();
        }

        private async Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.AsNoTracking()
                .Include(ExpandParticipantMatchStats)
                .Where(challenge => challenge.Id == challengeId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQueries : IChallengeQueries
    {
        public async Task<ChallengeListDTO> FindChallengesAsync(Game game, ChallengeType type, ChallengeState state)
        {
            var challenges = await this.FindChallengesAsNoTrackingAsync(game, type, state);

            return _mapper.Map<ChallengeListDTO>(challenges);
        }

        [ItemCanBeNull]
        public async Task<ChallengeDTO> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeAsNoTrackingAsync(challengeId);

            return _mapper.Map<ChallengeDTO>(challenge);
        }

        public async Task<ChallengeListDTO> FindUserChallengeHistoryAsync(UserId userId, Game game, ChallengeType type, ChallengeState state)
        {
            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, type, state);

            return _mapper.Map<ChallengeListDTO>(challenges);
        }
    }
}