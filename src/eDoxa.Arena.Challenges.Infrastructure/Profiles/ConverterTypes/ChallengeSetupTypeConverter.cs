// Filename: ChallengeSetupTypeConverter.cs
// Date Created: 2019-06-22
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
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ChallengeSetupTypeConverter : ITypeConverter<ChallengeSetupModel, ChallengeSetup>
    {
        [NotNull]
        public ChallengeSetup Convert([NotNull] ChallengeSetupModel source, [NotNull] ChallengeSetup destination, [NotNull] ResolutionContext context)
        {
            return new ChallengeSetup(
                new BestOf(source.BestOf),
                new PayoutEntries(source.PayoutEntries),
                new EntryFee(CurrencyType.FromValue(source.EntryFeeCurrency), source.EntryFeeAmount)
            );
        }
    }
}
