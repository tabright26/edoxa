// Filename: CandidatureId.cs
// Date Created: --
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class CandidatureId : EntityId<CandidatureId>
    {
    }
}
