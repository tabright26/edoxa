// Filename: UserFaker.cs
// Date Created: 2019-06-08
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

namespace eDoxa.Identity.Api.Application.Data.Fakers
{
    public sealed class UserFaker : Faker<User>
    {
        private const string NewUser = "new";
        private const string AdminUser = "admin";

        public UserFaker()
        {
            this.UseSeed(8675309);

            this.RuleSet(
                NewUser,
                ruleSet =>
                {
                    ruleSet.StrictMode(false);

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

                    ruleSet.RuleFor(user => user.Id, faker => faker.Random.Guid());

                    ruleSet.RuleFor(user => user.EmailConfirmed, faker => faker.Random.Bool());

                    ruleSet.RuleFor(user => user.PhoneNumber, faker => faker.Phone.PhoneNumber("##########"));

                    ruleSet.RuleFor(user => user.PhoneNumberConfirmed, faker => faker.Random.Bool());
                }
            );

            this.RuleSet(
                AdminUser,
                ruleSet =>
                {
                    ruleSet.StrictMode(false);

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

        public List<User> FakeNewUsers(int count)
        {
            return this.Generate(count, NewUser);
        }

        public User FakeNewUser()
        {
            return this.Generate(NewUser);
        }

        public User FakeAdminUser()
        {
            return this.Generate(AdminUser);
        }
    }
}
