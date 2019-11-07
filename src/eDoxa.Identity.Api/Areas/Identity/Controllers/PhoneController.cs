// Filename: PhoneController.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/phone")]
    [ApiExplorerSettings(GroupName = "Phone")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class PhoneController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public PhoneController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find user's address book.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PhoneResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (phoneNumber == null)
            {
                return this.NotFound("Phone number not found.");
            }

            return this.Ok(_mapper.Map<PhoneResponse>(user));
        }

        /// <summary>
        ///     Add user's address.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] PhonePostRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.SetPhoneNumberAsync(user, request.Number);

            if (result.Succeeded)
            {
                return this.Ok(_mapper.Map<PhoneResponse>(user));
            }

            ModelState.Bind(result);

            return this.ValidationProblem(ModelState);
        }
    }
}
