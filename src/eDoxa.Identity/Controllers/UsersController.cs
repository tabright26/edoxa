// Filename: UsersController.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserQueries _queries;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserQueries queries, IUserService userService)
        {
            _logger = logger;
            _queries = queries;
            _userService = userService;
        }

        /// <summary>
        ///     Find users.
        /// </summary>
        [HttpGet(Name = nameof(FindUsersAsync))]
        public async Task<IActionResult> FindUsersAsync()
        {
            try
            {
                var users = await _queries.FindUsersAsync();

                if (!users.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(users);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Change user tag by ID.
        /// </summary>
        [HttpPut("{userId}", Name = nameof(ChangeUserTagAsync))]
        public async Task<IActionResult> ChangeUserTagAsync(
            Guid userId,
            [FromBody]
            string username)
        {
            try
            {
                if (!await _userService.UserExistsAsync(userId))
                {
                    return this.NotFound(string.Empty);
                }

                await _userService.ChangeTagAsync(userId, username);

                return this.Ok(string.Empty);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Change user status by ID.
        /// </summary>
        [HttpPut("{userId}/{status}", Name = nameof(ChangeUserStatusAsync))]
        public async Task<IActionResult> ChangeUserStatusAsync(Guid userId, UserStatus status)
        {
            try
            {
                if (!await _userService.UserExistsAsync(userId))
                {
                    return this.NotFound(string.Empty);
                }

                await _userService.ChangeStatusAsync(userId, status);

                return this.Ok(string.Empty);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}