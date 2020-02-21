// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IOptions<CashierAppSettings> _options;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public UserCreatedIntegrationEventHandler(
            IOptionsSnapshot<CashierAppSettings> options,
            IAccountService accountService,
            ILogger<UserCreatedIntegrationEventHandler> logger
        )
        {
            _options = options;
            _accountService = accountService;
            _logger = logger;
        }

        private CashierAppSettings AppSettings => _options.Value;

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (!await _accountService.AccountExistsAsync(userId))
            {
                var result = await _accountService.CreateAccountAsync(userId);

                if (result.IsValid)
                {
                    if (AppSettings?.IntegrationEvent?.UserCreated?.Promotion?.Enabled ?? false)
                    {
                        var account = await _accountService.FindAccountAsync(userId);

                        await _accountService.CreateTransactionAsync(
                            account,
                            AppSettings.IntegrationEvent.UserCreated.Promotion.Currency.Type.ToEnumeration<CurrencyType>()
                                .ToCurrency(AppSettings.IntegrationEvent.UserCreated.Promotion.Currency.Amount),
                            TransactionType.Promotion);
                    }

                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogError(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}
