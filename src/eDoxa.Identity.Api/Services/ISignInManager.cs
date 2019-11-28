// Filename: ISignInManager.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Services
{
    public interface ISignInManager
    {
        Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user);

        bool IsSignedIn(ClaimsPrincipal principal);

        Task<bool> CanSignInAsync(User user);

        Task RefreshSignInAsync(User user);

        Task SignInAsync(User user, bool isPersistent, string authenticationMethod);

        Task SignInAsync(User user, AuthenticationProperties authenticationProperties, string authenticationMethod);

        Task SignOutAsync();

        Task<User> ValidateSecurityStampAsync(ClaimsPrincipal principal);

        Task<User> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal);

        Task<bool> ValidateSecurityStampAsync(User user, string securityStamp);

        Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent,
            bool lockoutOnFailure
        );

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent,
            bool lockoutOnFailure
        );

        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);

        Task<bool> IsTwoFactorClientRememberedAsync(User user);

        Task RememberTwoFactorClientAsync(User user);

        Task ForgetTwoFactorClientAsync();

        Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

        Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);

        Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent,
            bool rememberClient
        );

        Task<User> GetTwoFactorAuthenticationUserAsync();

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);

        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent,
            bool bypassTwoFactor
        );

        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf);

        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId);

        ILogger Logger { get; set; }

        UserManager<User> UserManager { get; set; }

        IUserClaimsPrincipalFactory<User> ClaimsFactory { get; set; }

        IdentityOptions Options { get; set; }

        HttpContext Context { get; set; }
    }
}
