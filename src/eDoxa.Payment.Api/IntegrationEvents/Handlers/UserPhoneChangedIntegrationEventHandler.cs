// Filename: UserPhoneChangedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserPhoneChangedIntegrationEventHandler : IIntegrationEventHandler<UserPhoneChangedIntegrationEvent>
    {
        //private readonly IStripeService _stripeService;
        //private readonly IStripeAccountService _stripeAccountService;
        //private readonly ILogger _logger;

        //public UserPhoneChangedIntegrationEventHandler(
        //    IStripeService stripeService,
        //    IStripeAccountService stripeAccountService,
        //    ILogger<UserPhoneChangedIntegrationEventHandler> logger
        //)
        //{
        //    _stripeService = stripeService;
        //    _stripeAccountService = stripeAccountService;
        //    _logger = logger;
        //}

        public async Task HandleAsync(UserPhoneChangedIntegrationEvent integrationEvent)
        {
            //var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            //if (await _stripeService.UserExistsAsync(userId))
            //{
            //    var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

            //    var result = await _stripeAccountService.UpdateIndividualAsync(
            //        accountId,
            //        new PersonUpdateOptions
            //        {
            //            Phone = integrationEvent.Phone.Number
            //        });

            //    if (result.IsValid)
            //    {
            //        _logger.LogInformation(""); // FRANCIS: TODO.
            //    }
            //    else
            //    {
            //        _logger.LogCritical(""); // FRANCIS: TODO.
            //    }
            //}
            //else
            //{
            //    _logger.LogCritical(""); // FRANCIS: TODO.
            //}
        }
    }
}
