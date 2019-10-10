// Filename: StripePersonService.cs
// Date Created: 2019-10-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripePersonService : PersonService, IStripePersonService
    {
        public async Task<Person> CreatePersonAsync(string accountId, string personToken)
        {
            return await this.CreateAsync(
                accountId,
                new PersonCreateOptions
                {
                    PersonToken = personToken
                });
        }

        public async Task<Person> UpdatePersonAsync(string accountId, string personId, string personToken)
        {
            return await this.UpdateAsync(
                accountId,
                personId,
                new PersonUpdateOptions
                {
                    PersonToken = personToken
                });
        }
    }
}
