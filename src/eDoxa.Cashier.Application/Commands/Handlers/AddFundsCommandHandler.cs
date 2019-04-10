﻿// Filename: AddFundsCommandHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class AddFundsCommandHandler : ICommandHandler<AddFundsCommand, decimal>
    {
        private readonly IUserRepository _userRepository;

        public AddFundsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<decimal> Handle(AddFundsCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var money = Money.FromDecimal(command.Amount);

            money = user.AddFunds(money);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return money.ToDecimal();
        }
    }
}