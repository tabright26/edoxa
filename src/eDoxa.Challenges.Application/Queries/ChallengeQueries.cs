﻿// Filename: ChallengeQueries.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
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

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class ChallengeQueries
    {
        internal static readonly string ExpandParticipants = nameof(Challenge.Participants);
        internal static readonly string ExpandParticipantMatches = $"{ExpandParticipants}.{nameof(Participant.Matches)}";
        internal static readonly string ExpandParticipantMatchStats = $"{ExpandParticipantMatches}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ChallengeQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                                                  (challenge.Settings.Type & type) != ChallengeType.None &&
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
                                                  (challenge.Settings.Type & type) != ChallengeType.None &&
                                                  (challenge.Timeline.State & state) != ChallengeState.None
                                 )
                                 .OrderBy(challenge => challenge.Game)
                                 .ThenBy(challenge => challenge.Settings.Type)
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