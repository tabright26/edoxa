// Filename: UserInformationChangedIntegrationEventHandler.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Domain.Stripe.Extensions;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserInformationChangedIntegrationEventHandler : IIntegrationEventHandler<UserInformationChangedIntegrationEvent>
    {
        private readonly IStripeAccountService _stripeAccountService;

        public UserInformationChangedIntegrationEventHandler(IStripeAccountService stripeAccountService)
        {
            _stripeAccountService = stripeAccountService;
        }

        public async Task HandleAsync(UserInformationChangedIntegrationEvent integrationEvent)
        {
            var accountId = await _stripeAccountService.GetAccountIdAsync(integrationEvent.UserId.ParseEntityId<UserId>());

            await _stripeAccountService.UpdateIndividualAsync(
                accountId,
                new PersonUpdateOptions
                {
                    FirstName = integrationEvent.FirstName,
                    LastName = integrationEvent.LastName,
                    Gender = integrationEvent.Gender.ToEnumerationOrDefault<Gender>()?.ToStripe(),
                    Dob = new DobOptions
                    {
                        Day = integrationEvent.Dob.Day,
                        Month = integrationEvent.Dob.Month,
                        Year = integrationEvent.Dob.Year
                    }
                });
        }
    }
}
