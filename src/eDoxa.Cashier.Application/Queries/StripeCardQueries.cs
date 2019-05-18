// Filename: StripeCardQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class StripeCardQueries
    {
        private readonly IMapper _mapper;
        private readonly ICashierSecurity _security;
        private readonly IStripeService _service;

        public StripeCardQueries(IStripeService service, ICashierSecurity security, IMapper mapper)
        {
            _service = service;
            _security = security;
            _mapper = mapper;
        }
    }

    public sealed partial class StripeCardQueries : IStripeCardQueries
    {
        public async Task<StripeCardListDTO> GetCardsAsync()
        {
            var customerId = _security.StripeCustomerId;

            var cards = await _service.GetCardsAsync(customerId);

            return _mapper.Map<StripeCardListDTO>(cards);
        }

        public async Task<Option<StripeCardDTO>> GetCardAsync(StripeCardId cardId)
        {
            var customerId = _security.StripeCustomerId;

            var option = await _service.GetCardAsync(customerId, cardId);

            return option
                .Select(card => new Option<StripeCardDTO>(_mapper.Map<StripeCardDTO>(card)))
                .DefaultIfEmpty(new Option<StripeCardDTO>())
                .Single();
        }
    }
}