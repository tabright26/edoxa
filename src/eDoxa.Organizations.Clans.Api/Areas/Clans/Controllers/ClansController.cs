// Filename: ClansController.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Api.Extensions;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clans")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClansController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public ClansController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var clans = await _clanService.FetchClansAsync();

            if (!clans.Any())
            {
                return this.NoContent();
            }


            return this.Ok(_mapper.Map<IEnumerable<ClanResponse>>(clans));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ClanPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();
                var clanId = HttpContext.GetClanId();
                var clan = await _clanService.FindClanAsync(clanId);

                if (clan != null)
                {
                    return this.NotFound("User's already has a clan.");
                }
                var result = await _clanService.CreateClanAsync(userId, request.Name);
                if (result.IsValid)
                {
                    return this.Ok("The clan has been created.");
                }
                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);

        }

        [HttpGet("{clanId}")]
        public async Task<IActionResult> GetByIdAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<ClanResponse>(clan));
        }
    }
}
