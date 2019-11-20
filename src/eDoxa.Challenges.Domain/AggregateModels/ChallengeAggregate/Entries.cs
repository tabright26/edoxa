// Filename: Entries.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Entries : ValueObject
    {
        public static readonly Entries Two = new Entries(2);
        public static readonly Entries Four = new Entries(4);
        public static readonly Entries Six = new Entries(6);
        public static readonly Entries Eight = new Entries(8);
        public static readonly Entries Ten = new Entries(10);
        public static readonly Entries Twenty = new Entries(20);
        public static readonly Entries Thirty = new Entries(30);
        public static readonly Entries Forty = new Entries(40);
        public static readonly Entries Fifty = new Entries(50);
        public static readonly Entries OneHundred = new Entries(100);
        public static readonly Entries OneHundredFifty = new Entries(150);
        public static readonly Entries TwoHundred = new Entries(200);

        private readonly int _entries;

        public Entries(int entries)
        {
            _entries = entries;
        }

        public static implicit operator int(Entries entries)
        {
            return entries._entries;
        }

        public override string ToString()
        {
            return _entries.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _entries;
        }
    }
}
