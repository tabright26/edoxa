// Filename: AddressService.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IOptions<IdentityApiOptions> _optionsSnapshot;

        public AddressService(IAddressRepository addressRepository, IServiceBusPublisher serviceBusPublisher, IOptionsSnapshot<IdentityApiOptions> optionsSnapshot)
        {
            _addressRepository = addressRepository;
            _serviceBusPublisher = serviceBusPublisher;
            _optionsSnapshot = optionsSnapshot;
        }

        private IdentityApiOptions Options => _optionsSnapshot.Value;

        public async Task<Address?> FindUserAddressAsync(User user, AddressId addressId)
        {
            return await _addressRepository.FindAddressAsync(UserId.FromGuid(user.Id), addressId);
        }

        public async Task<IReadOnlyCollection<Address>> FetchAddressBookAsync(User user)
        {
            return await _addressRepository.FetchAddressBookAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IDomainValidationResult> RemoveAddressAsync(Address address)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                _addressRepository.Delete(address);

                await _addressRepository.UnitOfWork.CommitAsync();

                //await this.UpdateSecurityStampAsync(user);

                //await this.UpdateUserAsync(user);

                result.AddEntityToMetadata(address);
            }

            return result;
        }

        public async Task<IDomainValidationResult> AddAddressAsync(
            UserId userId,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            var result = new DomainValidationResult();

            var addressBookLimit = Options.Static.AddressBook.Limit;

            if (await _addressRepository.AddressCountAsync(userId) >= addressBookLimit)
            {
                result.AddFailedPreconditionError($"You can have a maximum of {addressBookLimit} addresses in your address book");
            }

            if (result.IsValid)
            {
                var address = new Address(
                    userId,
                    country,
                    line1,
                    line2,
                    city,
                    state,
                    postalCode);

                _addressRepository.Create(address);

                await _addressRepository.UnitOfWork.CommitAsync();

                //await this.UpdateSecurityStampAsync(user);

                //await this.UpdateUserAsync(user);

                await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(userId, address);

                result.AddEntityToMetadata(address);
            }

            return result;
        }

        public async Task<IDomainValidationResult> UpdateAddressAsync(
            Address address,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                address.Update(
                    line1,
                    line2,
                    city,
                    state,
                    postalCode);

                await _addressRepository.UnitOfWork.CommitAsync();

                //await this.UpdateSecurityStampAsync(user);

                //await this.UpdateUserAsync(user);

                await _serviceBusPublisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(address.UserId), address);

                result.AddEntityToMetadata(address);
            }

            return result;
        }
    }
}
