// Filename: MediatorExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Commands.Abstractions;

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<T> SendCommandAsync<T>(this IMediator mediator, ICommand<T> command)
        {
            return await mediator.Send(command);
        }

        public static async Task SendCommandAsync(this IMediator mediator, ICommand command)
        {
            await mediator.Send(command);
        }
    }
}
