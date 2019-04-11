﻿// Filename: UserNotificationsController.cs
// Date Created: 2019-03-04
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

using eDoxa.Notifications.Api.Properties;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/notifications")]
    public class UserNotificationsController : ControllerBase
    {
        private readonly ILogger<UserNotificationsController> _logger;
        private readonly INotificationQueries _queries;

        public UserNotificationsController(ILogger<UserNotificationsController> logger, INotificationQueries queries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        ///     Get user notifications for the given user ID.
        /// </summary>
        [HttpGet(Name = nameof(FetchUserNotificationsAsync))]
        public async Task<IActionResult> FetchUserNotificationsAsync(UserId userId)
        {
            try
            {
                var notifications = await _queries.FindUserNotificationsAsync(userId);

                if (!notifications.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(notifications);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UserNotificationsController_Error_FetchUserNotificationsAsync);
            }

            return this.BadRequest(Resources.UserNotificationsController_BadRequest_FetchUserNotificationsAsync);
        }

        /// <summary>
        ///     Get a user notification by ID for the given user ID.
        /// </summary>
        [HttpGet("{notificationId}", Name = nameof(FindUserNotificationAsync))]
        public async Task<IActionResult> FindUserNotificationAsync(UserId userId, NotificationId notificationId)
        {
            try
            {
                var notification = await _queries.FindUserNotificationAsync(userId, notificationId);

                if (notification == null)
                {
                    return this.NotFound(Resources.UserNotificationsController_NotFound_FindUserNotificationAsync);
                }

                return this.Ok(notification);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UserNotificationsController_Error_FindUserNotificationAsync);
            }

            return this.BadRequest(Resources.UserNotificationsController_BadRequest_FindUserNotificationAsync);
        }
    }
}