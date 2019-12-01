// Filename: DoxatagsController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Responses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/doxatags")]
    [ApiExplorerSettings(GroupName = "Doxatags")]
    public sealed class DoxatagsController : ControllerBase
    {
        private readonly IDoxatagService _doxatagService;
        private readonly IMapper _mapper;

        public DoxatagsController(IDoxatagService doxatagService, IMapper mapper)
        {
            _doxatagService = doxatagService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Fetch Doxatags.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoxatagResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var doxatags = await _doxatagService.FetchDoxatagsAsync();

            if (!doxatags.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<DoxatagResponse>>(doxatags));
        }
    }
}
