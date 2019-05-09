// Filename: SynchronizeCommand.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Challenges.Application.Commands
{
    public sealed class SynchronizeCommand : Command
    {
        public SynchronizeCommand(Game game)
        {
            Game = game;
        }

        public Game Game { get; }
    }
}