// Filename: MediatorExtensions.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Commands.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Commands.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<IActionResult> SendCommandAsync(this IMediator mediator, ICommand<IActionResult> command)
        {
            return await mediator.Send(command);
        }

        public static async Task<Unit> SendCommandAsync(this IMediator mediator, ICommand<Unit> command)
        {
            return await mediator.Send(command);
        }
    }
}