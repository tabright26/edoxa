// Filename: IChallengeService.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.Services
{
    public interface IChallengeService
    {
        Task CompleteAsync(CancellationToken cancellationToken = default);

        Task PublishAsync(PublisherInterval interval, CancellationToken cancellationToken = default);

        Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default);
    }
}