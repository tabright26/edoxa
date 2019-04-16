// Filename: UserNotificationsController.cs
// Date Created: 2019-04-13
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
            _logger = logger;
            _queries = queries;
        }

        /// <summary>
        ///     Find user notifications.
        /// </summary>
        [HttpGet(Name = nameof(FindUserNotificationsAsync))]
        public async Task<IActionResult> FindUserNotificationsAsync(UserId userId)
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
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Find a notification for a user.
        /// </summary>
        [HttpGet("{notificationId}", Name = nameof(FindUserNotificationAsync))]
        public async Task<IActionResult> FindUserNotificationAsync(UserId userId, NotificationId notificationId)
        {
            try
            {
                var notification = await _queries.FindUserNotificationAsync(userId, notificationId);

                if (notification == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(notification);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}