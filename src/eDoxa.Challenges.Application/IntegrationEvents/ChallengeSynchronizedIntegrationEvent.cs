// Filename: ChallengeSynchronizedIntegrationEvent.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.ServiceBus;

namespace eDoxa.Challenges.Application.IntegrationEvents
{
    public sealed class ChallengeSynchronizedIntegrationEvent : IntegrationEvent
    {
        public ChallengeSynchronizedIntegrationEvent(Game game)
        {
            Game = game;
        }

        public Game Game { get; private set; }
    }
}