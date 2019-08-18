// Filename: IDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Infrastructure
{
    public interface IDbContextCleaner
    {
        Task CleanupAsync();
    }
}
