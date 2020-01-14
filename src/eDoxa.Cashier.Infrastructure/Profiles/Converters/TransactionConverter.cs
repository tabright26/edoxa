// Filename: TransactionConverter.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class TransactionConverter : ITypeConverter<TransactionModel, ITransaction>
    {
        public ITransaction Convert(TransactionModel source, ITransaction destination, ResolutionContext context)
        {
            var transaction = new Transaction(
                Convert(source.Amount, Currency.FromValue(source.Currency)!),
                new TransactionDescription(source.Description),
                TransactionType.FromValue(source.Type)!,
                new DateTimeProvider(source.Timestamp),
                new TransactionMetadata(source.Metadata.ToDictionary(metadata => metadata.Key, metadata => metadata.Value)));

            transaction.SetEntityId(TransactionId.FromGuid(source.Id));

            var status = TransactionStatus.FromValue(source.Status);

            if (status == TransactionStatus.Succeeded)
            {
                transaction.MarkAsSucceeded();
            }

            if (status == TransactionStatus.Failed)
            {
                transaction.MarkAsFailed();
            }

            return transaction;
        }

        private static ICurrency Convert(decimal amount, Currency currency)
        {
            if (currency == Currency.Money)
            {
                return new Money(amount);
            }

            return new Token(amount);
        }
    }
}
