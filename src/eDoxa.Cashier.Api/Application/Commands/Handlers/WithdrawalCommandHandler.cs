// Filename: WithdrawalCommandHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class WithdrawalCommandHandler : AsyncCommandHandler<WithdrawalCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IUserQuery _userQuery;

        public WithdrawalCommandHandler(IHttpContextAccessor httpContextAccessor, IAccountService accountService, IUserQuery userQuery)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _userQuery = userQuery;
        }

        protected override async Task Handle([NotNull] WithdrawalCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userQuery.FindUserAsync(userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            await _accountService.WithdrawalAsync(user, new Money(command.Amount), cancellationToken);
        }
    }
}
