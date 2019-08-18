// Filename: CloseChallengesRequestHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.Requests.Handlers
{
    public sealed class CloseChallengesRequestHandler : AsyncRequestHandler<CloseChallengesRequest>
    {
        private readonly IChallengeService _challengeService;

        public CloseChallengesRequestHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle( CloseChallengesRequest request, CancellationToken cancellationToken)
        {
            await _challengeService.CloseAsync(new UtcNowDateTimeProvider(), cancellationToken);
        }
    }
}
