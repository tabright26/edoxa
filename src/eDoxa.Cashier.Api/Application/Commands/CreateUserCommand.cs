// Filename: CreateUserCommand.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Api.Application.Commands
{
    public sealed class CreateUserCommand : Command
    {
        public CreateUserCommand(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; private set; }
    }
}
