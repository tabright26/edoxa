// Filename: CreateUserRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

using MediatR;

namespace eDoxa.Cashier.Api.Application.Requests
{
    public sealed class CreateUserRequest : IRequest
    {
        public CreateUserRequest(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; private set; }
    }
}
