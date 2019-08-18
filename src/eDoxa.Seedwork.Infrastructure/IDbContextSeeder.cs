// Filename: IDbContextSeeder.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Infrastructure
{
    public interface IDbContextSeeder
    {
        Task SeedAsync();

        void Cleanup();

        Task CleanupAsync();
    }
}
