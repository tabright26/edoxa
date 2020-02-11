// Filename: IHasAuthorityAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.AppSettings.Options;

namespace eDoxa.Seedwork.Application.AppSettings
{
    public interface IHasAuthorityAppSettings
    {
        string Authority { get; set; }
    }

    public interface IHasAuthorityAppSettings<TEndpointsOptions> : IHasAuthorityAppSettings, IHasEndpointsAppSettings<TEndpointsOptions>
    where TEndpointsOptions : AuthorityEndpointsOptions
    {
    }
}
