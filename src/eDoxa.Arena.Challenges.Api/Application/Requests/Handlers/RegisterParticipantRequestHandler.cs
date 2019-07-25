// Filename: RegisterParticipantRequestHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain.Providers;

using JetBrains.Annotations;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Requests.Handlers
{
    public sealed class RegisterParticipantRequestHandler : AsyncRequestHandler<RegisterParticipantRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeService _challengeService;

        public RegisterParticipantRequestHandler(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] RegisterParticipantRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var registeredAt = new UtcNowDateTimeProvider();

            await _challengeService.RegisterParticipantAsync(request.ChallengeId, userId, registeredAt, cancellationToken);
        }
    }
}
