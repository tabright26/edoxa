// Filename: AccountDepositController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Responses;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Bundles.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/bundles")]
    [ApiExplorerSettings(GroupName = "Bundles")]
    public sealed class BundlesController : ControllerBase
    {
        private readonly IBundleService _bundleService;
        private readonly IMapper _mapper;

        public BundlesController(IBundleService bundleService, IMapper mapper)
        {
            _bundleService = bundleService;
            _mapper = mapper;
        }

        //[HttpGet("bundles")]
        //[SwaggerOperation("Get bundles by currency.")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BundleResponse[]))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        //public IActionResult Get(Currency currency)
        //{
        //    if (currency == Currency.Money)
        //    {
        //        return this.Ok(_mapper.Map<IEnumerable<BundleResponse>>(_bundleService.FetchWithdrawalMoneyBundles()));
        //    }

        //    return this.BadRequest("Invalid or unsuported currency.");
        //}

        [HttpGet]
        [SwaggerOperation("Get bundles by currency.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BundleResponse[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Get(Currency currency, TransactionType transactionType)
        {
            if (currency == Currency.Money)
            {
                return this.Ok(_mapper.Map<IEnumerable<BundleResponse>>(_bundleService.FetchDepositMoneyBundles()));
            }

            if (currency == Currency.Token)
            {
                return this.Ok(_mapper.Map<IEnumerable<BundleResponse>>(_bundleService.FetchDepositTokenBundles()));
            }

            return this.BadRequest("Invalid or unsuported currency.");
        }
    }
}
