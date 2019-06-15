// Filename: DiagnosticsController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.IdentityServer.Infrastructure.Attributes;
using eDoxa.IdentityServer.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.IdentityServer.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var localAddresses = new[] {"127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString()};

            //if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            //{
            //    return this.NotFound();
            //}

            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());

            return this.View(model);
        }
    }
}
