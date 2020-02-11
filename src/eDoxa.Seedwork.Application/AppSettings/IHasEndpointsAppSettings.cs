// Filename: IHasEndpointsAppSettings.cs
// Date Created: 2019-11-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.AppSettings.Options;

namespace eDoxa.Seedwork.Application.AppSettings
{
    public interface IHasEndpointsAppSettings<TEndpointsOptions>
    where TEndpointsOptions : AuthorityEndpointsOptions
    {
        TEndpointsOptions Endpoints { get; set; }
    }
}
