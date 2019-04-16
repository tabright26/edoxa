// Filename: ChallengesMapperFactory.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.AutoMapper.Factories;
using eDoxa.Challenges.DTO.Profiles;

namespace eDoxa.Challenges.DTO.Factories
{
    public sealed partial class ChallengesMapperFactory
    {
        private static readonly Lazy<ChallengesMapperFactory> Lazy = new Lazy<ChallengesMapperFactory>(() => new ChallengesMapperFactory());

        public static ChallengesMapperFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

    public sealed partial class ChallengesMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new ChallengeListProfile();
            yield return new MatchListProfile();
            yield return new StatListProfile();
            yield return new ParticipantListProfile();
            yield return new ChallengeLiveDataProfile();
            yield return new ChallengePrizeBreakdownProfile();
            yield return new ChallengeProfile();
            yield return new ChallengeScoringProfile();
            yield return new ChallengeSettingsProfile();
            yield return new ChallengeTimelineProfile();
            yield return new MatchProfile();
            yield return new StatProfile();
            yield return new ParticipantProfile();
        }
    }
}