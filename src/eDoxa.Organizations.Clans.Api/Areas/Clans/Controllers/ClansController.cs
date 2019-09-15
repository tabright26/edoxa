// Filename: ClansController.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClansController : ControllerBase
    {
        private readonly IClanRepository _clanRepository;

        public ClansController(IClanRepository clanRepository)
        {
            _clanRepository = clanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var clans = await _clanRepository.FetchClansAsync();

            if (!clans.Any())
            {
                return this.NoContent();
            }

            return this.Ok(clans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var clanModel = await _clanRepository.FindClanAsync(id);

            if (clanModel == null)
            {
                return this.NotFound();
            }

            return this.Ok(clanModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string name)
        {
            var clanModel = new ClanModel
            {
                Name = name,
                Id = Guid.NewGuid(),
                Members = new Collection<MemberModel>()
            };

            _clanRepository.Create(clanModel);
            await _clanRepository.SaveChangesAsync();

            return this.Ok("Clan added");
        }
    }
}
