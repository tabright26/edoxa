// Filename: DomainSignatureCache.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Concurrent;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Reflection
{
    public sealed class DomainSignatureCache : IDomainSignatureCache
    {
        private readonly ConcurrentDictionary<Type, DomainSignature> _cache = new ConcurrentDictionary<Type, DomainSignature>();

        public int Count => _cache.Count;

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

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
