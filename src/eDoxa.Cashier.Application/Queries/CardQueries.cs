// Filename: CardQueries.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;

using Microsoft.EntityFrameworkCore;

using Stripe;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class CardQueries
    {
        private readonly CashierDbContext _context;
        private readonly CardService _cardService;
        private readonly IMapper _mapper;

        public CardQueries(CashierDbContext context, CardService cardService, IMapper mapper)
        {
            _context = context;
            _cardService = cardService;
            _mapper = mapper;
        }

        public async Task<User> FindUserAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users.AsNoTracking()./*Include(user => user.Account).*/Where(user => user.Id == userId).SingleOrDefaultAsync();
        }
    }

    public sealed partial class CardQueries : ICardQueries
    {
        public async Task<CardListDTO> FindUserCardsAsync(UserId userId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var cards = await _cardService.ListAsync(user.CustomerId.ToString());

            return _mapper.Map<CardListDTO>(cards);
        }

        public async Task<CardDTO> FindUserCardAsync(UserId userId, CardId cardId)
        {
            var user = await this.FindUserAsNoTrackingAsync(userId);

            var card = await _cardService.GetAsync(user.CustomerId.ToString(), cardId.ToString());

            return _mapper.Map<CardDTO>(card);
        }
    }
}