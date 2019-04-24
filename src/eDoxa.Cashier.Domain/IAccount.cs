using System;
using System.Collections.Generic;
using System.Text;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Domain
{
    public interface IAccount<TCurrency>
    where TCurrency : Currency<TCurrency>
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        void AddBalance(TCurrency currency);

        void AddPending(TCurrency currency);

        void SubtractBalance(TCurrency currency);

        void SubtractPending(TCurrency currency);
    }
}
