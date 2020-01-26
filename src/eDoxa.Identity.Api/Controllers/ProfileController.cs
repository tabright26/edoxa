// Filename: ProfileController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
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
    [Route("api/profile")]
    [ApiExplorerSettings(GroupName = "Profile")]
    public sealed class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var profile = await _userService.GetProfileAsync(user);

            if (profile == null)
            {
                return this.NotFound("User profile not found.");
            }

            return this.Ok(_mapper.Map<ProfileDto>(profile));
        }

        [HttpPost]
        [SwaggerOperation("Create user's profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user's profile has been created.", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] CreateProfileRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _userService.CreateProfileAsync(
                user,
                request.FirstName,
                request.LastName,
                request.Gender.ToEnumeration<Gender>(),
                request.Dob.Year,
                request.Dob.Month,
                request.Dob.Day);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<ProfileDto>(result.GetEntityFromMetadata<UserProfile>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut]
        [SwaggerOperation("Update user's profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user's profile has been updated.", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateProfileRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _userService.UpdateProfileAsync(user, request.FirstName);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<ProfileDto>(result.GetEntityFromMetadata<UserProfile>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
