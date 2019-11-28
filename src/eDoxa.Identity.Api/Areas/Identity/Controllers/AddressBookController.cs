﻿// Filename: AddressBookController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Requests;
using eDoxa.Identity.Responses;
using eDoxa.Seedwork.Domain.Miscs;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/address-book")]
    [ApiExplorerSettings(GroupName = "Address Book")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public sealed class AddressBookController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressBookController(IUserManager userManager, IAddressService addressService, IMapper mapper)
        {
            _userManager = userManager;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's address book.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserAddressResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var addressBook = await _addressService.GetAddressBookAsync(user);

            if (!addressBook.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<UserAddressResponse>>(addressBook));
        }

        [HttpPost]
        [SwaggerOperation("Add user's address.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] AddressPostRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _addressService.AddAddressAsync(
                user,
                Country.FromName(request.Country),
                request.Line1,
                request.Line2,
                request.City,
                request.State,
                request.PostalCode);

            if (result.Succeeded)
            {
                return this.Ok("The user's address has been added.");
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut("{addressId}")]
        [SwaggerOperation("Update user's address by id.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync(AddressId addressId, [FromBody] AddressPutRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _addressService.UpdateAddressAsync(
                user,
                addressId,
                request.Line1,
                request.Line2,
                request.City,
                request.State,
                request.PostalCode);

            if (result.Succeeded)
            {
                return this.Ok("The user's address has been updated.");
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{addressId}")]
        [SwaggerOperation("Remove user's address by id.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AddressId))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> DeleteAsync(AddressId addressId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _addressService.RemoveAddressAsync(user, addressId);

            if (result.Succeeded)
            {
                return this.Ok(addressId);
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
