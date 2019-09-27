// Filename: AccountService.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Validations.Extensions;
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

        public async Task<IReadOnlyCollection<Clan>> FetchClansAsync()
        {
            return await _clanRepository.FetchClansAsync();
        }

        public async Task<Clan?> FindClanAsync(ClanId clanId)
        {
            return await _clanRepository.FindClanAsync(clanId);
        }

        public async Task<ValidationResult> CreateClanAsync(UserId userId, string name)
        {
            var clans = await _clanRepository.FetchClansAsync();

            if (clans.Any(clan => clan.Name == name))
            {
                var failure = new ValidationFailure(string.Empty, "Clan with the same name already exist");

                return failure.ToResult();
            }

            _clanRepository.Create(new Clan(name, userId));
            await _clanRepository.CommitAsync();

            return new ValidationResult();
        }

        public Task<FileStream?> GetClanLogoAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> CreateOrUpdateClanLogoAsync(Clan clan, FileStream logo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId)
        {
            return await _clanRepository.FetchMembersAsync(clanId);
        }

        public async Task<Member?> FindMemberAsync(Clan clan, MemberId memberId)
        {
            return await _clanRepository.FindMemberAsync(clan.Id, memberId);
        }

        public async Task<ValidationResult> AddMemberToClanAsync(Clan clan, IMemberInfo memberInfo)
        {
            if (clan.Members.Any(member => member.UserId == memberInfo.UserId && member.ClanId == memberInfo.ClanId))
            {
                var failure = new ValidationFailure(string.Empty, "Member already in the clan.");
                return failure.ToResult();
            }

            clan.AddMember(memberInfo);
            await _clanRepository.CommitAsync();
            return new ValidationResult();
        }


        public async Task<ValidationResult> KickMemberFromClanAsync(Clan clan, MemberId memberId)
        {
            if (clan.Members.All(member => member.Id != memberId))
            {
                var failure = new ValidationFailure(string.Empty, "Member not in the clan.");
                return failure.ToResult();
            }

            var memberToKick = clan.Members.SingleOrDefault(member => member.Id == memberId);

            clan.Members.Remove(memberToKick);
            await _clanRepository.CommitAsync();
            return new ValidationResult();
        }


        public async Task<ValidationResult> LeaveClanAsync(Clan clan, UserId userId)
        {
            var memberToRemove = clan.Members.SingleOrDefault(member => member.UserId == userId);

            if (memberToRemove != null) //Member leaves the clan
            {
                clan.Members.Remove(memberToRemove);
            }
            else
            {
                if (clan.Members.Any() && clan.OwnerId == userId) //Owner leaves the clan with member left
                {
                    var nextOwner = clan.Members.First();
                    clan.ChangeOwner(nextOwner);
                    clan.Members.Remove(nextOwner);
                }
                else if (clan.OwnerId == userId)//Owner leaves the clan without member left
                {
                    _clanRepository.Delete(clan);
                }
                else
                {
                    var failure = new ValidationFailure(string.Empty, "User not in the clan.");
                    return failure.ToResult();
                }
            }
            await _clanRepository.CommitAsync();
            return new ValidationResult();
        }
    }
}
