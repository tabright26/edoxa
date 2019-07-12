// Filename: DevToolsControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Seedwork.Application.Mvc.Filters.Attributes;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Controllers
{
    [TestClass]
    public sealed class DevToolsControllerTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DevToolsController>.ForParameters(typeof(IDbContextData))
                .WithClassName("DevToolsController")
                .WithClassAttributes(
                    typeof(AuthorizeAttribute),
                    typeof(DevToolsAttribute),
                    typeof(ApiControllerAttribute),
                    typeof(ApiVersionAttribute),
                    typeof(ProducesAttribute),
                    typeof(RouteAttribute),
                    typeof(ApiExplorerSettingsAttribute)
                )
                .Assert();
        }
    }
}
