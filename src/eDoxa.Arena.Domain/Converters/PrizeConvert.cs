// Filename: CurrencyConvert.cs
// Date Created: 2019-05-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Domain.Converters
{
    public static class PrizeConvert
    {
        private const decimal Factor = 1000M;

        public static MoneyPrize ToMoneyPrize(TokenPrize tokenPrize)
        {
            return new MoneyPrize(tokenPrize / Factor);
        }

        public static TokenPrize ToTokenPrize(MoneyPrize tokenPrize)
        {
            return new TokenPrize(tokenPrize * Factor);
        }
    }
}
