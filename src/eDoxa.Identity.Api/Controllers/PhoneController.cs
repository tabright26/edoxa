// Filename: PhoneController.cs
// Date Created: 2020-01-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Domain.Services;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/phone")]
    [ApiExplorerSettings(GroupName = "Phone")]
    public sealed class PhoneController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PhoneController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's phone.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PhoneDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> FindPhoneAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            if (phoneNumber == null)
            {
                return this.NotFound("Phone number not found.");
            }

            return this.Ok(_mapper.Map<PhoneDto>(user));
        }

        [HttpPost]
        [SwaggerOperation("Udpate user's phone.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PhoneDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> ChangePhoneAsync([FromBody] ChangePhoneRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _userService.UpdatePhoneNumberAsync(user, request.Number);

            if (result.Succeeded)
            {
                //Francis: Si je comprends bien, le user ici est modifier parce que la fonction UpdatePhone effectue loperation directement sur le laggregate de user?
                return this.Ok(_mapper.Map<PhoneDto>(user));
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
