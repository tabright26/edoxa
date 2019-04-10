// Filename: DeleteCardCommandHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DeleteCardCommandHandler : AsyncCommandHandler<DeleteCardCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly CardService _cardService;

        public DeleteCardCommandHandler(IUserRepository userRepository, CardService cardService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        protected override async Task Handle(DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            await _cardService.DeleteAsync(user.CustomerId.ToString(), command.CardId.ToString(), cancellationToken: cancellationToken);
        }
    }
}