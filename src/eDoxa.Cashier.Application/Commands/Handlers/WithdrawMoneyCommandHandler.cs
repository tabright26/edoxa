// Filename: WithdrawalCommandHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;
using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand, decimal>
    {
        private readonly IUserRepository _userRepository;

        public WithdrawMoneyCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<decimal> Handle([NotNull] WithdrawMoneyCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var money = new Money(command.Amount);

            var transaction = user.WithdrawMoney(money).Single();

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return transaction.Amount;
        }
    }
}