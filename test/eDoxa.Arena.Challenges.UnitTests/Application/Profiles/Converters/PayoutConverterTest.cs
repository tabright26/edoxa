// Filename: PayoutConverterTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.ObjectModel;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Application.Profiles.Converters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Profiles.Converters
{
    [TestClass]
    public sealed class PayoutConverterTest
    {
        [TestMethod]
        public void Convert_BucketModels_ShouldBePayoutViewModel()
        {
            // Arrange
            var bucketModels = new Collection<BucketModel>
            {
                new BucketModel
                {
                    Size = 1,
                    PrizeCurrency = 1,
                    PrizeAmount = 10M
                },
                new BucketModel
                {
                    Size = 2,
                    PrizeCurrency = 1,
                    PrizeAmount = 5M
                },
                new BucketModel
                {
                    Size = 4,
                    PrizeCurrency = 1,
                    PrizeAmount = 2.5M
                }
            };

            var converter = new PayoutConverter();

            // Act
            var payoutViewModel = converter.Convert(bucketModels, null);

            // Assert
            payoutViewModel.PrizePool.Amount.Should().Be(bucketModels.Sum(bucket => bucket.Size * bucket.PrizeAmount));
            payoutViewModel.PrizePool.Currency.Should().Be(Currency.FromValue(bucketModels.First().PrizeCurrency).Name);
        }
    }
}
