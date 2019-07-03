// Filename: CardQuery.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Infrastructure.Queries
{
    public sealed partial class CardQuery
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserQuery _userQuery;
        private readonly IStripeService _service;

        public CardQuery(
            IStripeService service,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUserQuery userQuery
        )
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userQuery = userQuery;
        }
    }

    public sealed partial class CardQuery : ICardQuery
    {
        public async Task<IReadOnlyCollection<CardViewModel>> GetCardsAsync()
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userQuery.FindUserAsync(userId);

            var cards = await _service.GetCardsAsync(user.GetCustomerId());

            return _mapper.Map<IReadOnlyCollection<CardViewModel>>(cards);
        }
    }
}
