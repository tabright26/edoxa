using System.Collections.Generic;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public class Account : Entity<AccountId>, IAccount
    {
        private HashSet<Transaction> _transactions;

        public Account(User user) : this()
        {
            User = user;
        }

        private Account()
        {
            _transactions = new HashSet<Transaction>();
        }

        public User User { get; private set; }

        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        public void CreateTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
