// Filename: Success.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Commands.Result
{
    public class CommandResult : CommandResult<string>
    {
        public static readonly CommandResult Succeeded = new CommandResult("Command succeeded.");

        public CommandResult(string message) : base(message)
        {
        }
    }

    public class CommandResult<T>
    {
        public CommandResult(T data)
        {
            Data = data;
        }

        public T Data { get; }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}