// Filename: BundleService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;

using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.Application.Services
{
    public sealed class BundleService : IBundleService
    {
        public BundleService(IOptions<TransactionBundlesOptions> options)
        {
            Options = options.Value;
        }

        private TransactionBundlesOptions Options { get; }

        public IImmutableSet<Bundle> FetchDepositMoneyBundles()
        {
            return Options.Deposit[Currency.Money.Name]
                .Select(bundle => new Bundle(new Money(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }

        public IImmutableSet<Bundle> FetchDepositTokenBundles()
        {
            return Options.Deposit[Currency.Token.Name]
                .Select(bundle => new Bundle(new Token(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }

        public IImmutableSet<Bundle> FetchWithdrawalMoneyBundles()
        {
            return Options.Withdrawal[Currency.Money.Name]
                .Select(bundle => new Bundle(new Money(bundle.Amount), new Price(new Money(bundle.Price))))
                .ToImmutableHashSet();
        }
    }
}
