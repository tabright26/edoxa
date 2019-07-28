// Filename: CreateUserRequestHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;

using MediatR;

namespace eDoxa.Cashier.Api.Application.Requests.Handlers
{
    public sealed class CreateUserRequestHandler : AsyncRequestHandler<CreateUserRequest>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateUserRequestHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        protected override async Task Handle( CreateUserRequest request, CancellationToken cancellationToken)
        {
            var account = new Account(request.UserId);

            _accountRepository.Create(account);

            await _accountRepository.CommitAsync(cancellationToken);
        }
    }
}
