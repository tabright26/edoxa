// Filename: ChallengeId.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class ChallengeId : EntityId<ChallengeId>
    {
    }
}
