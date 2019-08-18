// Filename: IDateTimeProvider.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain
{
    public interface IDateTimeProvider
    {
        DateTime DateTime { get; }
    }
}
