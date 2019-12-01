// Filename: CandidaturePostRequestTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using eDoxa.Clans.Requests;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Requests
{
    public sealed class ClanLogoPostRequestTest : UnitTest
    {
        public ClanLogoPostRequestTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void DeserializeObject_WhenDeserializeWithDataContractConstructor_ShouldBeEquivalentToRequest()
        {
            //Arrange
            var imageStream = new MemoryStream();

            var request = new ClanLogoPostRequest(new FormFile(imageStream, 0, 0, "test", "testFile"));

            request.Logo.Should().BeOfType<FormFile>();
        }
    }
}
