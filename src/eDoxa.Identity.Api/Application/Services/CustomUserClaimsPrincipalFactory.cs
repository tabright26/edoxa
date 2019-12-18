// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Security;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class CustomUserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<User>
    {
        private readonly IDoxatagService _doxatagService;

        public CustomUserClaimsPrincipalFactory(
            IUserService userService,
            IRoleService roleService,
            IDoxatagService doxatagService,
            IOptions<IdentityOptions> optionsAccessor
        )
        {
            _doxatagService = doxatagService;
            UserService = userService;
            RoleService = roleService;
            Options = optionsAccessor.Value;
        }

        private IUserService UserService { get; }

        private IRoleService RoleService { get; }

        private IdentityOptions Options { get; }

        private ClaimsIdentity? Identity { get; set; }

        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Identity = new ClaimsIdentity("Identity.Application", Options.ClaimsIdentity.UserNameClaimType, Options.ClaimsIdentity.RoleClaimType);

            await this.GenerateUserClaimsAsync(user);

            await this.GenerateRoleClaimsAsync(user);

            return new ClaimsPrincipal(Identity);
        }

        private async Task GenerateUserClaimsAsync(User user)
        {
            Identity!.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, await UserService.GetUserIdAsync(user)));

            var country = await UserService.GetCountryAsync(user);

            Identity!.AddClaim(new Claim(CustomClaimTypes.Country, country.TwoDigitIso));

            await this.TryGenerateDoxatagClaimAsync(user);

            await this.TryGenerateFirstNameClaimAsync(user);

            await this.TryGenerateLastNameClaimAsync(user);

            await this.TryGenerateNameClaimAsync(user);

            await this.TryGenerateBirthDateClaimAsync(user);

            await this.TryGenerateEmailClaimsAsync(user);

            await this.TryGeneratePhoneNumberClaimsAsync(user);

            if (UserService.SupportsUserSecurityStamp)
            {
                Identity.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType, await UserService.GetSecurityStampAsync(user)));
            }

            if (UserService.SupportsUserClaim)
            {
                Identity.AddClaims(await UserService.GetClaimsAsync(user));
            }
        }

        private async Task TryGenerateDoxatagClaimAsync(User user)
        {
            var doxatag = await _doxatagService.FindDoxatagAsync(user);

            if (doxatag != null)
            {
                Identity!.AddClaim(new Claim(CustomClaimTypes.Doxatag, doxatag.ToString()));
            }
        }

        private async Task TryGenerateNameClaimAsync(User user)
        {
            var firstName = await UserService.GetFirstNameAsync(user);

            var lastName = await UserService.GetLastNameAsync(user);

            if (firstName != null && lastName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.Name, $"{firstName} {lastName}"));
            }
        }

        private async Task TryGenerateFirstNameClaimAsync(User user)
        {
            var firstName = await UserService.GetFirstNameAsync(user);

            if (firstName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.GivenName, firstName));
            }
        }

        private async Task TryGenerateLastNameClaimAsync(User user)
        {
            var lastName = await UserService.GetLastNameAsync(user);

            if (lastName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.FamilyName, lastName));
            }
        }

        private async Task TryGenerateBirthDateClaimAsync(User user)
        {
            var dob = await UserService.GetDobAsync(user);

            if (dob != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.BirthDate, dob.ToString()));
            }
        }

        private async Task TryGenerateEmailClaimsAsync(User user)
        {
            if (UserService.SupportsUserEmail)
            {
                var email = await UserService.GetEmailAsync(user);

                var emailConfirmed = await UserService.IsEmailConfirmedAsync(user);

                Identity!.AddClaim(new Claim(JwtClaimTypes.Email, email));

                Identity.AddClaim(new Claim(JwtClaimTypes.EmailVerified, emailConfirmed.ToString()));
            }
        }

        private async Task TryGeneratePhoneNumberClaimsAsync(User user)
        {
            if (UserService.SupportsUserPhoneNumber)
            {
                var phoneNumber = await UserService.GetPhoneNumberAsync(user);

                if (phoneNumber != null)
                {
                    var phoneNumberConfirmed = await UserService.IsPhoneNumberConfirmedAsync(user);

                    Identity!.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, phoneNumber));

                    Identity!.AddClaim(new Claim(JwtClaimTypes.PhoneNumberVerified, phoneNumberConfirmed.ToString()));
                }
            }
        }

        private async Task GenerateRoleClaimsAsync(User user)
        {
            if (UserService.SupportsUserRole)
            {
                var roles = await UserService.GetRolesAsync(user);

                foreach (var roleName in roles)
                {
                    Identity!.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));

                    if (RoleService.SupportsRoleClaims)
                    {
                        await this.GenerateRoleClaimsAsync(roleName);
                    }
                }
            }
        }

        private async Task GenerateRoleClaimsAsync(string roleName)
        {
            var role = await RoleService.FindByNameAsync(roleName);

            if (role != null)
            {
                Identity!.AddClaims(await RoleService.GetClaimsAsync(role));
            }
        }
    }
}
