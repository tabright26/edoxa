// Filename: IDbContextSeeder.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Application.SqlServer.Abstractions
{
    public interface IDbContextSeeder
    {
        Task SeedAsync();
    }
}
