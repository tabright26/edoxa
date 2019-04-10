// Filename: HomeController.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Identity.Areas.Identity.ViewModels;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Areas.Identity.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _environment;

        public HomeController(IIdentityServerInteractionService interaction, IHostingEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var model = new ErrorViewModel();

            // Retrieve error details from IdentityServer.
            var message = await _interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                model.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // Only show in development.
                    message.ErrorDescription = null;
                }
            }

            return this.View("Error", model);
        }
    }
}