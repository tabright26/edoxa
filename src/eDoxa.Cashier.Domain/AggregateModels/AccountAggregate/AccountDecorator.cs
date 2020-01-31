// Filename: AccountDecorator.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public abstract class AccountDecorator : IAccount
    {
        private readonly IAccount _account;

        protected AccountDecorator(IAccount account)
        {
            _account = account;
        }

        public IReadOnlyCollection<ITransaction> Transactions => _account.Transactions;

        public void CreateTransaction(ITransaction transaction)
        {
            _account.CreateTransaction(transaction);
        }

        public bool TransactionExists(TransactionId transactionId)
        {
            return _account.TransactionExists(transactionId);
        }

        public bool TransactionExists(TransactionMetadata metadata)
        {
            return _account.TransactionExists(metadata);
        }

        public ITransaction FindTransaction(TransactionId transactionId)
        {
            return _account.FindTransaction(transactionId);
        }

        public ITransaction FindTransaction(TransactionMetadata metadata)
        {
            return _account.FindTransaction(metadata);
        }

        public Balance GetBalanceFor(CurrencyType currencyType)
        {
            return _account.GetBalanceFor(currencyType);
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _account.DomainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _account.AddDomainEvent(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _account.ClearDomainEvents();
        }

        public UserId Id => _account.Id;

        public void SetEntityId(UserId entityId)
        {
            _account.SetEntityId(entityId);
        }
    }
}
