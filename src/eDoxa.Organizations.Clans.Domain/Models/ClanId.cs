// Filename: AccountId.cs
// Date Created: 2019-06-25
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class ClanId : EntityId<ClanId>
    {
    }
}
