// Filename: CardQueries.cs
// Date Created: 2019-04-21
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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class CardQueries
    {
        private readonly CardService _cardService;
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public CardQueries(CashierDbContext context, CardService cardService, IMapper mapper)
        {
            _context = context;
            _cardService = cardService;
            _mapper = mapper;
        }

        private async Task<User> FindUserAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(user => user.MoneyAccount.Transactions)
                .Include(user => user.TokenAccount.Transactions)
                .Where(user => user.Id == userId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class CardQueries : ICardQueries
    {
        public async Task<Option<CardListDTO>> FindUserCardsAsync(UserId userId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var cards = await _cardService.ListAsync(user.CustomerId.ToString());

            var list = _mapper.Map<CardListDTO>(cards);

            return list.Any() ? new Option<CardListDTO>(list) : new Option<CardListDTO>();
        }

        public async Task<Option<CardDTO>> FindUserCardAsync(UserId userId, CardId cardId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var card = await _cardService.GetAsync(user.CustomerId.ToString(), cardId.ToString());

            var mapper = _mapper.Map<CardDTO>(card);

            return mapper != null ? new Option<CardDTO>(mapper) : new Option<CardDTO>();
        }
    }
}