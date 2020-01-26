// Filename: DoxatagHistoryController.cs
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
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/doxatag-history")]
    [ApiExplorerSettings(GroupName = "Doxatag History")]
    public sealed class DoxatagHistoryController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDoxatagService _doxatagService;
        private readonly IMapper _mapper;

        public DoxatagHistoryController(IUserService userService, IDoxatagService doxatagService, IMapper mapper)
        {
            _userService = userService;
            _doxatagService = doxatagService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find user's Doxatag history.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoxatagDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            var doxatagHistory = await _doxatagService.FetchDoxatagHistoryAsync(user);

            if (!doxatagHistory.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<DoxatagDto>>(doxatagHistory));
        }

        [HttpPost]
        [SwaggerOperation("Create new user's Doxatag.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The user's Doxatag has been created.", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] ChangeDoxatagRequest request)
        {
            var user = await _userService.GetUserAsync(User);

            var result = await _doxatagService.ChangeDoxatagAsync(user, request.Name);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<DoxatagDto>(result.GetEntityFromMetadata<Doxatag>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
