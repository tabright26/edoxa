// Filename: CreateBankAccountCommand.cs
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
    public sealed class CreateBankAccountCommand : Command
    {
        public CreateBankAccountCommand(string externalAccountTokenId)
        {
            ExternalAccountTokenId = externalAccountTokenId;
        }

        [DataMember(Name = "externalAccountTokenId")]
        public string ExternalAccountTokenId { get; private set; }
    }
}
