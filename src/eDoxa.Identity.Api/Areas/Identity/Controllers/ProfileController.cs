// Filename: InformationsController.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/informations")]
    [ApiExplorerSettings(GroupName = "Informations")]
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
        [SwaggerOperation("Find user's profile informations.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var profile = await _userService.GetInformationsAsync(user);

            if (profile == null)
            {
                return this.NotFound("User profile not found.");
            }

            return this.Ok(_mapper.Map<ProfileDto>(profile));
        }

        [HttpPost]
        [SwaggerOperation("Create user's profile informations.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] CreateProfileRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var informations = await _userService.GetInformationsAsync(user);

            if (informations != null)
            {
                return this.BadRequest("The user's personal information has already been created.");
            }

            var result = await _userService.CreateInformationsAsync(
                user,
                request.FirstName,
                request.LastName,
                request.Gender.ToEnumeration<Gender>(),
                new UserDob(request.Dob.Year, request.Dob.Month, request.Dob.Day));

            if (result.Succeeded)
            {
                return this.Ok("The user's personal info has been created.");
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut]
        [SwaggerOperation("Update user's profile informations.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateProfileRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var informations = await _userService.GetInformationsAsync(user);

            if (informations == null)
            {
                return this.BadRequest("The user's personal informations does not exist.");
            }

            var result = await _userService.UpdateInformationsAsync(user, request.FirstName);

            if (result.Succeeded)
            {
                return this.Ok("The user's personal info has been updated.");
            }

            ModelState.Bind(result);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
