// Filename: WithdrawalMoneyCommandHandler.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.DTO;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class WithdrawalMoneyCommandHandler : ICommandHandler<WithdrawalMoneyCommand, IActionResult>
    {
        private readonly IMapper _mapper;
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserInfoService _userInfoService;

        public WithdrawalMoneyCommandHandler(IUserInfoService userInfoService, IMoneyAccountService moneyAccountService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _moneyAccountService = moneyAccountService;
            _mapper = mapper;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] WithdrawalMoneyCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var moneyTransaction = await _moneyAccountService.TryWithdrawalAsync(userId, command.Amount, cancellationToken);

            return moneyTransaction
                .Select(transaction => new OkObjectResult(_mapper.Map<MoneyTransactionDTO>(transaction)))
                .Cast<IActionResult>()
                .DefaultIfEmpty(new BadRequestResult())
                .Single();
        }
    }
}