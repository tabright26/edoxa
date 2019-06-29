// Filename: UserFaker.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Identity.Api.Application.Fakers
{
    public sealed class UserFaker : Faker<User>
    {
        private const string TestUser = "test";
        private const string AdminUser = "admin";

        public UserFaker(int seed)
        {
            this.UseSeed(seed);

            this.RuleSet(
                TestUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var firstName = faker.Name.FirstName();

                            var lastName = faker.Name.LastName();

                            var user = new User(
                                faker.Internet.UserName(firstName, lastName),
                                faker.Internet.Email(firstName, lastName),
                                new PersonalName(firstName, lastName),
                                new BirthDate(faker.Date.Past(18))
                            );

                            user.HashPassword("Pass@word1");

                            return user;
                        }
                    );

                    ruleSet.RuleFor(user => user.Id, faker => faker.UserId());

                    ruleSet.RuleFor(user => user.EmailConfirmed, faker => faker.Random.Bool());

                    ruleSet.RuleFor(user => user.PhoneNumber, faker => faker.Phone.PhoneNumber("##########"));

                    ruleSet.RuleFor(user => user.PhoneNumberConfirmed, faker => faker.Random.Bool());
                }
            );

            this.RuleSet(
                AdminUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        _ =>
                        {
                            var user = new User("Administrator", "admin@edoxa.gg", new PersonalName("eDoxa", "Admin"), new BirthDate(1970, 1, 1));

                            user.HashPassword("Pass@word1");

                            return user;
                        }
                    );

                    ruleSet.RuleFor(user => user.Id, Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));

                    ruleSet.RuleFor(user => user.EmailConfirmed, true);

                    ruleSet.RuleFor(user => user.PhoneNumber, "0000000000");

                    ruleSet.RuleFor(user => user.PhoneNumberConfirmed, true);

                    ruleSet.RuleFor(
                        user => user.Roles,
                        (faker, user) =>
                        {
                            var roleFaker = new RoleFaker();

                            var roles = roleFaker.FakeRoles();

                            return roles.Select(role => new UserRole(user.Id, role.Id)).ToList();
                        }
                    );
                }
            );
        }

        public IEnumerable<User> FakeTestUsers(int count)
        {
            return this.Generate(count, TestUser).ToList();
        }

        public IEnumerable<User> FakeTestUsers()
        {
            return DataResources.TestUserIds.Select(this.FakeTestUser).ToList();
        }

        private User FakeTestUser(Guid userId)
        {
            var user = this.Generate(TestUser);

            user.Id = userId;

            return user;
        }

        public User FakeTestUser()
        {
            return this.Generate(TestUser);
        }

        public User FakeAdminUser()
        {
            return this.Generate(AdminUser);
        }
    }
}
