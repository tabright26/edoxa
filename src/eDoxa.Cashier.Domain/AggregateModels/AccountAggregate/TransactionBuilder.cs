// Filename: TransactionBuilder.cs
// Date Created: 2019-12-26
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class TransactionBuilder : ITransactionBuilder
    {
        // Francis: Refactor nécessaire, FormatCurrencyByType peux throw un exception avant que GetDefault peux throw la sienne. Bref, l'exception
        // de GetDefaultDescription est impossible a throw parce que Format la throw avant.
        public TransactionBuilder(TransactionType type, Currency currency)
        {
            Type = type;
            Currency = FormatCurrencyByType(type, currency);
            Description = GetDefaultDescriptionByType(type, currency);
            Provider = new UtcNowDateTimeProvider();
            Metadata = null;
        }

        private TransactionBuilder(
            Currency currency,
            TransactionType type,
            TransactionDescription description,
            IDateTimeProvider provider,
            TransactionMetadata? metadata
        )
        {
            Currency = currency;
            Type = type;
            Description = description;
            Provider = provider;
            Metadata = metadata;
        }

        private Currency Currency { get; }

        private TransactionType Type { get; }

        private TransactionDescription Description { get; }

        private IDateTimeProvider Provider { get; }

        private TransactionMetadata? Metadata { get; }

        public ITransaction Build()
        {
            return new Transaction(
                Currency,
                Description,
                Type,
                Provider,
                Metadata);
        }

        public ITransactionBuilder WithDescription(TransactionDescription description)
        {
            return new TransactionBuilder(
                Currency,
                Type,
                description,
                Provider,
                Metadata);
        }

        public ITransactionBuilder WithProvider(IDateTimeProvider provider)
        {
            return new TransactionBuilder(
                Currency,
                Type,
                Description,
                provider,
                Metadata);
        }

        public ITransactionBuilder WithMetadata(TransactionMetadata? metadata)
        {
            return metadata != null
                ? new TransactionBuilder(
                    Currency,
                    Type,
                    Description,
                    Provider,
                    metadata)
                : this;
        }

        private static TransactionDescription GetDefaultDescriptionByType(TransactionType type, Currency currency)
        {
            if (currency is Money money)
            {
                if (TransactionType.Charge == type)
                {
                    return new TransactionDescription($"{money}");
                }

                if (TransactionType.Deposit == type)
                {
                    return new TransactionDescription($"{money}");
                }

                if (TransactionType.Payout == type)
                {
                    return new TransactionDescription($"{money}");
                }

                if (TransactionType.Withdraw == type)
                {
                    return new TransactionDescription($"{money}");
                }

                if (TransactionType.Promotion == type)
                {
                    return new TransactionDescription($"{money}");
                }
            }

            if (currency is Token token)
            {
                if (TransactionType.Charge == type)
                {
                    return new TransactionDescription($"{token} tokens.");
                }

                if (TransactionType.Deposit == type)
                {
                    return new TransactionDescription($"{token} tokens.");
                }

                if (TransactionType.Payout == type)
                {
                    return new TransactionDescription($"{token} tokens.");
                }

                if (TransactionType.Reward == type)
                {
                    return new TransactionDescription($"{token} tokens.");
                }

                if (TransactionType.Promotion == type)
                {
                    return new TransactionDescription($"{token} tokens.");
                }
            }

            // Francis: Impossible de se rendre ici.
            throw InvalidOperationException(type, currency);
        }

        private static Currency FormatCurrencyByType(TransactionType type, Currency currency)
        {
            if (currency is Money money)
            {
                if (TransactionType.Deposit == type || TransactionType.Payout == type || TransactionType.Promotion == type)
                {
                    return money;
                }

                if (TransactionType.Charge == type || TransactionType.Withdraw == type)
                {
                    return -money;
                }
            }

            if (currency is Token token)
            {
                if (TransactionType.Deposit == type || TransactionType.Payout == type || TransactionType.Reward == type || TransactionType.Promotion == type)
                {
                    return token;
                }

                if (TransactionType.Charge == type)
                {
                    return -token;
                }
            }

            throw InvalidOperationException(type, currency);
        }

        private static InvalidOperationException InvalidOperationException(TransactionType type, Currency currency)
        {
            return new InvalidOperationException($"The {type} transaction type isn't supported for the {currency.Type} currency.");
        }
    }
}
