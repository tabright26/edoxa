// Filename: AllowValueAttribute.cs
// Date Created: 2019-06-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Attributes
{
    public sealed class AllowValueAttribute : Attribute
    {
        public AllowValueAttribute(bool isAllowed)
        {
            IsAllowed = isAllowed;
        }

        public bool IsAllowed { get; set; }
    }
}
