// Filename: PlayerId.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(StringIdTypeConverter))]
    public sealed class PlayerId : StringId<PlayerId>
    {
    }
}
