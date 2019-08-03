// Filename: IHasAuthorityAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasAuthorityAppSettings
    {
        AuthorityOptions Authority { get; set; }
    }
}
