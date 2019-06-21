﻿// Filename: SetupConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    public sealed class SetupConverter : IValueConverter<ChallengeModel, SetupViewModel>
    {
        [NotNull]
        public SetupViewModel Convert([NotNull] ChallengeModel challenge, [NotNull] ResolutionContext context)
        {
            return new SetupViewModel
            {
                BestOf = challenge.Setup.BestOf,
                Entries = challenge.Setup.Entries,
                PayoutEntries = challenge.Setup.PayoutEntries,
                EntryFee = new EntryFeeViewModel
                {
                    Amount = challenge.Setup.EntryFeeAmount,
                    Type = CurrencyType.FromValue(challenge.Setup.EntryFeeCurrency)
                }
            };
        }
    }
}
