// Filename: IDateTimeProvider.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain
{
    public interface IDateTimeProvider
    {
        DateTime DateTime { get; }
    }
}
