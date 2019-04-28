// Filename: UsersController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Commands.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAddressQueries _queries;

        public UsersController(IAddressQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the user's address.
        /// </summary>
        [HttpGet("{userId}/address", Name = nameof(FindUserAddressAsync))]
        public async Task<IActionResult> FindUserAddressAsync(UserId userId)
        {
            var address = await _queries.FindUserAddressAsync(userId);

            return address
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User address not found."))
                .Single();
        }

        /// <summary>
        ///     Update the user's address.
        /// </summary>
        [HttpPut("{userId}/address", Name = nameof(UpdateAddressAsync))]
        public async Task<IActionResult> UpdateAddressAsync(
            UserId userId,
            [FromBody] UpdateAddressCommand command)
        {
            command.UserId = userId;

            command.Name = "Francis Quenneville"; // TODO: 

            command.Phone = "5147580313"; // TODO: 

            return await _mediator.SendCommandAsync(command);
        }
    }
}