﻿// Filename: UserStripePaymentIntentSucceededIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserStripePaymentIntentSucceededIntegrationEventHandler : IIntegrationEventHandler<UserStripePaymentIntentSucceededIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public UserStripePaymentIntentSucceededIntegrationEventHandler(
            IAccountService accountService,
            IMapper mapper,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<UserStripePaymentIntentSucceededIntegrationEventHandler> logger
        )
        {
            _accountService = accountService;
            _mapper = mapper;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UserStripePaymentIntentSucceededIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            var transactionId = integrationEvent.TransactionId.ParseEntityId<TransactionId>();

            if (await _accountService.AccountExistsAsync(userId))
            {
                var account = await _accountService.FindAccountAsync(userId);

                var result = await _accountService.MarkAccountTransactionAsSucceededAsync(account, transactionId);

                if (result.IsValid)
                {
                    await _serviceBusPublisher.PublishUserDepositSucceededIntegrationEventAsync(userId, _mapper.Map<TransactionDto>(result.Response));

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
