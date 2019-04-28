// Filename: WithdrawMoneyCommandHandler.cs
// Date Created: 2019-04-26
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

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;

        public WithdrawMoneyCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] WithdrawMoneyCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var money = new Money(command.Amount);

            var transaction = user.WithdrawMoney(money).Single();

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkObjectResult(transaction.Amount);
        }
    }
}