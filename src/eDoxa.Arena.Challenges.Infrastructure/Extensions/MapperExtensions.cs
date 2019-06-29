// Filename: ParticipantExtensions.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Infrastructure.Extensions
{
    internal static class MapperExtensions
    {
        public static void CopyChanges(this IMapper mapper, IChallenge challenge, ChallengeModel challengeModel)
        {
            challengeModel.State = challenge.Timeline.State.Value;

            challengeModel.SynchronizedAt = challenge.SynchronizedAt;

            challengeModel.Timeline.StartedAt = challenge.Timeline.StartedAt;

            challengeModel.Timeline.ClosedAt = challenge.Timeline.ClosedAt;

            challengeModel.Participants.ForEach(participantModel => mapper.CopyChanges(challenge.Participants.Single(participant => participant.Id == participantModel.Id), participantModel));

            var participants = challenge.Participants.Where(participant => challengeModel.Participants.All(participantModel => participantModel.Id != participant.Id));

            mapper.Map<ICollection<ParticipantModel>>(participants).ForEach(participant => challengeModel.Participants.Add(participant));
        }

        private static void CopyChanges(this IMapper mapper, Participant participant, ParticipantModel participantModel)
        {
            var matches = participant.Matches.Where(match => participantModel.Matches.All(matchModel => matchModel.Id != match.Id));

            mapper.Map<ICollection<MatchModel>>(matches).ForEach(match => participantModel.Matches.Add(match));
        }
    }
}
