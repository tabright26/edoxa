// Filename: MockUserLoginInfoServiceExtensions.cs
// Date Created: 2019-05-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Abstractions;
using eDoxa.Seedwork.Domain.Enumerations;

using Moq;

namespace eDoxa.Testing.MSTest.Extensions
{
    public static class MockUserLoginInfoServiceExtensions
    {
        public static void SetupGetProperties(this Mock<IUserLoginInfoService> mockUserProfile)
        {
            mockUserProfile.Setup(mock => mock.GetExternalKey(Game.LeagueOfLegends)).Returns("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");
        }
    }
}