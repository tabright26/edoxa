// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure.Models;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.IdentityServer.Infrastructure.Factories
{
    public sealed class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserModel, RoleModel>
    {
        public CustomUserClaimsPrincipalFactory(
            IMapper mapper,
            UserManager<UserModel> userManager,
            RoleManager<RoleModel> roleManager,
            IOptions<IdentityOptions> options
        ) : base(userManager, roleManager, options)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        [ItemNotNull]
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync([NotNull] UserModel userModel)
        {
            var identity = await base.GenerateClaimsAsync(userModel);

            var user = Mapper.Map<User>(userModel);

            identity.AddClaim(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.ToString()));

            identity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.PersonalName.FirstName));

            identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.PersonalName.LastName));

            identity.AddClaim(new Claim(JwtClaimTypes.Name, user.PersonalName.FullName));

            return identity;
        }
    }
}
