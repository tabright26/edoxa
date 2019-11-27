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
    public sealed class UserAddressType : Enumeration<UserAddressType>
    {
        public static readonly UserAddressType Principal = new UserAddressType(1, nameof(Principal));
        public static readonly UserAddressType Billing = new UserAddressType(1 << 1, nameof(Billing));
        public static readonly UserAddressType Delivery = new UserAddressType(1 << 2, nameof(Delivery));

        public UserAddressType()
        {
        }

        private UserAddressType(int value, string name) : base(value, name)
        {
        }
    }
}
