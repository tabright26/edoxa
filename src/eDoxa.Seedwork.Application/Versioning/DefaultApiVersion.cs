﻿// Filename: DefaultApiVersion.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Seedwork.Application.Versioning
{
    public sealed class DefaultApiVersion : ApiVersion
    {
        public DefaultApiVersion() : base(1, 0)
        {
        }
    }
}