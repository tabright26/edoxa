// Filename: CreateBankAccountCommand.cs
// Date Created: 2019-05-14
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
    public sealed class CreateBankAccountCommand : Command<Either<ValidationError, CommandResult>>
    {
        public CreateBankAccountCommand(string externalAccountTokenId)
        {
            ExternalAccountTokenId = externalAccountTokenId;
        }

        [DataMember(Name = "externalAccountTokenId")]
        public string ExternalAccountTokenId { get; private set; }
    }
}