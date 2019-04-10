// Filename: DiagnosticsController.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Identity.Areas.Identity.ViewModels.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Areas.Identity.Controllers
{
    [Authorize]
    [Area(nameof(Identity))]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var localAddresses = new[]
            //{
            //    "127.0.0.1", "::1", this.HttpContext.Connection.LocalIpAddress.ToString()
            //};

            //if (!localAddresses.Contains(this.HttpContext.Connection.RemoteIpAddress.ToString()))
            //{
            //    return this.NotFound();
            //}

            return this.View(new DiagnosticsViewModel(await HttpContext.AuthenticateAsync()));
        }
    }
}