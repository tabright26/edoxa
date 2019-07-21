// Filename: DepositCommandHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class DepositCommandHandler : AsyncCommandHandler<DepositCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;

        public DepositCommandHandler(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }

        protected override async Task Handle([NotNull] DepositCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var customerId = _httpContextAccessor.GetCustomerId();

            await _accountService.DepositAsync(customerId, userId, MapCurrency(command.Currency, command.Amount), cancellationToken);
        }

        // TODO: Must be Currency static method...
        private static ICurrency MapCurrency(string currency, decimal amount)
        {
            if (Currency.FromName(currency) == Currency.Money)
            {
                return new Money(amount);
            }

            if (Currency.FromName(currency) == Currency.Token)
            {
                return new Token(amount);
            }

            throw new InvalidOperationException();
        }
    }
}
