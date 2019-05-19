// Filename: TransactionDescription.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain
{
    public sealed class TransactionDescription
    {
        private string _value;

        public TransactionDescription(string description)
        {
            _value = description;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
