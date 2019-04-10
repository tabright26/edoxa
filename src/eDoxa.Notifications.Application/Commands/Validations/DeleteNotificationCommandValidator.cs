﻿// Filename: DeleteNotificationCommandValidator.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Validations;

using FluentValidation;

namespace eDoxa.Notifications.Application.Commands.Validations
{
    public class DeleteNotificationCommandValidator : CommandValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            this.RuleFor(command => command.NotificationId).NotEmpty();
        }
    }
}