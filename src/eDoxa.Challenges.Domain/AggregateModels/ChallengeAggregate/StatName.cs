// Filename: StatName.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatName : ValueObject, IComparable
    {
        private readonly string _name;

        public StatName(PropertyInfo propertyInfo) : this(propertyInfo.GetMethod.Name.Substring(4))
        {
        }

        public StatName(string name)
        {
            _name = name;
        }

        public StatName(Game game)
        {
            _name = game.Name;
        }

        public int CompareTo(object? obj)
        {
            return string.Compare(_name, (obj as StatName)?._name, StringComparison.OrdinalIgnoreCase);
        }

        public static implicit operator string(StatName name)
        {
            return name._name;
        }

        public override string ToString()
        {
            return _name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _name;
        }
    }
}
