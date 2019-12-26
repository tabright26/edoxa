// Filename: Doxatag.cs
// Date Created: 2019-11-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate
{
    public sealed class Doxatag : Entity<DoxatagId>, IAggregateRoot
    {
        public Doxatag(
            UserId userId,
            string name,
            int code,
            IDateTimeProvider provider
        ) : this()
        {
            UserId = userId;
            Name = name;
            Code = code;
            Timestamp = provider.DateTime;
        }

        #nullable disable

        private Doxatag()
        {
            // Required by EF Core.
        }

        #nullable restore

        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public int Code { get; private set; }

        public DateTime Timestamp { get; private set; }

        public static int GenerateUniqueCode(IImmutableSet<int> codes)
        {
            return codes.Any() ? GenerateCode(codes) : GenerateCode();
        }

        private static int GenerateCode(IImmutableSet<int> codes)
        {
            while (true)
            {
                var code = GenerateCode();

                if (codes.Contains(code))
                {
                    continue;
                }

                return code;
            }
        }

        private static int GenerateCode()
        {
            var random = new Random();

            return random.Next(100, 10000);
        }

        public override string ToString()
        {
            return $"{Name}#{Code}";
        }
    }
}
