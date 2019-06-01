﻿// Filename: FakeChallengeSetup.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Tests.Utilities.Fakes
{
    public sealed class FakeChallengeSetup : ChallengeSetup
    {
        public FakeChallengeSetup() : base(BestOf.Three, PayoutEntries.Ten, MoneyEntryFee.TwoAndHalf)
        {
        }
    }
}