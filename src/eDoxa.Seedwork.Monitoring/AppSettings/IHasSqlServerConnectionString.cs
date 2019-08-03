// Filename: IHasSqlServerConnectionString.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasSqlServerConnectionString
    {
        string SqlServer { get; set; }
    }
}
