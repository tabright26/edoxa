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

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional.Maybe;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class CardQueries
    {
        private readonly CardService _cardService;
        private readonly IMapper _mapper;

        public CardQueries(CardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }
    }

    public sealed partial class CardQueries : ICardQueries
    {
        public async Task<Option<CardListDTO>> FindUserCardsAsync(CustomerId customerId)
        {
            var cards = await _cardService.ListAsync(customerId.ToString());

            var list = _mapper.Map<CardListDTO>(cards);

            return list.Any() ? new Option<CardListDTO>(list) : new Option<CardListDTO>();
        }

        public async Task<Option<CardDTO>> FindUserCardAsync(CustomerId customerId, CardId cardId)
        {
            var card = await _cardService.GetAsync(customerId.ToString(), cardId.ToString());

            var mapper = _mapper.Map<CardDTO>(card);

            return mapper != null ? new Option<CardDTO>(mapper) : new Option<CardDTO>();
        }
    }
}