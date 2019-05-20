// Filename: ParticipantQueries.cs
// Date Created: 2019-05-03
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Functional;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    public sealed partial class ParticipantQueries
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Participant.Matches)}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public ParticipantQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Participant>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Participants
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(participant => participant.Challenge.Id == challengeId)
                .OrderBy(participant => participant.Timestamp)
                .ToListAsync();
        }

        private async Task<Participant> FindParticipantAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Participants
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(participant => participant.Id == participantId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class ParticipantQueries : IParticipantQueries
    {
        public async Task<Option<ParticipantListDTO>> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await this.FindChallengeParticipantsAsNoTrackingAsync(challengeId);

            var list = _mapper.Map<ParticipantListDTO>(participants);

            return list.Any() ? new Option<ParticipantListDTO>(list) : new Option<ParticipantListDTO>();
        }

        public async Task<Option<ParticipantDTO>> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await this.FindParticipantAsNoTrackingAsync(participantId);

            var mapper = _mapper.Map<ParticipantDTO>(participant);

            return mapper != null ? new Option<ParticipantDTO>(mapper) : new Option<ParticipantDTO>();
        }
    }
}