﻿// Filename: InitializeServiceCommand.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Application.Commands
{
    public sealed class InitializeServiceCommand : Command
    {
        public InitializeServiceCommand(
            UserId userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day
        )
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
            Month = month;
            Day = day;
        }

        public UserId UserId { get; private set; }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }
    }
}