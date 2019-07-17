// Filename: DiagnosticsController.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Attributes;
using eDoxa.Identity.Api.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Controllers
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
