// Filename: StripeCardQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Security.Abstractions;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class StripeCardQueries
    {
        private readonly ICashierHttpContext _httpContext;
        private readonly IMapper _mapper;
        private readonly IStripeService _service;

        public StripeCardQueries(IStripeService service, ICashierHttpContext httpContext, IMapper mapper)
        {
            _service = service;
            _httpContext = httpContext;
            _mapper = mapper;
        }
    }

    public sealed partial class StripeCardQueries : IStripeCardQueries
    {
        public async Task<StripeCardListDTO> GetCardsAsync()
        {
            var customerId = _httpContext.StripeCustomerId;

            var cards = await _service.GetCardsAsync(customerId);

            return _mapper.Map<StripeCardListDTO>(cards);
        }
    }
}