// Filename: Token.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public sealed partial class Token : ICurrency
    {
        public static readonly Token Zero = new Token(0);
        public static readonly Token FiftyThousand = new Token(50000);
        public static readonly Token OneHundredThousand = new Token(100000);
        public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        public static readonly Token FiveHundredThousand = new Token(500000);
        public static readonly Token OneMillion = new Token(1000000);
        public static readonly Token FiveMillions = new Token(5000000);

        private readonly long _value;

        public Token(long amount)
        {
            _value = amount;
        }

        public static implicit operator long(Token token)
        {
            return token._value;
        }

        public static implicit operator decimal(Token token)
        {
            return token._value;
        }

        public static Token operator -(Token token)
        {
            return new Token(-token._value);
        }

        public override string ToString()
        {
            return Convert.ToInt32(this).ToString();
        }
    }

    public sealed partial class Token : IEquatable<Token>
    {
        public bool Equals(Token other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Token);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class Token : IComparable, IComparable<Token>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Token);
        }

        public int CompareTo([CanBeNull] Token other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}