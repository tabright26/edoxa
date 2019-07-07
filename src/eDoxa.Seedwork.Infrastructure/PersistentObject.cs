// Filename: PersistentObject.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Infrastructure
{
    public abstract class PersistentObject
    {
        public Guid Id { get; set; }
    }
}
