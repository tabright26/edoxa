// Filename: ChallengesMapperFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.DTO.Profiles;
using eDoxa.AutoMapper.Factories;

namespace eDoxa.Arena.Challenges.DTO.Factories
{
    public sealed partial class ChallengesMapperFactory
    {
        private static readonly Lazy<ChallengesMapperFactory> Lazy = new Lazy<ChallengesMapperFactory>(() => new ChallengesMapperFactory());

        public static ChallengesMapperFactory Instance => Lazy.Value;
    }

    public sealed partial class ChallengesMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new ChallengeListProfile();
            yield return new MatchListProfile();
            yield return new StatListProfile();
            yield return new ParticipantListProfile();
            //yield return new ChallengeLiveDataProfile();
            yield return new ChallengePayoutProfile();
            yield return new ChallengeProfile();
            yield return new ChallengeScoringProfile();
            yield return new ChallengeSetupProfile();
            yield return new ChallengeTimelineProfile();
            yield return new MatchProfile();
            yield return new StatProfile();
            yield return new ParticipantProfile();
            yield return new BucketProfile();
            yield return new BucketListProfile();
        }
    }
}