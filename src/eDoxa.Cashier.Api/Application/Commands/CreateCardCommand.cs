// Filename: CreateCardCommand.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Api.Application.Commands
{
    [DataContract]
    public sealed class CreateCardCommand : Command
    {
        public CreateCardCommand(string sourceToken)
        {
            SourceToken = sourceToken;
        }

        [DataMember(Name = "sourceToken")]
        public string SourceToken { get; private set; }
    }
}
