// Filename: MockUserProfileExtensions.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Abstractions;

using Moq;

namespace eDoxa.Testing.MSTest.Extensions
{
    public static class MockUserInfoServiceExtensions
    {
        public static void SetupGetProperties(this Mock<IUserInfoService> mockUserProfile)
        {
            mockUserProfile.SetupGet(mock => mock.Subject).Returns("e4655fe0-affd-4323-b022-bdb2ebde6091");

            mockUserProfile.SetupGet(mock => mock.PreferredUserName).Returns("Administrator");

            mockUserProfile.SetupGet(mock => mock.GivenName).Returns("Francis");

            mockUserProfile.SetupGet(mock => mock.FamilyName).Returns("Quenneville");

            mockUserProfile.SetupGet(mock => mock.Name).Returns("Francis Quenneville");

            mockUserProfile.SetupGet(mock => mock.BirthDate).Returns("1995-05-06");

            mockUserProfile.SetupGet(mock => mock.Email).Returns("admin@edoxa.gg");

            mockUserProfile.SetupGet(mock => mock.EmailVerified).Returns("true");

            mockUserProfile.SetupGet(mock => mock.PhoneNumber).Returns("5147580313");

            mockUserProfile.SetupGet(mock => mock.PhoneNumberVerified).Returns("true");

            mockUserProfile.SetupGet(mock => mock.Address).Returns((string) null);

            mockUserProfile.SetupGet(mock => mock.CustomerId).Returns("cus_we234rTi24o");

            mockUserProfile.SetupGet(mock => mock.Roles).Returns(new[]
            {
                "Role1",
                "Role2",
                "Role3",
                "Role4",
                "Role5"
            });

            mockUserProfile.SetupGet(mock => mock.Permissions).Returns(new[]
            {
                "permission1",
                "permission2",
                "permission3",
                "permission4",
                "permission5"
            });
        }
    }
}