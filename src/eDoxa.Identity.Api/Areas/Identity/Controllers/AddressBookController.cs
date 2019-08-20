// Filename: AddressController.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/address-book")]
    [ApiExplorerSettings(GroupName = "Address Book")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class AddressBookController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AddressBookController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's address book.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var addressBook = await _userManager.GetAddressBookAsync(user);

            if (!addressBook.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<UserAddressResponse>>(addressBook));
        }

        /// <summary>
        ///     Add user's address.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddressPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.AddAddressAsync(
                    user,
                    request.Country,
                    request.Line1,
                    request.Line2,
                    request.City,
                    request.State,
                    request.PostalCode
                );

                if (result.Succeeded)
                {
                    return this.Ok("The user's address has been added.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }

        /// <summary>
        ///     Find user's address by id.
        /// </summary>
        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetByIdAsync(Guid addressId)
        {
            var user = await _userManager.GetUserAsync(User);

            var address = await _userManager.FindUserAddressAsync(user, addressId);

            if (address == null)
            {
                return this.NotFound("User's address not found.");
            }

            return this.Ok(_mapper.Map<UserAddressResponse>(address));
        }

        /// <summary>
        ///     Update user's address by id.
        /// </summary>
        [HttpPut("{addressId}")]
        public async Task<IActionResult> PutAsync(Guid addressId, [FromBody] AddressPutRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.UpdateAddressAsync(
                    user,
                    addressId,
                    request.Line1,
                    request.Line2,
                    request.City,
                    request.State,
                    request.PostalCode
                );

                if (result.Succeeded)
                {
                    return this.Ok("The user's address has been updated.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }

        /// <summary>
        ///     Remove user's address by id.
        /// </summary>
        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAsync(Guid addressId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.RemoveAddressAsync(user, addressId);

                if (result.Succeeded)
                {
                    return this.Ok("The user's address has been removed.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
