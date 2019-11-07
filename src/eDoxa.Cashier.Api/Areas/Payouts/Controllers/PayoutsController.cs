// Filename: PayoutsController.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Responses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Areas.Payouts.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/payouts")]
    [ApiExplorerSettings(GroupName = "Payouts")]
    public class PayoutsController : ControllerBase
    {
        private readonly IPayoutFactory _payoutFactory;
        private readonly IMapper _mapper;

        public PayoutsController(IPayoutFactory payoutFactory, IMapper mapper)
        {
            _payoutFactory = payoutFactory;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(int payoutEntries, int entryFeeAmount, string entryFeeCurrency)
        {
            var strategy = _payoutFactory.CreateInstance();

            var payout = strategy.GetPayout(new PayoutEntries(payoutEntries), new EntryFee(entryFeeAmount, Currency.FromName(entryFeeCurrency)));

            return this.Ok(_mapper.Map<PayoutResponse>(payout));
        }
    }
}
