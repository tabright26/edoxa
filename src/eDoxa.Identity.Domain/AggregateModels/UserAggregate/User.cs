// Filename: User.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.ValueObjects;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class User : IdentityUser<Guid>
    {
        public User(string email, string firstName, string lastName, int year, int month, int day, string username) : this()
        {
            Email = email;
            NormalizedEmail = Email.ToUpperInvariant();
            Name = new Name(firstName, lastName);
            BirthDate = new BirthDate(year, month, day);
            Tag = new UserTag(username);
        }

        private User()
        {
            Id = Guid.NewGuid();
            UserName = Id.ToString();
            NormalizedUserName = UserName.ToUpperInvariant();
            CurrentStatus = UserStatus.Unknown;
            PreviousStatus = UserStatus.Unknown;
            StatusChanged = DateTime.UtcNow;
        }

        public UserStatus CurrentStatus { get; private set; }

        public UserStatus PreviousStatus { get; private set; }

        public DateTime StatusChanged { get; private set; }

        public Name Name { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public UserTag Tag { get; private set; }

        public static User Create(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day,
            string username,
            string phoneNumber,
            string password)
        {
            var admin = new User(email, firstName, lastName, year, month, day, username)
            {
                Id = userId,
                UserName = userId.ToString(),
                NormalizedUserName = userId.ToString().ToUpperInvariant(),
                NormalizedEmail = email.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<User>();

            admin.PasswordHash = passwordHasher.HashPassword(admin, password);

            return admin;
        }

        public static User Create(UserData data)
        {
            return Create(
                data.Id,
                data.Email,
                data.FirstName,
                data.LastName,
                data.BirthDate.Year,
                data.BirthDate.Month,
                data.BirthDate.Day,
                data.Username,
                data.PhoneNumber,
                data.Password
            );
        }

        public void Connect()
        {
            this.ChangeStatus(PreviousStatus != UserStatus.Unknown ? PreviousStatus : UserStatus.Online);
        }

        public void Disconnect()
        {
            this.ChangeStatus(UserStatus.Offline);
        }

        public void ChangeStatus(UserStatus status)
        {
            if (CurrentStatus != status)
            {
                switch (status)
                {
                    case UserStatus.Online:

                    case UserStatus.Absent:

                    case UserStatus.Invisible:

                        PreviousStatus = UserStatus.Unknown;

                        break;

                    case UserStatus.Offline:

                        PreviousStatus = CurrentStatus;

                        break;
                }

                CurrentStatus = status;

                StatusChanged = DateTime.UtcNow;
            }
        }

        public void ChangeTag(string username)
        {
            Tag.ChangeTag(username);
        }
    }
}