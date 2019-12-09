// Filename: AddressService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public AddressService(IAddressRepository addressRepository, IServiceBusPublisher serviceBusPublisher)
        {
            _addressRepository = addressRepository;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<Address?> FindUserAddressAsync(User user, AddressId addressId)
        {
            return await _addressRepository.FindAddressAsync(UserId.FromGuid(user.Id), addressId);
        }

        public async Task<IReadOnlyCollection<Address>> GetAddressBookAsync(User user)
        {
            return await _addressRepository.FetchAddressBookAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IdentityResult> RemoveAddressAsync(User user, AddressId addressId)
        {
            var address = await this.FindUserAddressAsync(user, addressId);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Address not found."
                    });
            }

            _addressRepository.Delete(address);

            await _addressRepository.UnitOfWork.CommitAsync();

            //await this.UpdateSecurityStampAsync(user);

            //return await this.UpdateUserAsync(user);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> AddAddressAsync(
            User user,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            var address = new Address(
                UserId.FromGuid(user.Id),
                country,
                line1,
                line2,
                city,
                state,
                postalCode);

            _addressRepository.Create(address);

            await _addressRepository.UnitOfWork.CommitAsync();

            //await this.UpdateSecurityStampAsync(user);

            //var result = await this.UpdateUserAsync(user);

            //if (result.Succeeded)
            //{
            //    await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);
            //}

            //return result;

            await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAddressAsync(
            User user,
            AddressId addressId,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            var address = await _addressRepository.FindAddressAsync(UserId.FromGuid(user.Id), addressId);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserAddressNotFound",
                        Description = "User's address not found."
                    });
            }

            address.Update(
                line1,
                line2,
                city,
                state,
                postalCode);

            await _addressRepository.UnitOfWork.CommitAsync();

            //await this.UpdateSecurityStampAsync(user);

            //var result = await this.UpdateUserAsync(user);

            //if (result.Succeeded)
            //{
            //    await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);
            //}

            //return result;

            await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);

            return IdentityResult.Success;
        }
    }
}
