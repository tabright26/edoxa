// Filename: WithdrawCommandHandler.cs
// Date Created: 2019-06-08
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

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class WithdrawCommandHandler : ICommandHandler<WithdrawCommand, TransactionViewModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IUserQuery _userQuery;
        private readonly IMapper _mapper;

        public WithdrawCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IUserQuery userQuery,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _userQuery = userQuery;
            _mapper = mapper;
        }

        [ItemNotNull]
        public async Task<TransactionViewModel> Handle([NotNull] WithdrawCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userQuery.FindUserAsync(userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            var transaction = await _accountService.WithdrawAsync(user.Id, new Money(command.Amount), cancellationToken);

            return _mapper.Map<TransactionViewModel>(transaction);
        }
    }
}
