// Filename: EitherTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Functional.Tests
{
    [TestClass]
    public sealed class EitherTest
    {
        [TestMethod]
        public void Match_ShouldBeOfTypeBadRequestObjectResult()
        {
            var either = new Either<ValidationResult, string>(
                new ValidationResult(
                    new List<ValidationFailure>
                    {
                        new ValidationFailure("right", "invalid result")
                    }
                )
            );

            var result = either.Match<IActionResult>(validationResult => new BadRequestObjectResult(validationResult), value => new OkObjectResult(value));

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestMethod]
        public void Match_ShouldBeOfTypeOkObjectResult()
        {
            var either = new Either<ValidationResult, string>("Valid right result.");

            var result = either.Match<IActionResult>(validationResult => new BadRequestObjectResult(validationResult), value => new OkObjectResult(value));

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
