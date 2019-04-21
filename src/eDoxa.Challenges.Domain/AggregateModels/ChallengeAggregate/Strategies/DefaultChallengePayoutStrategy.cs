﻿// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies
{
    public class DefaultChallengePayoutStrategy : IChallengePayoutStrategy
    {
        private readonly EntryFee _entryFee;
        private readonly PayoutEntries _payoutEntries;
        private readonly PrizePool _prizePool;

        public DefaultChallengePayoutStrategy(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            _payoutEntries = payoutEntries;
            _prizePool = prizePool;
            _entryFee = entryFee;
        }

        public IChallengePayout Payout
        {
            get
            {
                try
                {
                    if (_payoutEntries < 10)
                    {
                        return this.CreatePayout();
                    }

                    var prizes = new Prizes(_payoutEntries, _prizePool, _entryFee);
                    var bucketSizes = new BucketSizes(_payoutEntries, 5);

                    var (initialPrizes, leftover) = Prizes.InitPrizes(prizes, bucketSizes);
                    var (finalPrizes, finalBucketSizes, finalLeftover) = Prizes.SpendLeftover(initialPrizes, bucketSizes, leftover);

                    var buckets = new Buckets(finalBucketSizes, finalPrizes);

                    return new ChallengePayout(buckets, finalLeftover);
                }
                catch (Exception)
                {
                    return this.CreatePayout();
                }
            }
        }

        private ChallengePayout CreatePayout()
        {
            var payoutEntries = new PayoutEntries(Entries.DefaultValue, PayoutRatio.DefaultValue);

            return new ChallengePayout(
                new Buckets(new BucketSizes(payoutEntries, 10),
                    new Prizes(payoutEntries, new PrizePool(Entries.DefaultValue, EntryFee.DefaultValue, ServiceChargeRatio.DefaultValue),
                        EntryFee.DefaultValue)), new PayoutLeftover(0));
        }
    }
}