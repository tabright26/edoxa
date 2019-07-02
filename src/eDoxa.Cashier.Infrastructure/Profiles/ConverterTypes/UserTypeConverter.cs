// Filename: UserTypeConverter.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class UserTypeConverter : ITypeConverter<UserModel, User>
    {
        [NotNull]
        public User Convert([NotNull] UserModel source, [NotNull] User destination, [NotNull] ResolutionContext context)
        {
            var user = new User(UserId.FromGuid(source.Id), source.ConnectAccountId, source.CustomerId);

            user.AddBankAccount(source.BankAccountId);

            return user;
        }
    }
}
