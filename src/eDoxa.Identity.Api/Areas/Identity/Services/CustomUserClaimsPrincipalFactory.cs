// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Models;
using eDoxa.Seedwork.Security.Constants;

using IdentityModel;

using IdentityServer4;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class CustomUserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<User>
    {
        public CustomUserClaimsPrincipalFactory(CustomUserManager userManager, CustomRoleManager roleManager, IOptions<IdentityOptions> optionsAccessor)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            RoleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            Options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        private CustomUserManager UserManager { get; }

        private CustomRoleManager RoleManager { get; }

        private IdentityOptions Options { get; }

        private ClaimsIdentity Identity { get; set; }

        [ItemNotNull]
        public async Task<ClaimsPrincipal> CreateAsync([NotNull] User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Identity = new ClaimsIdentity("Identity.Application", Options.ClaimsIdentity.UserNameClaimType, Options.ClaimsIdentity.RoleClaimType);

            await this.GenerateUserClaimsAsync(user);

            await this.GenerateRoleClaimsAsync(user);

            await this.GenerateGameClaimsAsync(user);

            return new ClaimsPrincipal(Identity);
        }

        private async Task GenerateUserClaimsAsync(User user)
        {
            Identity.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, await UserManager.GetUserIdAsync(user)));

            await this.TryGenerateUserNameClaimAsync(user);

            await this.TryGenerateFirstNameClaimAsync(user);

            await this.TryGenerateLastNameClaimAsync(user);

            await this.TryGenerateBirthDateClaimAsync(user);

            await this.TryGenerateEmailClaimsAsync(user);

            await this.TryGeneratePhoneNumberClaimsAsync(user);

            if (UserManager.SupportsUserSecurityStamp)
            {
                Identity.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType, await UserManager.GetSecurityStampAsync(user)));
            }

            if (UserManager.SupportsUserClaim)
            {
                Identity.AddClaims(await UserManager.GetClaimsAsync(user));
            }
        }

        private async Task TryGenerateUserNameClaimAsync(User user)
        {
            var userName = await UserManager.GetUserNameAsync(user);

            if (userName != null)
            {
                Identity.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            }
        }

        private async Task TryGenerateFirstNameClaimAsync(User user)
        {
            var firstName = await UserManager.GetFirstNameAsync(user);

            if (firstName != null)
            {
                Identity.AddClaim(new Claim(JwtClaimTypes.GivenName, firstName));
            }
        }

        private async Task TryGenerateLastNameClaimAsync(User user)
        {
            var lastName = await UserManager.GetLastNameAsync(user);

            if (lastName != null)
            {
                Identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, lastName));
            }
        }

        private async Task TryGenerateBirthDateClaimAsync(User user)
        {
            var birthDate = await UserManager.GetBirthDateAsync(user);

            if (birthDate != null)
            {
                Identity.AddClaim(new Claim(JwtClaimTypes.BirthDate, birthDate));
            }
        }

        private async Task TryGenerateEmailClaimsAsync(User user)
        {
            if (UserManager.SupportsUserEmail)
            {
                var email = await UserManager.GetEmailAsync(user);

                var emailConfirmed = await UserManager.IsEmailConfirmedAsync(user);

                Identity.AddClaim(new Claim(JwtClaimTypes.Email, email));

                Identity.AddClaim(new Claim(JwtClaimTypes.EmailVerified, emailConfirmed.ToString()));
            }
        }

        private async Task TryGeneratePhoneNumberClaimsAsync(User user)
        {
            if (UserManager.SupportsUserPhoneNumber)
            {
                var phoneNumber = await UserManager.GetPhoneNumberAsync(user);

                if (phoneNumber != null)
                {
                    var phoneNumberConfirmed = await UserManager.IsPhoneNumberConfirmedAsync(user);

                    Identity.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, phoneNumber));

                    Identity.AddClaim(new Claim(JwtClaimTypes.PhoneNumberVerified, phoneNumberConfirmed.ToString()));
                }
            }
        }

        private async Task GenerateRoleClaimsAsync(User user)
        {
            if (UserManager.SupportsUserRole)
            {
                var roles = await UserManager.GetRolesAsync(user);

                foreach (var roleName in roles)
                {
                    Identity.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));

                    if (RoleManager.SupportsRoleClaims)
                    {
                        await this.GenerateRoleClaimsAsync(roleName);
                    }
                }
            }
        }

        private async Task GenerateRoleClaimsAsync(string roleName)
        {
            var role = await RoleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                Identity.AddClaims(await RoleManager.GetClaimsAsync(role));
            }
        }

        private async Task GenerateGameClaimsAsync(User user)
        {
            var games = await UserManager.GetGamesAsync(user);

            var gameInfos = games.ToDictionary(gameInfo => gameInfo.Name, providerInfo => providerInfo.PlayerId);

            if (gameInfos.Any())
            {
                Identity.AddClaim(new Claim(CustomClaimTypes.Games, JsonConvert.SerializeObject(gameInfos, Formatting.Indented), IdentityServerConstants.ClaimValueTypes.Json));
            }
        }
    }
}
