// Filename: DomainSignature.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Reflection;

namespace eDoxa.Seedwork.Domain.Reflexion
{
    public sealed class DomainSignature
    {
        private Type _type;
        private PropertyInfo[] _properties;

        public DomainSignature(Type type, PropertyInfo[] properties)
        {
            Type = type;
            Properties = properties;
        }

        public Type Type
        {
            get
            {
                return _type;
            }
            private set
            {
                _type = value ?? throw new ArgumentNullException(nameof(Type));
            }
        }

        public PropertyInfo[] Properties
        {
            get
            {
                return _properties;
            }
            private set
            {
                if (value != null && value.Any())
                {
                    _properties = value;
                }
                else
                {
                    _properties = new PropertyInfo[0];
                }
            }
        }

        public override bool Equals(object obj)
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