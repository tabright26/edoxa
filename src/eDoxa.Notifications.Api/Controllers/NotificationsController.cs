﻿// Filename: NotificationsController.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Notifications.Api.Properties;
using eDoxa.Notifications.Application.Commands;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly IMediator _mediator;

        public NotificationsController(ILogger<NotificationsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Read notification by ID for the given recipient ID.
        /// </summary>
        [HttpPatch("{notificationId}/read", Name = nameof(ReadNotificationAsync))]
        public async Task<IActionResult> ReadNotificationAsync(NotificationId notificationId)
        {
            try
            {
                var command = new ReadNotificationCommand(notificationId);

                await _mediator.SendCommandAsync(command);

                return this.Ok(Resources.NotificationsController_Ok_ReadNotificationAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.NotificationsController_Error_ReadNotificationAsync);
            }

            return this.BadRequest(Resources.NotificationsController_BadRequest_ReadNotificationAsync);
        }

        /// <summary>
        ///     Delete notification by ID for the given recipient ID.
        /// </summary>
        [HttpDelete("{notificationId}/delete", Name = nameof(DeleteNotificationAsync))]
        public async Task<IActionResult> DeleteNotificationAsync(NotificationId notificationId)
        {
            try
            {
                var command = new DeleteNotificationCommand(notificationId);

                await _mediator.SendCommandAsync(command);

                return this.Ok(Resources.NotificationsController_Ok_DeleteNotificationAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.NotificationsController_Error_DeleteNotificationAsync);
            }

            return this.BadRequest(Resources.NotificationsController_BadRequest_DeleteNotificationAsync);
        }
    }
}