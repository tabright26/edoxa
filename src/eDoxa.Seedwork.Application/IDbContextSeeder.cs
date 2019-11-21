// Filename: IDbContextSeeder.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Application
{
    public interface IDbContextSeeder
    {
        Task SeedAsync();
    }
}
