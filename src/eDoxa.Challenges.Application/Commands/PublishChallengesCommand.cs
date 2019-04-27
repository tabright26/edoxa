// Filename: PublishChallengesCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Challenges.Application.Commands
{
    [DataContract]
    public sealed class PublishChallengesCommand : Command
    {
    }
}