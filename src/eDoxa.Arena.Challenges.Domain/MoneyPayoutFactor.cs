// Filename: MoneyPayoutFactor.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class MoneyPayoutFactor : PayoutFactor
    {
        public static readonly PayoutFactor TwoAndHalf = new MoneyPayoutFactor(1);
        public static readonly PayoutFactor Five = new MoneyPayoutFactor(2);
        public static readonly PayoutFactor Ten = new MoneyPayoutFactor(4);
        public static readonly PayoutFactor Twenty = new MoneyPayoutFactor(8);
        public static readonly PayoutFactor TwentyFive = new MoneyPayoutFactor(10);
        public static readonly PayoutFactor Fifty = new MoneyPayoutFactor(20);
        public static readonly PayoutFactor SeventyFive = new MoneyPayoutFactor(30);
        public static readonly PayoutFactor OneHundred = new MoneyPayoutFactor(40);

        private MoneyPayoutFactor(int value) : base(value)
        {
        }
    }
}
