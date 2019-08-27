// Filename: RegisterParticipantRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.Requests
{
    [DataContract]
    public sealed class RegisterParticipantRequest : IRequest
    {
        public RegisterParticipantRequest(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

#nullable disable
        public RegisterParticipantRequest()
        {
            // Required by Fluent Validation
        }
#nullable restore

        [IgnoreDataMember] public ChallengeId ChallengeId { get; private set; }
    }
}
