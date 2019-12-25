// Filename: TransactionBundlesController.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/transactions/{transactionType}/bundles")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public sealed class TransactionBundlesController : ControllerBase
    {
        private readonly IBundleService _bundleService;
        private readonly IMapper _mapper;

        public TransactionBundlesController(IBundleService bundleService, IMapper mapper)
        {
            _bundleService = bundleService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get bundles by currency.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionBundleDto[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult Get(TransactionType transactionType, [FromQuery] Currency currency)
        {
            if (currency == Currency.Money)
            {
                if (transactionType == TransactionType.Deposit)
                {
                    return this.Ok(_mapper.Map<IEnumerable<TransactionBundleDto>>(_bundleService.FetchDepositMoneyBundles()));
                }

                if (transactionType == TransactionType.Withdrawal)
                {
                    return this.Ok(_mapper.Map<IEnumerable<TransactionBundleDto>>(_bundleService.FetchWithdrawalMoneyBundles()));
                }
            }

            if (currency == Currency.Token)
            {
                if (transactionType == TransactionType.Deposit)
                {
                    return this.Ok(_mapper.Map<IEnumerable<TransactionBundleDto>>(_bundleService.FetchDepositTokenBundles()));
                }
            }

            return this.NotFound("Invalid or unsuported currency.");
        }
    }
}
