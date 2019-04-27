// Filename: ParticipantQueries.cs
// Date Created: 2019-04-21
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
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class ParticipantQueries
    {
        private static readonly string ExpandMatches = nameof(Participant.Matches);
        private static readonly string ExpandMatchStats = $"{ExpandMatches}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ParticipantQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Participant>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Participants.AsNoTracking()
                .Include(ExpandMatchStats)
                .Where(participant => participant.Challenge.Id == challengeId)
                .OrderBy(participant => participant.Timestamp)
                .ToListAsync();
        }

        private async Task<Participant> FindParticipantAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Participants.AsNoTracking()
                .Include(ExpandMatchStats)
                .Where(participant => participant.Id == participantId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class ParticipantQueries : IParticipantQueries
    {
        public async Task<Maybe<ParticipantListDTO>> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await this.FindChallengeParticipantsAsNoTrackingAsync(challengeId);

            var mapper = _mapper.Map<ParticipantListDTO>(participants);

            return mapper.Any() ? new Maybe<ParticipantListDTO>(mapper) : new Maybe<ParticipantListDTO>();
        }

        public async Task<Maybe<ParticipantDTO>> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await this.FindParticipantAsNoTrackingAsync(participantId);

            var mapper = _mapper.Map<ParticipantDTO>(participant);

            return mapper != null ? new Maybe<ParticipantDTO>(mapper) : new Maybe<ParticipantDTO>();
        }
    }
}