// Filename: CreateCardCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class CreateCardCommand : Command<IActionResult>
    {
        public CreateCardCommand(string sourceToken, bool defaultCard = false)
        {
            SourceToken = sourceToken;
            DefaultCard = defaultCard;
        }

        [DataMember(Name = "sourceToken")] public string SourceToken { get; private set; }

        [DataMember(Name = "defaultCard", IsRequired = false)]
        public bool DefaultCard { get; private set; }
    }
}