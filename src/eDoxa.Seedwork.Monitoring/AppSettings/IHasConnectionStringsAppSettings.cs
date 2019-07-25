// Filename: IHasConnectionStringsAppSettings.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasConnectionStringsAppSettings
    {
        ConnectionStrings ConnectionStrings { get; set; }
    }
}
