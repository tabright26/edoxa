// Filename: ChallengeSetupDefaults.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services
{
    public sealed class DefaultChallengeSetup : ChallengeSetup
    {
        public DefaultChallengeSetup() : base(
            BestOf.DefaultValue,
            Entries.DefaultValue,
            EntryFee.DefaultValue,
            PayoutRatio.DefaultValue,
            ServiceChargeRatio.DefaultValue,
            ChallengeType.Default
        )
        {
        }
    }
}