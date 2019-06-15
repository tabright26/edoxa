// Filename: DepositCommandHandler.cs
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

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.Abstractions.Services;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class DepositCommandHandler : ICommandHandler<DepositCommand, TransactionViewModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DepositCommandHandler(
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
        public async Task<TransactionViewModel> Handle([NotNull] DepositCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            var transaction = await _accountService.DepositAsync(user.Id, _mapper.Map<Currency>(command.Currency), cancellationToken);

            return _mapper.Map<TransactionViewModel>(transaction);
        }
    }
}
