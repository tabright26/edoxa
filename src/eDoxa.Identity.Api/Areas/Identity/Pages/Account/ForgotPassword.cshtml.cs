// Filename: ForgotPassword.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ForgotPasswordModel(IUserService userService, IServiceBusPublisher serviceBusPublisher)
        {
            _userService = userService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByEmailAsync(Input.Email);

                if (user == null || !await _userService.IsEmailConfirmedAsync(user))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userService.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    null,
                    new
                    {
                        code
                    },
                    Request.Scheme
                );

                await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                );

                return this.RedirectToPage("./ForgotPasswordConfirmation");
            }

            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
