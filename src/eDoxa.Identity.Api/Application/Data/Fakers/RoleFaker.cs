// Filename: RoleFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Security.Constants;

namespace eDoxa.Identity.Api.Application.Data.Fakers
{
    public sealed class RoleFaker : CustomFaker<Role>
    {
        private const string AdminRole = "admin";
        private const string ChallengerRole = "challenger";

        public RoleFaker()
        {
            this.RuleSet(
                AdminRole,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(_ => new Role(CustomRoles.Administrator));

                    ruleSet.RuleFor(role => role.Id, Guid.Parse("6349E9F9-4799-4100-9A1D-34F79CF480D4"));

                    ruleSet.RuleFor(
                        role => role.Claims,
                        (faker, role) => new List<RoleClaim>
                        {
                            new RoleClaim(role.Id, CustomClaimTypes.Permission, "*")
                        }
                    );
                }
            );

            this.RuleSet(
                ChallengerRole,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(_ => new Role(CustomRoles.Challenger));

                    ruleSet.RuleFor(role => role.Id, Guid.Parse("0FC7B6DF-69CE-443D-B127-D4A8CC4B7361"));

                    ruleSet.RuleFor(
                        role => role.Claims,
                        (faker, role) => new List<RoleClaim>
                        {
                            new RoleClaim(role.Id, CustomClaimTypes.Permission, "challenge.read"),
                            new RoleClaim(role.Id, CustomClaimTypes.Permission, "challenge.register")
                        }
                    );
                }
            );
        }

        public Role FakeAdminRole()
        {
            return this.Generate(AdminRole);
        }

        public Role FakeChallengerRole()
        {
            return this.Generate(ChallengerRole);
        }

        public List<Role> FakeRoles()
        {
            return new List<Role>
            {
                this.FakeAdminRole(),
                this.FakeChallengerRole()
            };
        }
    }
}
