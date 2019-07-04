// Filename: CreateUserCommandHandler.cs
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

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        protected override async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var account = new Account(command.UserId);

            _accountRepository.Create(account);

            await _accountRepository.CommitAsync(cancellationToken);
        }
    }
}
