// Filename: IStripePersonService.cs
// Date Created: 2019-10-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public interface IStripePersonService
    {
        Task<Person> CreatePersonAsync(string accountId, string personToken);

        Task<Person> UpdatePersonAsync(string accountId, string personId, string personToken);
    }
}
