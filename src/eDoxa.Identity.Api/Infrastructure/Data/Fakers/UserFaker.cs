// Filename: UserFaker.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Identity.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Identity.Domain.AggregateModels;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Security.Constants;

namespace eDoxa.Identity.Api.Infrastructure.Data.Fakers
{
    public sealed class UserFaker : Faker<User>
    {
        private const string TestUser = "TEST_USER";
        private const string AdminUser = "ADMIN_USER";

        public UserFaker()
        {
            this.RuleSet(
                TestUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        faker =>
                        {
                            var firstName = faker.Name.FirstName();

                            var lastName = faker.Name.LastName();

                            var gamertag = new Gamertag(faker.Internet.UserName());

                            var email = new Email(faker.Internet.Email(firstName, lastName));

                            if (faker.Random.Bool())
                            {
                                email.Confirm();
                            }

                            var personalName = new PersonalName(firstName, lastName);

                            var birthDate = new BirthDate(faker.Date.Past(18));

                            var user = new User(gamertag, email, birthDate, personalName);

                            user.SetEntityId(faker.User().Id());

                            user.HashPassword("Pass@word1");

                            var phone = new Phone(faker.Phone.PhoneNumber("##########"));

                            if (faker.Random.Bool())
                            {
                                phone.Confirm();
                            }

                            user.LinkPhone(phone);

                            return user;
                        }
                    );
                }
            );

            this.RuleSet(
                AdminUser,
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(
                        _ =>
                        {
                            var gamertag = new Gamertag("Administrator");

                            var email = new Email("admin@edoxa.gg");

                            email.Confirm();

                            var birthDate = new BirthDate(1970, 1, 1);

                            var personalName = new PersonalName("eDoxa", "Admin");

                            var user = new User(gamertag, email, birthDate, personalName);

                            user.SetEntityId(UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));

                            user.HashPassword("Pass@word1");

                            var phone = new Phone("0000000000");

                            phone.Confirm();

                            user.LinkPhone(phone);

                            var roleFaker = new RoleFaker();

                            var roles = roleFaker.FakeRoles();

                            roles.ForEach(user.AddRole);

                            user.AddClaim(new Claim(CustomClaimTypes.StripeCustomerId, "cus_F5L8mRzm6YN5ma"));

                            user.AddClaim(new Claim(CustomClaimTypes.StripeConnectAccountId, "acct_1EbASfAPhMnJQouG"));

                            return user;
                        }
                    );
                }
            );
        }

        public IEnumerable<User> FakeTestUsers(int count = 1000)
        {
            FakerHub.User().Reset();

            return this.Generate(count, TestUser).ToList();
        }

        public User FakeTestUser()
        {
            FakerHub.User().Reset();

            this.UseSeed(FakerHub.Random.Int());

            return this.Generate(TestUser);
        }

        public User FakeAdminUser()
        {
            return this.Generate(AdminUser);
        }
    }
}
