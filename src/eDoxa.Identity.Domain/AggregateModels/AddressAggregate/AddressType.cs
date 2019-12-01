// Filename: AddressType.cs
// Date Created: 2019-08-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Identity.Domain.AggregateModels.AddressAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class AddressType : Enumeration<AddressType>
    {
        public static readonly AddressType Principal = new AddressType(1, nameof(Principal));
        public static readonly AddressType Billing = new AddressType(1 << 1, nameof(Billing));
        public static readonly AddressType Delivery = new AddressType(1 << 2, nameof(Delivery));

        public AddressType()
        {
        }

        private AddressType(int value, string name) : base(value, name)
        {
        }
    }
}
