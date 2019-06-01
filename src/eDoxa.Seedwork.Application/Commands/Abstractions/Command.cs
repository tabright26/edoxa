// Filename: Command.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Abstractions
{
    public abstract class Command : Command<Unit>, ICommand
    {
    }

    public abstract class Command<TResponse> : BaseCommand, ICommand<TResponse>
    {
    }
}