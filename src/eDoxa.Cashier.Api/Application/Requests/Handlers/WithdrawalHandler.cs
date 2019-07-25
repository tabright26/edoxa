// Filename: WithdrawalHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;

using JetBrains.Annotations;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Requests.Handlers
{
    public sealed class WithdrawalHandler : AsyncRequestHandler<WithdrawalRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;

        public WithdrawalHandler(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }

        protected override async Task Handle([NotNull] WithdrawalRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var connectAccountId = _httpContextAccessor.GetConnectAccountId();

            await _accountService.WithdrawalAsync(connectAccountId, userId, new Money(request.Amount), cancellationToken);
        }
    }
}
