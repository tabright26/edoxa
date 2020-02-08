// Filename: ChallengeScoreboard.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeScoreboard : Dictionary<UserId, decimal?>
    {
        public ChallengeScoreboard(IChallengePayout payout, IDictionary<UserId, decimal?> scoreboard) : base(scoreboard)
        {
            Buckets = payout.Buckets;
            PayoutCurrencyType = payout.PrizePool.Type;
            var participants = scoreboard.Where(x => x.Value != null).OrderByDescending(item => item.Value).Select(item => item.Key).ToList();
            Winners = new Queue<UserId>(participants.Take(payout.Entries));
            Losers = new List<UserId>(participants.Skip(payout.Entries));
        }

        public IChallengePayoutBuckets Buckets { get; }

        public CurrencyType PayoutCurrencyType { get; }

        public Queue<UserId> Winners { get; }

        public List<UserId> Losers { get; }
    }
}
