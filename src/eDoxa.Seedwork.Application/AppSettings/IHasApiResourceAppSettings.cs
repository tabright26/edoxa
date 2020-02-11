// Filename: IHasApiResourceAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Application.AppSettings
{
    public interface IHasApiResourceAppSettings
    {
        ApiResource ApiResource { get; set; }
    }

    public interface IHasApiResourceAppSettings<TEndpointsOptions> : IHasApiResourceAppSettings, IHasAuthorityAppSettings<TEndpointsOptions>
    where TEndpointsOptions : AuthorityEndpointsOptions
    {
    }
}
