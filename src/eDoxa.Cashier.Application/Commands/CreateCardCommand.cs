// Filename: CreateCardCommand.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class CreateCardCommand : Command<Either<ValidationError, CommandResult>>
    {
        public CreateCardCommand(string sourceToken)
        {
            SourceToken = sourceToken;
        }

        [DataMember(Name = "sourceToken")]
        public string SourceToken { get; private set; }
    }
}
