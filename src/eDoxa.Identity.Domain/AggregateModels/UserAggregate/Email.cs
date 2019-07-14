// Filename: Email.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;
        }

        public string Address { get; }

        public bool Confirmed { get; private set; }

        public override string ToString()
        {
            return Address;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
            yield return Confirmed;
        }

        public void Confirm()
        {
            Confirmed = true;
        }
    }
}
