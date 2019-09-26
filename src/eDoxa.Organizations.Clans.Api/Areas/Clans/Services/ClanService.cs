// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Services
{
    public sealed class ClanService : IClanService
    {
        private readonly IClanRepository _clanRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ClanService(IClanRepository clanRepository, IServiceBusPublisher serviceBusPublisher)
        {
            _clanRepository = clanRepository;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public Task<IReadOnlyCollection<Clan>> FetchClansAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Clan?> FindClanAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateClanAsync(UserId userId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<FileStream?> GetClanLogoAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateOrUpdateClanLogoAsync(Clan clan, FileStream logo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<Member?> FindMemberAsync(MemberId memberId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> AddMemberToClanAsync(Clan clan, IMemberInfo memberInfo)
        {
            throw new NotImplementedException();
        }


        public Task<ValidationResult> KickMemberFromClanAsync(Clan clan, MemberId memberId)
        {
            throw new NotImplementedException();
        }


        //TODO: I seriously dont think we need this method. KickMemberFromClanAsync() exactly the same.
        public Task<ValidationResult> LeaveClanAsync(Clan clan, MemberId memberId)
        {
            throw new NotImplementedException();
        }
    }
}
