// Filename: DoxaTagsController.cs
// Date Created: 2019-08-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/doxatags")]
    [ApiExplorerSettings(GroupName = "DoxaTags")]
    public class DoxaTagsController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public DoxaTagsController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Fetch DoxaTags.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var doxaTags = await _userManager.Store.DoxaTagHistory.GroupBy(doxaTag => doxaTag.UserId)
                .Select(doxaTagHistory => doxaTagHistory.OrderBy(doxaTag => doxaTag.Timestamp).First())
                .ToListAsync();

            if (!doxaTags.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<UserDoxaTagResponse>>(doxaTags));
        }
    }
}
