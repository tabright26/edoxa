// Filename: Maybe.cs
// Date Created: 2019-04-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections;
using System.Collections.Generic;

namespace eDoxa.Functional.Maybe
{
    public sealed class Option<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _values;

        public Option()
        {
            _values = new T[0];
        }

        public Option(T value)
        {
            _values = new[] {value};
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}