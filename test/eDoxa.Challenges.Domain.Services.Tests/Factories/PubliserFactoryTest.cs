// Filename: PubliserFactoryTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

//using System;

//using eDoxa.Challenges.Domain.Entities;
//using eDoxa.Challenges.Domain.Services.Factories;
//using eDoxa.Seedwork.Enumerations;

//using FluentAssertions;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace eDoxa.Challenges.Domain.Services.Tests.Factories
//{
//    [TestClass]
//    public sealed class PubliserFactoryTest
//    {
//        private static readonly PubliserFactory PubliserFactory = PubliserFactory.Instance;

//        [DataRow(PublisherInterval.Daily, Game.LeagueOfLegends)]
//        [DataRow(PublisherInterval.Weekly, Game.LeagueOfLegends)]
//        [DataRow(PublisherInterval.Monthly, Game.LeagueOfLegends)]
//        [DataTestMethod]
//        public void Create_ImplementedType_ShouldNotBeNull(PublisherInterval periodicity, Game game)
//        {
//            // Act
//            var strategy = PubliserFactory.CreatePublisherStrategy(periodicity, game);

//            // Assert
//            strategy.Challenges.Should().NotBeNull();
//        }

//        [DataRow(PublisherInterval.Daily, Game.CSGO)]
//        [DataRow(PublisherInterval.Weekly, Game.CSGO)]
//        [DataRow(PublisherInterval.Monthly, Game.CSGO)]
//        [DataRow(PublisherInterval.Daily, Game.Fortnite)]
//        [DataRow(PublisherInterval.Weekly, Game.Fortnite)]
//        [DataRow(PublisherInterval.Monthly, Game.Fortnite)]
//        [DataTestMethod]
//        public void Create_NotImplementedType_ShouldThrowNotImplementedException(PublisherInterval periodicity, Game game)
//        {
//            // Act
//            var action = new Action(() => PubliserFactory.CreatePublisherStrategy(periodicity, game));

//            // Assert
//            action.Should().Throw<NotImplementedException>();
//        }
//    }
//}