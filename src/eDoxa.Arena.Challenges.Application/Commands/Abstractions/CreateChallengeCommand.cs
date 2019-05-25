// Filename: CreateChallengeCommand.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Domain;
using eDoxa.Commands.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Arena.Challenges.Application.Commands.Abstractions
{
    [DataContract]
    public abstract class CreateChallengeCommand<TEntryFee> : Command<Either<ValidationError, ChallengeDTO>>
    where TEntryFee : EntryFee
    {
        protected CreateChallengeCommand(
            string name,
            Game game,
            BestOf bestOf,
            PayoutEntries payoutEntries,
            TEntryFee entryFee,
            bool equivalentCurrency = true,
            bool registerParticipants = false,
            bool snapshotParticipantMatches = false
        )
        {
            Name = name;
            Game = game;
            BestOf = bestOf;
            PayoutEntries = payoutEntries;
            EntryFee = entryFee;
            EquivalentCurrency = equivalentCurrency;
            RegisterParticipants = registerParticipants;
            SnapshotParticipantMatches = snapshotParticipantMatches;
        }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "game")]
        public Game Game { get; private set; }

        [DataMember(Name = "bestOf")]
        public BestOf BestOf { get; private set; }

        [DataMember(Name = "payoutEntries")]
        public PayoutEntries PayoutEntries { get; private set; }

        [DataMember(Name = "entryFee")]
        public TEntryFee EntryFee { get; private set; }

        [DataMember(Name = "equivalentCurrency")]
        public bool EquivalentCurrency { get; private set; }

        [DataMember(Name = "registerParticipants", IsRequired = false)]
        public bool RegisterParticipants { get; private set; }

        [DataMember(Name = "snapshotParticipantMatches", IsRequired = false)]
        public bool SnapshotParticipantMatches { get; private set; }
    }
}
