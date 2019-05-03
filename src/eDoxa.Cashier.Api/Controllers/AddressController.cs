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
using eDoxa.Security.Services;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;
        private readonly IAddressQueries _queries;

        public AddressController(IUserInfoService userInfoService, IAddressQueries queries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the user's address.
        /// </summary>
        [HttpGet(Name = nameof(FindUserAddressAsync))]
        public async Task<IActionResult> FindUserAddressAsync()
        {
            var customerId = _userInfoService.CustomerId.Select(CustomerId.Parse).SingleOrDefault();

            var address = await _queries.FindUserAddressAsync(customerId);

            return address
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User address not found."))
                .Single();
        }

        /// <summary>
        ///     Update the user's address.
        /// </summary>
        [HttpPut(Name = nameof(UpdateAddressAsync))]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] UpdateAddressCommand command)
        {
            command.Name = "Francis Quenneville"; // TODO: 

            command.Phone = "5147580313"; // TODO: 

            return await _mediator.SendCommandAsync(command);
        }
    }
}