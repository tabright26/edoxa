// Filename: WithdrawCommandHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public WithdrawCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [ItemNotNull]
        public async Task<TransactionViewModel> Handle([NotNull] WithdrawCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            var transaction = await _accountService.WithdrawAsync(user.Id, new Money(command.Amount), cancellationToken);

            return _mapper.Map<TransactionViewModel>(transaction);
        }
    }
}
