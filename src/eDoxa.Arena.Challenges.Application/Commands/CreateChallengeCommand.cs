// Filename: CreateChallengeCommand.cs
// Date Created: 2019-05-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Commands.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Arena.Challenges.Application.Commands
{
    [DataContract]
    public class CreateChallengeCommand : Command<Either<ValidationError, ChallengeDTO>>
    {
        public CreateChallengeCommand(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            decimal amount,
            Currency currency,
            ChallengeState testModeState = null
        )
        {
            Name = name;
            Game = game;
            Duration = duration;
            BestOf = bestOf;
            PayoutEntries = payoutEntries;

            EntryFee = new EntryFeeDTO
            {
                Amount = amount,
                Currency = currency
            };

            TestModeState = testModeState;
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
        
        [DataMember(Name = "testModeState", IsRequired = false)]
        public ChallengeState TestModeState { get; set; }
    }
}
