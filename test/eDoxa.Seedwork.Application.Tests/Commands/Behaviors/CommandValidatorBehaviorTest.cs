// Filename: CommandValidatorBehaviorTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Application.Tests.Commands.Behaviors
{
    [TestClass]
    public sealed class CommandValidatorBehaviorTest
    {
        //[TestMethod]
        //public async Task Test1()
        //{
        //    // Arrange
        //    var validator = new Mock<IValidator<MockCommand>>();

        //    validator.Setup(command => command.Validate(It.IsAny<MockCommand>()))
        //             .Returns(
        //                 new ValidationResult(
        //                     new[]
        //                     {
        //                         new ValidationFailure("propertyName", "errorMessage")
        //                     }
        //                 )
        //             );

        //    var builder = MockContainerBuilder();
        //    builder.RegisterInstance(validator.Object).AsImplementedInterfaces();
        //    var container = builder.Build();
        //    var mediator = container.Resolve<IMediator>();

        //    // Act => Assert
        //    await Assert.ThrowsExceptionAsync<ValidationException>(async () => await mediator.Send(new MockCommand()));
        //}

        //private static ContainerBuilder MockContainerBuilder()
        //{
        //    var builder = new ContainerBuilder();

        //    builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

        //    // Register MediatR service factory.
        //    builder.Register<ServiceFactory>(
        //        context =>
        //        {
        //            var componentContext = context.Resolve<IComponentContext>();

        //            return type => componentContext.TryResolve(type, out var instance) ? instance : null;
        //        }
        //    );

        //    builder.RegisterGeneric(typeof(CommandValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        //    return builder;
        //}

        //[DataContract]
        //public sealed class MockCommand : Command
        //{
        //}
    }
}