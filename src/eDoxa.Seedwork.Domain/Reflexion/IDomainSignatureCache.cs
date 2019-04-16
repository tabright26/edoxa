﻿// Filename: IDomainSignatureCache.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Reflexion
{
    public interface IDomainSignatureCache
    {
        [CanBeNull]
        DomainSignature Find(Type type);

        DomainSignature GetOrAdd(Type type, Func<Type, DomainSignature> factory);
    }
}