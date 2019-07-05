﻿// Filename: ChallengeSetupModelConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ChallengeSetupModelConverter : IValueConverter<ChallengeSetup, ChallengeSetupModel>
    {
        [NotNull]
        public ChallengeSetupModel Convert([NotNull] ChallengeSetup sourceMember, [NotNull] ResolutionContext context)
        {
            return new ChallengeSetupModel
            {
                BestOf = sourceMember.BestOf,
                Entries = sourceMember.Entries,
                PayoutEntries = sourceMember.PayoutEntries,
                EntryFeeAmount = sourceMember.EntryFee.Amount,
                EntryFeeCurrency = sourceMember.EntryFee.Currency.Value
            };
        }
    }
}