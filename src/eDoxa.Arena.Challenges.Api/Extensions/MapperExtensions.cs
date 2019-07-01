// Filename: MapperExtensions.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Reflection;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class MapperExtensions
    {
        public static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                config =>
                {
                    config.AddProfiles(Assembly.GetAssembly(typeof(ChallengesDbContext)));
                    config.AddProfiles(Assembly.GetAssembly(typeof(Startup)));
                }
            )
        );

        public static IReadOnlyCollection<ChallengeModel> ToModels(this IEnumerable<Challenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static ChallengeModel ToModel(this Challenge challenge)
        {
            return Mapper.Map<ChallengeModel>(challenge);
        }

        public static IReadOnlyCollection<ParticipantModel> ToModels(this IEnumerable<Participant> participants)
        {
            return Mapper.Map<IReadOnlyCollection<ParticipantModel>>(participants);
        }

        public static ParticipantModel ToModel(this Participant participant)
        {
            return Mapper.Map<ParticipantModel>(participant);
        }

        public static IReadOnlyCollection<MatchModel> ToModels(this IEnumerable<Match> matches)
        {
            return Mapper.Map<IReadOnlyCollection<MatchModel>>(matches);
        }

        public static MatchModel ToModel(this Match match)
        {
            return Mapper.Map<MatchModel>(match);
        }

        public static IReadOnlyCollection<ChallengeViewModel> ToViewModels(this IEnumerable<Challenge> challenges)
        {
            return Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges.ToModels());
        }

        public static ChallengeViewModel ToViewModel(this Challenge challenge)
        {
            return Mapper.Map<ChallengeViewModel>(challenge.ToModel());
        }

        public static IReadOnlyCollection<ParticipantViewModel> ToViewModels(this IEnumerable<Participant> participants)
        {
            return Mapper.Map<IReadOnlyCollection<ParticipantViewModel>>(participants.ToModels());
        }

        public static ParticipantViewModel ToViewModel(this Participant participant)
        {
            return Mapper.Map<ParticipantViewModel>(participant.ToModel());
        }

        public static IReadOnlyCollection<MatchViewModel> ToViewModels(this IEnumerable<Match> matches)
        {
            return Mapper.Map<IReadOnlyCollection<MatchViewModel>>(matches.ToModels());
        }

        public static MatchViewModel ToViewModel(this Match match)
        {
            return Mapper.Map<MatchViewModel>(match.ToModel());
        }
    }
}
