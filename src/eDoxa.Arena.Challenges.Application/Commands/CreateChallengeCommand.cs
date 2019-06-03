// Filename: CreateChallengeCommand.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.DTO;
using eDoxa.Seedwork.Application.Commands.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Application.Commands
{
    [DataContract]
    public class CreateChallengeCommand : Command<ChallengeDTO>
    {
        public CreateChallengeCommand(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            EntryFeeDTO entryFee,
            TestModeDTO testMode = null
        )
        {
            Name = name;
            Game = game;
            Duration = duration;
            BestOf = bestOf;
            PayoutEntries = payoutEntries;
            EntryFee = entryFee;
            TestMode = testMode;
        }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "game")]
        public Game Game { get; private set; }

        [DataMember(Name = "duration")]
        public int Duration { get; private set; }

        [DataMember(Name = "bestOf")]
        public int BestOf { get; private set; }

        [DataMember(Name = "payoutEntries")]
        public int PayoutEntries { get; private set; }

        [DataMember(Name = "entryFee")]
        public EntryFeeDTO EntryFee { get; private set; }

        [DataMember(Name = "testMode", IsRequired = false)]
        public TestModeDTO TestMode { get; set; }
    }
}
