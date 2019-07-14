// Filename: IdentityStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Api.Infrastructure.Data.Storage
{
    public static class IdentityStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";

        public static IReadOnlyCollection<User> TestUsers => Users.OrderBy(user => user.Id).ToList();

        private static IEnumerable<User> Users
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords(
                        new
                        {
                            Id = default(Guid),
                            Gamertag = default(string),
                            Email = default(string),
                            EmailConfirmed = default(bool),
                            Phone = default(string),
                            PhoneConfirmed = default(bool),
                            BirthDate = default(DateTime),
                            FirstName = default(string),
                            LastName = default(string)
                        }
                    );

                    foreach (var record in records)
                    {
                        var gamertag = new Gamertag(record.Gamertag);

                        var email = new Email(record.Email);

                        if (record.EmailConfirmed)
                        {
                            email.Confirm();
                        }

                        var personalName = new PersonalName(record.FirstName, record.LastName);

                        var birthDate = new BirthDate(record.BirthDate);

                        var user = new User(gamertag, email, birthDate, personalName);

                        user.SetEntityId(UserId.FromGuid(record.Id));

                        var phone = new Phone(record.Phone);

                        if (record.PhoneConfirmed)
                        {
                            phone.Confirm();
                        }

                        user.LinkPhone(phone);

                        yield return user;
                    }
                }
            }
        }
    }
}
