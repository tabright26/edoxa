// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using IdentityModel;

using IdentityServer4;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using ClaimTypes = eDoxa.Seedwork.Security.ClaimTypes;

namespace eDoxa.Identity.Api.Services
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
            IUserService = userService;
            RoleService = roleService;
            Options = optionsAccessor.Value;
        }

        private IUserService IUserService { get; }

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
            Identity!.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, await IUserService.GetUserIdAsync(user)));

            var country = await IUserService.GetCountryAsync(user);

            Identity!.AddClaim(new Claim(ClaimTypes.Country, country.TwoDigitIso));

            await this.TryGenerateDoxatagClaimAsync(user);

            await this.TryGenerateFirstNameClaimAsync(user);

            await this.TryGenerateLastNameClaimAsync(user);

            await this.TryGenerateNameClaimAsync(user);

            await this.TryGenerateBirthDateClaimAsync(user);

            await this.TryGenerateEmailClaimsAsync(user);

            await this.TryGeneratePhoneNumberClaimsAsync(user);

            if (IUserService.SupportsUserSecurityStamp)
            {
                Identity.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType, await IUserService.GetSecurityStampAsync(user)));
            }

            if (IUserService.SupportsUserClaim)
            {
                Identity.AddClaims(await IUserService.GetClaimsAsync(user));
            }
        }

        private async Task TryGenerateDoxatagClaimAsync(User user)
        {
            var doxatag = await _doxatagService.FindDoxatagAsync(user);

            if (doxatag != null)
            {
                Identity!.AddClaim(
                    new Claim(
                        ClaimTypes.Doxatag,
                        JsonConvert.SerializeObject(
                            doxatag,
                            Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }),
                        IdentityServerConstants.ClaimValueTypes.Json));
            }
        }

        private async Task TryGenerateNameClaimAsync(User user)
        {
            var firstName = await IUserService.GetFirstNameAsync(user);

            var lastName = await IUserService.GetLastNameAsync(user);

            if (firstName != null && lastName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.Name, $"{firstName} {lastName}"));
            }
        }

        private async Task TryGenerateFirstNameClaimAsync(User user)
        {
            var firstName = await IUserService.GetFirstNameAsync(user);

            if (firstName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.GivenName, firstName));
            }
        }

        private async Task TryGenerateLastNameClaimAsync(User user)
        {
            var lastName = await IUserService.GetLastNameAsync(user);

            if (lastName != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.FamilyName, lastName));
            }
        }

        private async Task TryGenerateBirthDateClaimAsync(User user)
        {
            var dob = await IUserService.GetDobAsync(user);

            if (dob != null)
            {
                Identity!.AddClaim(new Claim(JwtClaimTypes.BirthDate, dob.ToString()));
            }
        }

        private async Task TryGenerateEmailClaimsAsync(User user)
        {
            if (IUserService.SupportsUserEmail)
            {
                var email = await IUserService.GetEmailAsync(user);

                var emailConfirmed = await IUserService.IsEmailConfirmedAsync(user);

                Identity!.AddClaim(new Claim(JwtClaimTypes.Email, email));

                Identity.AddClaim(new Claim(JwtClaimTypes.EmailVerified, emailConfirmed.ToString()));
            }
        }

        private async Task TryGeneratePhoneNumberClaimsAsync(User user)
        {
            if (IUserService.SupportsUserPhoneNumber)
            {
                var phoneNumber = await IUserService.GetPhoneNumberAsync(user);

                if (phoneNumber != null)
                {
                    var phoneNumberConfirmed = await IUserService.IsPhoneNumberConfirmedAsync(user);

                    Identity!.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, phoneNumber));

                    Identity!.AddClaim(new Claim(JwtClaimTypes.PhoneNumberVerified, phoneNumberConfirmed.ToString()));
                }
            }
        }

        private async Task GenerateRoleClaimsAsync(User user)
        {
            if (IUserService.SupportsUserRole)
            {
                var roles = await IUserService.GetRolesAsync(user);

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
