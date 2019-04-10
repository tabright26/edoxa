using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.NotificationAggregate
{
    [TestClass]
    public sealed class NotificationNamesTest
    {
        [DataRow("user.email.updated")]
        [DataRow("challenge.published")]
        [DataRow("challenge.closed")]
        [DataRow("challenge.participant.registered")]
        [DataRow("challenge.participant.paid")]
        [DataTestMethod]
        public void GetValues_ShouldBeTrue(string name)
        {
            // Act
            var values = NotificationNames.GetValues();

            // Should
            values.Should().Contain(name);
        }

        [DataRow("user.email.updated")]
        [DataRow("challenge.published")]
        [DataRow("challenge.closed")]
        [DataRow("challenge.participant.registered")]
        [DataRow("challenge.participant.paid")]
        [DataTestMethod]
        public void IsValid_ShouldBeTrue(string name)
        {
            // Act
            var isValid = NotificationNames.IsValid(name);

            // Should
            isValid.Should().BeTrue();
        }

        [DataRow(null)]
        [DataRow("  ")]
        [DataRow("user")]
        [DataRow("challenge")]
        [DataRow("structure")]
        [DataTestMethod]
        public void IsValid_ShouldBeFalse(string name)
        {
            // Act
            var isValid = NotificationNames.IsValid(name);

            // Should
            isValid.Should().BeFalse();
        }
    }
}
