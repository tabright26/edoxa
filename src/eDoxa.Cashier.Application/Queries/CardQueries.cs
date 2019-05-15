// Filename: CardQueries.cs
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

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class CardQueries
    {
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;
        
        public CardQueries(IStripeService stripeService, IMapper mapper)
        {
            _stripeService = stripeService;
            _mapper = mapper;
        }
    }

    public sealed partial class CardQueries : ICardQueries
    {
        public async Task<Option<CardListDTO>> FindUserCardsAsync(StripeCustomerId customerId)
        {
            var option = await _stripeService.ListCardsAsync(customerId);

            return option
                .Select(cards => new Option<CardListDTO>(_mapper.Map<CardListDTO>(cards)))
                .DefaultIfEmpty(new Option<CardListDTO>())
                .Single();
        }

        public async Task<Option<CardDTO>> FindUserCardAsync(StripeCustomerId customerId, StripeCardId cardId)
        {
            var option = await _stripeService.GetCardAsync(customerId, cardId);

            return option
                .Select(card => new Option<CardDTO>(_mapper.Map<CardDTO>(card)))
                .DefaultIfEmpty(new Option<CardDTO>())
                .Single();
        }
    }
}