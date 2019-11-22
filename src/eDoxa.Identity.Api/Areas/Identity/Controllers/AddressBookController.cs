// Filename: AddressBookController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Responses;

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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserAddressResponse>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] AddressPostRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.AddAddressAsync(
                user,
                request.Country,
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

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Update user's address by id.
        /// </summary>
        [HttpPut("{addressId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync(Guid addressId, [FromBody] AddressPutRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.UpdateAddressAsync(
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

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Remove user's address by id.
        /// </summary>
        [HttpDelete("{addressId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> DeleteAsync(Guid addressId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.RemoveAddressAsync(user, addressId);

            if (result.Succeeded)
            {
                return this.Ok(addressId);
            }
    
            ModelState.Bind(result);

            return this.ValidationProblem(ModelState);
        }
    }
}
