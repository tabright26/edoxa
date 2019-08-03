// Filename: IHasApiResourceAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Monitoring.AppSettings
{
    public interface IHasApiResourceAppSettings : IHasAuthorityAppSettings
    {
        ApiResource ApiResource { get; set; }
    }
}
