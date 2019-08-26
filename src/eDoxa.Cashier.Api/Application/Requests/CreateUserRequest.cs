// Filename: CreateUserRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

using MediatR;

namespace eDoxa.Cashier.Api.Application.Requests
{
    [DataContract]
    public sealed class CreateUserRequest : IRequest
    {
        public CreateUserRequest(UserId userId)
        {
            UserId = userId;
        }

#nullable disable
        public CreateUserRequest()
        {
            // Required by Fluent Validation
        }
#nullable restore

        [DataMember] public UserId UserId { get; private set; }
    }
}
