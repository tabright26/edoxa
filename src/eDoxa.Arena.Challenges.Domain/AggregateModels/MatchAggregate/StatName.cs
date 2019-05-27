// Filename: StatName.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class StatName : TypedObject<StatName, string>
    {
        public StatName(PropertyInfo propertyInfo)
        {
            Value = propertyInfo.GetMethod.Name.Substring(4);
        }

        public StatName(string name)
        {
            Value = name;
        }

        public static implicit operator StatName(string name)
        {
            return new StatName(name);
        }
    }
}
