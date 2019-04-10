// Filename: Command.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Application.Commands
{
    public abstract class Command : Command<Unit>, ICommand
    {
    }

    public abstract class Command<TResponse> : BaseCommand, ICommand<TResponse>
    {
    }
}