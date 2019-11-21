// Filename: HomeController.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Attributes;
using eDoxa.Identity.Api.ViewModels;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Controllers
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger _logger;

        public HomeController(IIdentityServerInteractionService interaction, IWebHostEnvironment environment, ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (_environment.IsDevelopment())
            {
                // only show in development
                return this.View();
            }

            _logger.LogInformation("Homepage is disabled in production. Returning 404.");

            return this.NotFound();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        ///     Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return this.View("Error", vm);
        }
    }
}
