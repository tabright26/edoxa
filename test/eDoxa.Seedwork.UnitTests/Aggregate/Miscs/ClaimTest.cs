// Filename: ClaimTest.cs
// Date Created: 2019-10-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Security;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Aggregate.Miscs
{
    public sealed class ClaimTest
    {
        [Fact]
        public void DeserializeObject_WhenDeserializeWithJsonConstructor_ShouldBeEquivalentToClaims()
        {
            // Arrange
            var claims = new Claims(new Claim("TEST", "TEST1"), new Claim("TEST", "TEST2"), new Claim("TEST", "TEST3"));

            var serializeObject = JsonConvert.SerializeObject(claims);

            // Act
            var deserializeObject = JsonConvert.DeserializeObject<Claims>(serializeObject);

            // Assert
            deserializeObject.Should().BeEquivalentTo(claims);
        }
    }
}
