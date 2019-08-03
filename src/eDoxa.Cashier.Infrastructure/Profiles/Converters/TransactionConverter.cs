// Filename: TransactionConverter.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class TransactionConverter : ITypeConverter<TransactionModel, ITransaction>
    {
        
        public ITransaction Convert( TransactionModel source,  ITransaction destination,  ResolutionContext context)
        {
            var transaction = new Transaction(
                Convert(source.Amount, Currency.FromValue(source.Currency)!),
                new TransactionDescription(source.Description),
                TransactionType.FromValue(source.Type)!,
                new DateTimeProvider(source.Timestamp)
            );

            transaction.SetEntityId(TransactionId.FromGuid(source.Id));

            var status = TransactionStatus.FromValue(source.Status);

            if (status == TransactionStatus.Succeded)
            {
                transaction.MarkAsSucceded();
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
