// Filename: AddressBookController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/address-book")]
    [ApiExplorerSettings(GroupName = "Address Book")]
    public sealed class AddressBookController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressBookController(IUserService userService, IAddressService addressService, IMapper mapper)
        {
            _userService = userService;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's address book.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AddressDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var addressBook = await _addressService.GetAddressBookAsync(user);

            if (!addressBook.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<AddressDto>>(addressBook));
        }

        [HttpPost]
        [SwaggerOperation("Add user's address.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user's address has been added.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] CreateAddressRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _addressService.AddAddressAsync(
                user.Id.ConvertTo<UserId>(),
                request.CountryIsoCode.ToEnumeration<Country>(),
                request.Line1,
                request.Line2,
                request.City,
                request.State,
                request.PostalCode);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<AddressDto>(result.GetEntityFromMetadata<Address>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut("{addressId}")]
        [SwaggerOperation("Update user's address by id.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user's address has been updated.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PutAsync(AddressId addressId, [FromBody] UpdateAddressRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var address = await _addressService.FindUserAddressAsync(user, addressId);

            if (address == null)
            {
                return this.NotFound("Address not found.");
            }

            var result = await _addressService.UpdateAddressAsync(
                address,
                request.Line1,
                request.Line2,
                request.City,
                request.State,
                request.PostalCode);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<AddressDto>(result.GetEntityFromMetadata<Address>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{addressId}")]
        [SwaggerOperation("Remove user's address by id.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(AddressId addressId)
        {
            var user = await _userService.GetUserAsync(User);

            var address = await _addressService.FindUserAddressAsync(user, addressId);

            if (address == null)
            {
                return this.NotFound("Address not found.");
            }

            var result = await _addressService.RemoveAddressAsync(address);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<AddressDto>(result.GetEntityFromMetadata<Address>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
