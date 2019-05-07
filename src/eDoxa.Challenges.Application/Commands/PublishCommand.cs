// Filename: PublishChallengesCommand.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Challenges.Application.Commands
{
    public sealed class PublishCommand : Command
    {
        public PublishCommand(PublisherInterval interval)
        {
            Interval = interval;
        }

        public PublisherInterval Interval { get; }
    }
}