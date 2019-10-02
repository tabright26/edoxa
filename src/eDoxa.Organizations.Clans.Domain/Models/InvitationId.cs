// Filename: CandidatureId.cs
// Date Created: --
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Domain.Models
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class InvitationId : EntityId<InvitationId>
    {
    }
}
