// Filename: SetupConverter.cs
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

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    internal sealed class ChallengeSetupConverter : IValueConverter<ChallengeSetup, SetupModel>
    {
        [NotNull]
        public SetupModel Convert([NotNull] ChallengeSetup sourceMember, [NotNull] ResolutionContext context)
        {
            return new SetupModel
            {
                BestOf = sourceMember.BestOf.Value,
                Entries = sourceMember.Entries.Value,
                PayoutEntries = sourceMember.PayoutEntries.Value,
                EntryFeeAmount = sourceMember.EntryFee.Amount,
                EntryFeeCurrency = sourceMember.EntryFee.Type.Value
            };
        }
    }
}
