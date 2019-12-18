// Filename: Register.cshtml.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IRedirectService _redirectService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInService _signInService;
        private readonly IUserService _userService;

        public RegisterModel(
            IUserService userService,
            SignInService signInService,
            ILogger<RegisterModel> logger,
            IServiceBusPublisher serviceBusPublisher,
            IRedirectService redirectService
        )
        {
            _userService = userService;
            _signInService = signInService;
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _redirectService = redirectService;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= _redirectService.RedirectToWebSpa();

            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = Input.Email,
                        UserName = Input.Email,
                        Country = Input.Country
                    },
                    Input.Password);

                if (result.Succeeded)
                {
                    var user = await _userService.FindByEmailAsync(Input.Email);

                    _logger.LogInformation("User created a new account with password.");

                    await _serviceBusPublisher.PublishUserCreatedIntegrationEventAsync(UserId.FromGuid(user.Id), Input.Email, Input.Country);

                    var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

                    //var callbackUrl = $"{_redirectService.RedirectToWebSpa("/email/confirm")}?userId={user.Id}&code={code}";

                    //await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                    //    UserId.FromGuid(user.Id),
                    //    Input.Email,
                    //    "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _serviceBusPublisher.PublishAsync(
                        new UserEmailConfirmationTokenGeneratedIntegrationEvent
                        {
                            UserId = user.Id.ToString(),
                            Code = code
                        });

                    await _signInService.SignInAsync(user, false);

                    return this.Redirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public Country Country { get; set; } = Country.Canada; // FRANCIS: Should be in an input of type select. (TEMP)
        }
    }
}
