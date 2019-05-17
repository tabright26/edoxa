using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Testing.MSTest;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class AccountsControllerTest
    {
        private Mock<IAccountQueries> _mockAccountQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountQueries = new Mock<IAccountQueries>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountsController>.For(typeof(IAccountQueries))
                .WithName("AccountsController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute), typeof(RouteAttribute), typeof(ApiExplorerSettingsAttribute))
                .Assert();
        }
    }
}
