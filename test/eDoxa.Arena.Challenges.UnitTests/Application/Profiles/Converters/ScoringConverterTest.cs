// Filename: ScoringConverterTest.cs
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
using eDoxa.Arena.Challenges.Infrastructure.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Profiles.Converters
{
    [TestClass]
    public sealed class ScoringConverterTest
    {
        [TestMethod]
        public void Convert_ScoringItemModels_ShouldBeScoringViewModel()
        {
            // Arrange
            var scoringItemModels = new Collection<ScoringItemModel>
            {
                new ScoringItemModel
                {
                    Name = "Item1",
                    Weighting = 1
                },
                new ScoringItemModel
                {
                    Name = "Item2",
                    Weighting = 2
                },
                new ScoringItemModel
                {
                    Name = "Item3",
                    Weighting = 3
                }
            };

            var scoringConverter = new ScoringConverter();

            // Act
            var scoringViewModel = scoringConverter.Convert(scoringItemModels, null);

            // Assert
            scoringViewModel.Should().ContainKeys(scoringItemModels.Select(scoringItemModel => scoringItemModel.Name).ToList());
            scoringViewModel.Should().ContainValues(scoringItemModels.Select(scoringItemModel => scoringItemModel.Weighting).ToList());
        }
    }
}
