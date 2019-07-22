// Filename: Login.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        //private readonly CustomSignInManager _signInManager;
        //private readonly CustomUserManager _userManager;
        //private readonly ILogger<LoginModel> _logger;

        //public LoginModel(CustomSignInManager signInManager, CustomUserManager userManager, ILogger<LoginModel> logger)
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //    _logger = logger;
        //}

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public string ReturnUrl { get; set; }

        //[TempData]
        //public string ErrorMessage { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            return this.RedirectToAction(
                "Login",
                "Account",
                new
                {
                    area = "",
                    returnUrl
                }
            );

            //if (!string.IsNullOrEmpty(ErrorMessage))
            //{
            //    ModelState.AddModelError(string.Empty, ErrorMessage);
            //}

            //returnUrl = returnUrl ?? Url.Content("~/");

            //// Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //ReturnUrl = returnUrl;
        }

        //public IActionResult OnPost(string returnUrl = null)
        //{
        //    return this.RedirectToAction(
        //        "Login",
        //        "Account",
        //        new
        //        {
        //            area = "",
        //            returnUrl
        //        }
        //    );

        //    //returnUrl = returnUrl ?? Url.Content("~/");

        //    //if (ModelState.IsValid)
        //    //{
        //    //    var usr = await _userManager.Users.SingleOrDefaultAsync(user => user.Email == Input.Email);

        //    //    // This doesn't count login failures towards account lockout
        //    //    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //    //    var result = await _signInManager.PasswordSignInAsync(usr.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        //    //    if (result.Succeeded)
        //    //    {
        //    //        _logger.LogInformation("User logged in.");
        //    //        return this.LocalRedirect(returnUrl);
        //    //    }
        //    //    if (result.RequiresTwoFactor)
        //    //    {
        //    //        return this.RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        //    //    }
        //    //    if (result.IsLockedOut)
        //    //    {
        //    //        _logger.LogWarning("User account locked out.");
        //    //        return this.RedirectToPage("./Lockout");
        //    //    }
        //    //    else
        //    //    {
        //    //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //    //        return this.Page();
        //    //    }
        //    //}

        //    //// If we got this far, something failed, redisplay form
        //    //return this.Page();
        //}

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }

        //    [Required]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [Display(Name = "Remember me?")]
        //    public bool RememberMe { get; set; }
        //}
    }
}
