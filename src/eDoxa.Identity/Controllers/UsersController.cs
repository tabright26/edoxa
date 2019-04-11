﻿// Filename: UsersController.cs
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
using eDoxa.Identity.Properties;

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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
                _logger.LogError(exception, Resources.UsersController_Error_FetchUsersAsync);
            }

            return this.BadRequest(Resources.UsersController_BadRequest_FetchUsersAsync);
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
                    return this.NotFound(Resources.UsersController_NotFound_ChangeUserTagAsync);
                }

                await _userService.ChangeTagAsync(userId, username);

                return this.Ok(Resources.UsersController_Ok_ChangeUserTagAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UsersController_Error_ChangeUserTagAsync);
            }

            return this.BadRequest(Resources.UsersController_BadRequest_ChangeUserTagAsync);
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
                    return this.NotFound(Resources.UsersController_NotFound_ChangeUserStatusAsync);
                }

                await _userService.ChangeStatusAsync(userId, status);

                return this.Ok(Resources.UsersController_Ok_ChangeUserStatusAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UsersController_Error_ChangeUserStatusAsync);
            }

            return this.BadRequest(Resources.UsersController_BadRequest_ChangeUserStatusAsync);
        }
    }
}