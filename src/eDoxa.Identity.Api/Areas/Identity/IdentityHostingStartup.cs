// Filename: IdentityHostingStartup.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity;
using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Models;
using eDoxa.Seedwork.Security.Constants;

using FluentValidation.AspNetCore;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace eDoxa.Identity.Api.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure([NotNull] IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (context, services) =>
                {
                    
                }
            );
        }
    }
}
