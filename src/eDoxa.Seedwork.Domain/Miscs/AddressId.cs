// Filename: AddressId.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class AddressId : EntityId<AddressId>
    {
    }
}
