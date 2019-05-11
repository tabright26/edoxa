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

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class CreateCardCommand : Command<IActionResult>
    {
        public CreateCardCommand(string sourceToken, bool defaultSource = false)
        {
            SourceToken = sourceToken;
            DefaultSource = defaultSource;
        }

        [DataMember(Name = "sourceToken")] public string SourceToken { get; private set; }

        [DataMember(Name = "defaultSource", IsRequired = false)]
        public bool DefaultSource { get; private set; }
    }
}