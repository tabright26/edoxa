// Filename: DomainSignature.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Reflexion
{
    public sealed class DomainSignature
    {
        private PropertyInfo[] _properties;
        private Type _type;

        public DomainSignature(Type type, PropertyInfo[] properties)
        {
            _type = type;
            _properties = properties.Any() ? properties : new PropertyInfo[0];
        }

        public Type Type => _type;

        public PropertyInfo[] Properties => _properties;

        public override bool Equals([CanBeNull] object obj)
        {
            if (!(obj is DomainSignature other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Type == other.Type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}