// Filename: PhoneNumber.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public class Phone : ValueObject
    {
        public Phone(string number)
        {
            Number = number;
        }

        public string Number { get; }

        public bool Confirmed { get; private set; }

        public override string ToString()
        {
            return Number;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return Confirmed;
        }

        public void Confirm()
        {
            Confirmed = true;
        }
    }
}
