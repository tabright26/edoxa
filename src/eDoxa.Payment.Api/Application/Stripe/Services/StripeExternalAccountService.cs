﻿//// Filename: StripeExternalAccountService.cs
//// Date Created: 2019-12-15
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Linq;
//using System.Threading.Tasks;

//using eDoxa.Payment.Domain.Stripe.Services;

//using Stripe;

//namespace eDoxa.Payment.Api.Application.Stripe.Services
//{
//    public sealed class StripeExternalAccountService : ExternalAccountService, IStripeExternalAccountService
//    {
//        public async Task<IExternalAccount?> FindBankAccountAsync(string accountId)
//        {
//            var bankAccounts = await this.ListAsync(
//                accountId,
//                new ExternalAccountListOptions
//                {
//                    ExtraParams =
//                    {
//                        ["object"] = "bank_account"
//                    }
//                });

//            return bankAccounts.FirstOrDefault();
//        }

//        public async Task<IExternalAccount> ChangeBankAccountAsync(string accountId, string token)
//        {
//            return await this.CreateAsync(
//                accountId,
//                new ExternalAccountCreateOptions
//                {
//                    ExternalAccount = token,
//                    DefaultForCurrency = true
//                });
//        }
//    }
//}
