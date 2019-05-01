// Filename: User.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.IdentityServer.Models
{
    public sealed class User : IdentityUser<Guid>
    {
        private PersonalName _personalName;
        private BirthDate _birthDate;

        public User(string email, PersonalName personalName, BirthDate birthDate, string username) : this()
        {
            UserName = username;
            NormalizedUserName = username.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            _personalName = personalName;
            _birthDate = birthDate;
        }

        private User()
        {
            Id = Guid.NewGuid();
            CurrentStatus = UserStatus.Unknown;
            PreviousStatus = UserStatus.Unknown;
            StatusChanged = DateTime.UtcNow;
        }

        public UserStatus CurrentStatus { get; private set; }

        public UserStatus PreviousStatus { get; private set; }

        public DateTime StatusChanged { get; private set; }

        public PersonalName PersonalName => _personalName;

        public BirthDate BirthDate => _birthDate;

        private static User Create(
            Guid userId,
            string email,
            PersonalName personalName,
            BirthDate birthDate,
            string username,
            string phoneNumber,
            string password)
        {
            var admin = new User(email, personalName, birthDate, username)
            {
                Id = userId,
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
                new PersonalName(data.FirstName, data.LastName),
                BirthDate.FromDate(data.BirthDate),
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
    }
}