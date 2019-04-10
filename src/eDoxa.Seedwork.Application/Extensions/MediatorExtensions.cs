// Filename: MediatorExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Commands;

using MediatR;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<TResponse> SendCommandAsync<TResponse>(this IMediator mediator, ICommand<TResponse> command)
        {
            return await mediator.Send(command);
        }
    }
}