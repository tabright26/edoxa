// Filename: ChallengeSetupConverter.cs
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
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    internal sealed class ChallengeSetupConverter : IValueConverter<ChallengeModel, ChallengeSetupViewModel>
    {
        [NotNull]
        public ChallengeSetupViewModel Convert([NotNull] ChallengeModel challenge, [NotNull] ResolutionContext context)
        {
            return new ChallengeSetupViewModel
            {
                BestOf = challenge.Setup.BestOf,
                Entries = challenge.Setup.Entries,
                PayoutEntries = challenge.Setup.PayoutEntries,
                EntryFee = new EntryFeeViewModel
                {
                    Amount = challenge.Setup.EntryFeeAmount,
                    Currency = Currency.FromValue(challenge.Setup.EntryFeeCurrency)
                }
            };
        }
    }
}
