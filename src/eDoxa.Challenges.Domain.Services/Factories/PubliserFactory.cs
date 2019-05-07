﻿// Filename: ChallengePubliserFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Enumerations;

namespace eDoxa.Challenges.Domain.Services.Factories
{
    public sealed class PubliserFactory
    {
        private static readonly Lazy<PubliserFactory> Lazy = new Lazy<PubliserFactory>(() => new PubliserFactory());

        public static PubliserFactory Instance => Lazy.Value;

        public IPublisherStrategy CreatePublisherStrategy(PublisherInterval interval, Game game)
        {
            switch (game)
            {
                case Game.LeagueOfLegends:

                    return LeagueOfLegendsChallengePublisherFactory.Instance.CreatePublisher(interval);

                default:

                    throw new NotImplementedException();
            }
        }
    }
}