// Filename: RoleFaker.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Bogus;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Seedwork.Security.Constants;

namespace eDoxa.Identity.Api.Infrastructure.Data.Fakers
{
    public sealed class RoleFaker : Faker<Role>
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
                        role => role.Permissions,
                        (faker, role) => new List<Permission>
                        {
                            new Permission("*")
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
                        role => role.Permissions,
                        (faker, role) => new List<Permission>
                        {
                            new Permission("challenge.read"),
                            new Permission("challenge.register")
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
