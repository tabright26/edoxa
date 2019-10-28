// Filename: ServiceInfo.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Games.Domain.AggregateModels
{
    public sealed class ServiceInfo : ValueObject
    {
        public ServiceInfo(string name, bool displayed, bool enabled)
        {
            Name = name;
            Displayed = displayed;
            Enabled = enabled;
        }

        public string Name { get; }

        public bool Displayed { get; }

        public bool Enabled { get; }

        public override string ToString()
        {
            return Name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Displayed;
            yield return Enabled;
        }
    }
}
