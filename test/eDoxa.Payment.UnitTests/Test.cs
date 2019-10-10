// Filename: Test.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests
{
    public sealed class Test : UnitTest
    {
        public Test(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        //public async Task<Account> TestAccount(string email, PersonCreateOptions individual, AccountBankAccountOptions externalAccount)
        //{
        //    const string apiKey = "sk_test_xRMH8A7bagp2Auj7YPqNihlY";
        //    var accountService = new AccountService(new StripeClient(apiKey));

        //    await accountService.UpdateAsync("", new AccountUpdateOptions
        //    {
        //        Individual = new PersonUpdateOptions
        //        {
        //            FirstName = "Francis",
        //            LastName = "Quenneville",
        //            Dob = new DobOptions
        //            {
        //                Day = 6,
        //                Month = 5,
        //                Year = 1995
        //            },
        //            Address = new AddressOptions
        //            {
        //                Line1 = "1225 Notre-Dame W",
        //                City = "Montreal",
        //                Country = "CA",
        //                PostalCode = "H3C 6S3",
        //                State = "QC"
        //            },
        //            Email = email
        //        },
        //        Email = email,
        //        TosAcceptance = new AccountTosAcceptanceOptions
        //        {
        //            Date = DateTime.UtcNow,
        //            Ip = "10.10.10.10"
        //        },
        //        ExternalAccount = externalAccount,
        //        Settings = new AccountSettingsOptions
        //        {
        //            Payouts = new AccountSettingsPayoutsOptions
        //            {
        //                StatementDescriptor = "eDoxa Withdrawal"
        //            }
        //        }
        //    });

        //    return await accountService.CreateAsync(
        //        new AccountCreateOptions
        //        {
        //            Individual = individual,
        //            BusinessType = "individual",
        //            Type = "custom",
        //            Email = email,
        //            TosAcceptance = new AccountTosAcceptanceOptions
        //            {
        //                Date = DateTime.UtcNow,
        //                Ip = "10.10.10.10"
        //            },
        //            ExternalAccount = externalAccount,
        //            Settings = new AccountSettingsOptions
        //            {
        //                Payouts = new AccountSettingsPayoutsOptions
        //                {
        //                    StatementDescriptor = "eDoxa Withdrawal"
        //                }
        //            }
        //        });
        //}

        //[Fact]
        //public async Task Test1()
        //{
        //    var externalAccount = new AccountBankAccountOptions
        //    {
        //        AccountHolderName = "Francis Quenneville",
        //        Country = "CA",
        //        Currency = "cad",
        //        AccountNumber = "000123456789",
        //        RoutingNumber = "11000-000"
        //    };

        //    const string email = "francis@edoxa.gg";

        //    var account = await this.TestAccount(
        //        email,
        //        new PersonCreateOptions
        //        {
        //            FirstName = "Francis",
        //            LastName = "Quenneville",
        //            Dob = new DobOptions
        //            {
        //                Day = 6,
        //                Month = 5,
        //                Year = 1995
        //            },
        //            Address = new AddressOptions
        //            {
        //                Line1 = "1225 Notre-Dame W",
        //                City = "Montreal",
        //                Country = "CA",
        //                PostalCode = "H3C 6S3",
        //                State = "QC"
        //            },
        //            Email = email
        //        },
        //        externalAccount);
        //}
    }
}
