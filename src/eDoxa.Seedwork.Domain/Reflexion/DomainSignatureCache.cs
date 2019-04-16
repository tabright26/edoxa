// Filename: DomainSignatureCache.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Reflexion
{
    public sealed class DomainSignatureCache : IDomainSignatureCache
    {
        private readonly ConcurrentDictionary<Type, DomainSignature> _cache = new ConcurrentDictionary<Type, DomainSignature>();

        public int Count
        {
            get
            {
                return _cache.Count;
            }
        }

        public void Clear()
        {
            _cache.Clear();
        }
        
        [CanBeNull]
        public DomainSignature Find(Type type)
        {
            _cache.TryGetValue(type, out var result);

            return result;
        }

        public DomainSignature GetOrAdd(Type type, Func<Type, DomainSignature> factory)
        {
            return _cache.GetOrAdd(type, factory);
        }
    }
}