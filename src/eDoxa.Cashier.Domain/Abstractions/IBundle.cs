﻿// Filename: IBundle.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IBundle
    {
        Money Price { get; }
    }
}
