// Filename: DeleteCardCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, IActionResult>
    {
        private readonly CardService _cardService;
        private readonly IUserRepository _userRepository;

        public DeleteCardCommandHandler(IUserRepository userRepository, CardService cardService)
        {
            _userRepository = userRepository;
            _cardService = cardService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsNoTrackingAsync(command.UserId);

            await _cardService.DeleteAsync(user.CustomerId.ToString(), command.CardId.ToString(), cancellationToken: cancellationToken);

            return new OkResult();
        }
    }
}