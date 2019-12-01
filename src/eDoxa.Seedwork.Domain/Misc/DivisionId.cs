// Filename: DivisionId.cs
// Date Created: 2019-10-31
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Misc
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class DivisionId : EntityId<DivisionId>
    {
    }
}
