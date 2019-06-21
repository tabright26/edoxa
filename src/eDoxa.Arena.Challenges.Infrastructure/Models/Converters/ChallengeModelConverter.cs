// Filename: ChallengeConverter.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Models.Converters
{
    internal sealed class ChallengeModelConverter : ITypeConverter<ChallengeModel, IChallenge>
    {
        [NotNull]
        public IChallenge Convert([NotNull] ChallengeModel source, [NotNull] IChallenge destination, [NotNull] ResolutionContext context)
        {
            var challenge = new PersistentChallenge(
                ChallengeId.FromGuid(source.Id),
                ChallengeGame.FromValue(source.Game),
                new ChallengeName(source.Name),
                new ChallengeSetup(
                    new BestOf(source.Setup.BestOf),
                    new PayoutEntries(source.Setup.PayoutEntries),
                    new EntryFee(CurrencyType.FromValue(source.Setup.EntryFeeCurrency), source.Setup.EntryFeeAmount)
                ),
                new ChallengeDuration(TimeSpan.FromTicks(source.Timeline.Duration)),
                new PersistentDateTimeProvider(source.CreatedAt)
            );

            var scoring = new Scoring();

            source.ScoringItems.ForEach(item => scoring.Add(new StatName(item.Name), new StatWeighting(item.Weighting)));

            challenge.ApplyScoring(scoring);

            challenge.ApplyPayout(
                new Payout(
                    new Buckets(
                        source.Buckets.Select(
                            bucket => new Bucket(new Prize(bucket.PrizeAmount, CurrencyType.FromValue(bucket.PrizeCurrency)), new BucketSize(bucket.Size))
                        )
                    )
                )
            );

            var participants = context.Mapper.Map<ICollection<Participant>>(source.Participants);

            participants.ForEach(participant => challenge.Register(participant));

            return challenge;
        }
    }
}
