// Filename: IEnumeration.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain
{
    public interface IEnumeration
    {
        int Value { get; }

        string Name { get; }
    }
}