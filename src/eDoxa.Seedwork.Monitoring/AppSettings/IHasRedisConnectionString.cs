// Filename: IHasRedisConnectionString.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasRedisConnectionString
    {
        string Redis { get; set; }
    }
}
