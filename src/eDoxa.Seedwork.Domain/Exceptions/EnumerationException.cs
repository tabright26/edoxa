// Filename: EnumerationException.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Exceptions
{
    internal class EnumerationException : Exception
    {
        public EnumerationException(Type enumerationType, int value, ArgumentNullException exception) : base(
            $"'{value}' is not a valid value in {enumerationType}.", exception)
        {
        }

        public EnumerationException(Type enumerationType, int value, InvalidOperationException exception) : base(
            $"'{value}' is not a valid value in {enumerationType}.", exception)
        {
        }

        public EnumerationException(Type enumerationType, string name, ArgumentNullException exception) : base(
            $"'{name}' is not a valid name in {enumerationType}.", exception)
        {
        }

        public EnumerationException(Type enumerationType, string name, InvalidOperationException exception) : base(
            $"'{name}' is not a valid name in {enumerationType}.", exception)
        {
        }
    }
}