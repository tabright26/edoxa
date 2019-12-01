// Filename: IDbContextCleaner.cs
// Date Created: 2019-12-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Application.SqlServer.Abstractions
{
    public interface IDbContextCleaner
    {
        Task CleanupAsync();
    }
}
