// Filename: ContainerBuilderExtensions.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using Autofac;

using Moq;

namespace eDoxa.Seedwork.Application.Autofac.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterMock<T>(this ContainerBuilder builder)
        where T : class
        {
            builder.RegisterInstance(new Mock<T>().Object).As<T>().SingleInstance();
        }
    }
}
