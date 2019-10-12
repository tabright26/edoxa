// Filename: ChallengeId.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class ChallengeId : EntityId<ChallengeId>
    {
    }
}
