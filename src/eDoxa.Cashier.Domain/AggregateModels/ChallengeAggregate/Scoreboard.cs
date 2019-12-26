// Filename: Scoreboard.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Scoreboard : Dictionary<UserId, decimal?>
    {
        public Scoreboard(IPayout payout, IDictionary<UserId, decimal?> scoreboard) : base(scoreboard)
        {
            Ladders = payout.Buckets;
            PayoutCurrency = payout.PrizePool.Currency;
            Winners = new Queue<UserId>(scoreboard.OrderByDescending(item => item.Value).Select(item => item.Key).Take(payout.Entries));
            Losers = new List<UserId>(scoreboard.OrderByDescending(item => item.Value).Select(item => item.Key).Skip(payout.Entries));
        }

        public IBuckets Ladders { get; }

        public Currency PayoutCurrency { get; }

        public Queue<UserId> Winners { get; }

        public List<UserId> Losers { get; }
    }
}
