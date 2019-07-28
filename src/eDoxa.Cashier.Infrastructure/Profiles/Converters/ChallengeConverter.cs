// Filename: ChallengeConverter.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class ChallengeConverter : ITypeConverter<ChallengeModel, IChallenge>
    {
        
        public IChallenge Convert( ChallengeModel source,  IChallenge destination,  ResolutionContext context)
        {
            var entryFee = new EntryFee(source.EntryFeeAmount, Currency.FromValue(source.EntryFeeCurrency));

            var payout = context.Mapper.Map<IPayout>(source.Buckets);

            var challenge = new Challenge(entryFee, payout);

            challenge.SetEntityId(ChallengeId.FromGuid(source.Id));

            return challenge;
        }
    }
}
