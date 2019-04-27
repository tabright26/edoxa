// Filename: UsersController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserQueries _queries;
        private readonly IUserService _userService;

        public UsersController(IUserQueries queries, IUserService userService)
        {
            _queries = queries;
            _userService = userService;
        }

        /// <summary>
        ///     Find users.
        /// </summary>
        [HttpGet(Name = nameof(FindUsersAsync))]
        public async Task<IActionResult> FindUsersAsync()
        {
            var users = await _queries.FindUsersAsync();

            return users
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Change user status by ID.
        /// </summary>
        [HttpPut("{userId}/{status}", Name = nameof(ChangeUserStatusAsync))]
        public async Task<IActionResult> ChangeUserStatusAsync(Guid userId, UserStatus status)
        {
            if (!await _userService.UserExistsAsync(userId))
            {
                return this.NotFound(string.Empty);
            }

            await _userService.ChangeStatusAsync(userId, status);

            return this.Ok(string.Empty);
        }
    }
}