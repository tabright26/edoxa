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

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Services.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Queries
{
    public sealed partial class StripeCardQueries
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IStripeService _service;

        public StripeCardQueries(IStripeService service, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserRepository userRepository)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
        }
    }

    public sealed partial class StripeCardQueries : IStripeCardQueries
    {
        public async Task<StripeCardListDTO> GetCardsAsync()
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            var cards = await _service.GetCardsAsync(user.GetCustomerId());

            return _mapper.Map<StripeCardListDTO>(cards);
        }
    }
}
