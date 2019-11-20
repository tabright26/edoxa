// Filename: HomeController.cs
// Date Created: 2019-11-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Web.Spa.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eDoxa.Web.Spa.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Configuration([FromServices] IOptionsSnapshot<WebSpaAppSettings> settings)
        {
            return this.Json(settings.Value);
        }
    }
}
