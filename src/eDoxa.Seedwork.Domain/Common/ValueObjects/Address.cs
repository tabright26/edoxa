// Filename: Address.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Text;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Common.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public static readonly Address Default = new Address();

        public Address(string city, string country, string line1, string line2, string postalCode, string state)
        {
            City = city != "null" ? city?.Trim() : null;
            Country = country != "null" ? country?.Trim() : null;
            Line1 = line1 != "null" ? line1?.Trim() : null;
            Line2 = line2 != "null" ? line2?.Trim() : null;
            PostalCode = postalCode != "null" ? postalCode?.Trim() : null;
            State = state != "null" ? state?.Trim() : null;
        }

        private Address()
        {
            City = null;
            Country = null;
            Line1 = null;
            Line2 = null;
            PostalCode = null;
            State = null;
        }

        /// <summary>
        ///     City/District/Suburb/Town/Village.
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        ///     Address country.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        ///     Address line 1 (Street address/PO Box/Company name).
        /// </summary>
        public string Line1 { get; private set; }

        /// <summary>
        ///     Address line 2 (Apartment/Suite/Unit/Building).
        /// </summary>
        public string Line2 { get; private set; }

        /// <summary>
        ///     ZIP or postal code.
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        ///     State/County/Province/Region.
        /// </summary>
        public string State { get; private set; }

        public void Edit(string city, string country, string line1, string line2, string postalCode, string state)
        {
            City = city != "null" ? city?.Trim() : null;
            Country = country != "null" ? country?.Trim() : null;
            Line1 = line1 != "null" ? line1?.Trim() : null;
            Line2 = line2 != "null" ? line2?.Trim() : null;
            PostalCode = postalCode != "null" ? postalCode?.Trim() : null;
            State = state != "null" ? state?.Trim() : null;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(Line1 + " \n");

            if (Line2 != null)
            {
                builder.Append(Line2 + " \n");
            }

            var queue = new Queue<string>();

            if (City != null)
            {
                queue.Enqueue(City);
            }

            if (State != null)
            {
                queue.Enqueue(State);
            }

            if (PostalCode != null)
            {
                queue.Enqueue(PostalCode);
            }

            if (Country != null)
            {
                queue.Enqueue(Country);
            }

            builder.Append(string.Join(", ", queue.ToArray()));

            return builder.ToString();
        }
    }
}